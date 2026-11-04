using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using StajYonetimSistemi.Services;
using StajYonetimSistemi.Validation;

namespace StajYonetimSistemi.Controllers
{
    // öğrencinin Ek-16 belgesini yüklediği sayfaları yöneten controller
    [Authorize(Roles = "ROLE_STUDENT")]
    // eğer sadece öğrenci ise girebilir
    public class DocumentsController : Controller
    {
        private readonly StajDbContext veritabani; // veri tabanına erişiip satır eklemek için

        private readonly DosyaKaydedici kaydedici;  // dosyayı diske kaydetmek icin

        private readonly DosyaFormatDogrulayici formatKurali = new DosyaFormatDogrulayici(); // dosya uzantısını kontrol eden kural

        public DocumentsController(StajDbContext context, IWebHostEnvironment ortam) // normalde new ile sınıfı biz oluştururken burada asp.net kendisi oluştruyor
        // asp.net parantez içindekileri bize hazır olarak verir StajDbContext context veri tabanı bağlantısı IWebHostEnvironment ortam ise projenin bilgisini verir
        {
            veritabani = context;
            kaydedici = new DosyaKaydedici(Path.Combine(ortam.ContentRootPath, "uploads")); // uploads klasörü oluşturur ortam.ContentRootPath ise yolun tamamı
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
            veritabani.SaveChanges();

            // başarı mesajıyla sayfayı tekrar göster
            ViewBag.Mesaj = "Belge başariyla yüklendi";
            return View();
        }
    }
}