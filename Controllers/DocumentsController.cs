using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using StajYonetimSistemi.Services;
using StajYonetimSistemi.Validation;
using System.Globalization; // tr-TR için 

namespace StajYonetimSistemi.Controllers
{
    // öğrencinin Ek-16 belgesini yüklediği sayfaları yöneten controller
    [Authorize(Roles = "ROLE_STUDENT")]
    // eğer sadece öğrenci ise girebilir
    public class DocumentsController : Controller
    {
        private readonly StajDbContext veritabani; // veri tabanına erişiip satır eklemek için

        private readonly DosyaKaydedici kaydedici;  // dosyayı diske kaydetmek icin

        private readonly string uploadsKlasoru; // yüklenen dosyaların durduğu klasör okuyup kontrol etmek icin

        private readonly DosyaFormatDogrulayici formatKurali = new DosyaFormatDogrulayici(); // dosya uzantısını kontrol eden kural

        private readonly Ek16Ayiklayici ayiklayici = new Ek16Ayiklayici(); // okunan sözlüğü düzenli bilgiye çevirir

        public DocumentsController(StajDbContext context, IWebHostEnvironment ortam) // normalde new ile sınıfı biz oluştururken burada asp.net kendisi oluştruyor
        // asp.net parantez içindekileri bize hazır olarak verir StajDbContext context veri tabanı bağlantısı IWebHostEnvironment ortam ise projenin bilgisini verir
        {
            veritabani = context;
            uploadsKlasoru = Path.Combine(ortam.ContentRootPath, "uploads"); // uploads klasörünün tam yolu
            kaydedici = new DosyaKaydedici(uploadsKlasoru); // kaydediciye de aynı yolu veriyoruz
        }

        [HttpGet]
        // Yükleme sayfasını gösterir
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        // Yükle butonuna basılınca çalışır.
        public IActionResult Upload(IFormFile? dosya)
        {
            if (dosya == null || dosya.Length == 0) // dosya seçilmemişse ya da boşsa uyarır
            {
                ModelState.AddModelError("", "Lütfen bir dosya seçin");
                return View();
            }
            var sonuc = formatKurali.Dogrula(dosya.FileName); // uzantı kuralına bak geçersizse kullanıcıya mesajı göster
            if (!sonuc.Gecerli)
            {
                ModelState.AddModelError("", sonuc.Mesaj);
                return View();
            }

            string email = User.Identity?.Name ?? "";
            var ogrenci = veritabani.Students.FirstOrDefault(s => s.Email == email); // giriş yapan öğrenciyi e postasından bulur
            if (ogrenci == null)
            {
                ModelState.AddModelError("", "Ögrenci kaydi bulunamadi,"); // eger bulursa öğrenciyi bulamazsa null döner
                return View();
            }

            string kaydedilenAd = kaydedici.Kaydet(dosya); // guild ile verilen yeni adı kaydeder

            var belge = new Document // burda mesaj satırı hazırlanır eklenir ve kaydedilir
            {
                StudentId = ogrenci.id,
                UploadDate = DateTime.Now,
                OriginalFileName = Path.GetFileName(dosya.FileName),
                StoredPath = kaydedilenAd
            };
            veritabani.Documents.Add(belge);

            string uzanti = Path.GetExtension(kaydedilenAd).ToLowerInvariant(); // kaydedilen belge docx ise okuyup kurallara veriyoruz
            var rapor = new List<string>(); // ekranda göstereceğimiz satırlar

            bool okundu = false; // belge başarıyla okunabildi mi
            bool hataVar = false; // kontrollerden en az biri hata verdi mi

            if (uzanti == ".docx")
            {
                try
                {
                    BelgeOkuyucu okuyucu = new DocxOkuyucu(); // türü BelgeOkuyucu ama içinde DocxOkuyucu var çok biçimlilik sağlıyor
                    var alanlar = okuyucu.Oku(Path.Combine(uploadsKlasoru, kaydedilenAd));
                    var bilgi = ayiklayici.Ayikla(alanlar);

                    string uyruk = bilgi.Uyruk.Replace(".", "").Trim().ToUpper(new CultureInfo("tr-TR")); // uyruğu karşılaştırmaya için noktaları at boşlukları sil büyük harfe çevir 

                    if (uyruk == "")
                    {
                        rapor.Add("Kimlik: uyruk belgeden okunamadi, kontrol yapilamadi"); // uyruğu okuyamadıysak hangi kuralın çalışacağını bilemeyiz
                        hataVar = true;
                    }
                    else
                    {
                        Kural kimlikKurali;
                        if (uyruk == "TC" || uyruk == "TÜRKİYE")
                        {
                            kimlikKurali = new TcKimlikDogrulayici();
                        }
                        else
                        {
                            kimlikKurali = new YabanciKimlikDogrulayici(); // ülke adı yazan herkes yabancı sayılır
                        }

                        var kimlikSonuc = kimlikKurali.Dogrula(bilgi.TcNo);
                        rapor.Add("Kimlik: " + (kimlikSonuc.Gecerli ? "geçerli" : kimlikSonuc.Mesaj));
                        if (!kimlikSonuc.Gecerli) 
                        { 
                            hataVar = true; 
                        }
                }

                    var telefonSonuc = new TelefonDogrulayici().Dogrula(bilgi.Telefon);
                    rapor.Add("Telefon: " + (telefonSonuc.Gecerli ? "geçerli" : telefonSonuc.Mesaj));
                    if (!telefonSonuc.Gecerli) 
                    { 
                        hataVar = true; 
                    }

                    var epostaSonuc = new EpostaDogrulayici().Dogrula(bilgi.Eposta);
                    rapor.Add("E-posta: " + (epostaSonuc.Gecerli ? "geçerli" : epostaSonuc.Mesaj));
                    if (!epostaSonuc.Gecerli) 
                    { 
                        hataVar = true; 
                    }
                    okundu = true;
                }
                catch (Exception)
                {
                    // bozuk ya da beklenmeyen bir docx gelirse site çökmesin
                    rapor.Add("Belge okunamadi, dosya bozuk olabilir");
                }
            }
            else
            {
                rapor.Add("Bu formatta otomatik okuma henüz desteklenmiyor");
            }
            // kontrol sonucuna göre belgenin durumunu belirliyoruz
            if (!okundu)
            {
                belge.Status = DocumentStatus.Inceleniyor; // otomatik okunamadı elle bakılmalı
            }
            else if (hataVar)
            {
                belge.Status = DocumentStatus.HataliEksik;
            }
            else
            {
                belge.Status = DocumentStatus.Onaylandi;
            }
            veritabani.SaveChanges(); // belgeyi durumuyla birlikte kaydediyoruz

            // mesajı ve raporu TempData ile bir sonraki isteğe taşıyoruz 
            TempData["Mesaj"] = "Belge başariyla yüklendi";
            TempData["Rapor"] = rapor.ToArray();

            // sayfayı yeniden yönlendiriyoruz böylece F5'e basınca belge ikinci kez yüklenmez
            return RedirectToAction("Upload");
        }
    }
}

            