using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using StajYonetimSistemi.ViewModels;
using StajYonetimSistemi.Services;

namespace StajYonetimSistemi.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class AdminController : Controller
    {
        private readonly StajDbContext _context; // veri tabanına erişim için
        private readonly VeriSetiAktarici _aktarici; // csv veri setini okuyup öğrencileri kaydeden servis
        public AdminController(StajDbContext context, VeriSetiAktarici aktarici) // program cs deki kuralları controlerı oluturuken atııyor
        {
            _context = context;
            _aktarici = aktarici;
        }        
        public IActionResult Index()
        {
            var ozet = new AdminOzetViewModel // tablodaki sutun sayılarını çeker tek tek öğrencileri çekip saymaz
            {
                OgrenciSayisi = _context.Students.Count(),
                ToplamBelge = _context.Documents.Count(),
                HataliEksik = _context.Documents.Count(d => d.Status == DocumentStatus.HataliEksik),
                Tamamlanan = _context.Documents.Count(d => d.Status == DocumentStatus.Tamamlandi)
            };
            return View(ozet);
        }

        // başvuru listesi: duruma göre sekme ve öğrenci adı ya da numarasına göre arama
        public IActionResult Basvurular(DocumentStatus? durum, string? arama)
        {
            // belgeleri al sekme seçildiyse sadece o durumdakileri bırak
            IQueryable<Document> belgeler = _context.Documents;
            if (durum != null)
            {
                belgeler = belgeler.Where(d => d.Status == durum);
            }

            //her belge için sahibi olan öğrenciyi bulup tablo satırı hazırla
            var satirlar = new List<BasvuruSatiriViewModel>();
            var ogrenciler = _context.Students.ToList(); // öğrencileri döngüden önce bir kez çekiyoruz her belge için tekrar sorgulamıyoruz
            foreach (var belge in belgeler.OrderByDescending(d => d.UploadDate).ToList())
            {
                var ogrenci = ogrenciler.FirstOrDefault(s => s.id == belge.StudentId); 
                //belgede sadece öğrenci numarası var idsi bu numaraya eşit olan ilk öğrenciyi döndürür
                if (ogrenci == null)
                {
                    continue; // öğrencisi bulunamayan belgeyi atla
                }

                string adSoyad = ogrenci.FirstName + " " + ogrenci.LastName; //format düzenler

                //arama yazıldıysa ad soyad ya da numara uymayan satırı atla
                if (!string.IsNullOrWhiteSpace(arama))
                {
                    string aranan = arama.Trim(); // başındaki ve sonundaki boşlukları siler
                    if (!adSoyad.Contains(aranan) && !ogrenci.StudentNumber.Contains(aranan)) //ad soyad veya numara uyuşuyormu diye test eder
                    {
                        continue;
                    }
                }

                satirlar.Add(new BasvuruSatiriViewModel // sahini eşlenen belgenin verileri atıyoruz
                {
                    Id = belge.id,
                    OgrenciAdSoyad = adSoyad,
                    OgrenciNo = ogrenci.StudentNumber,
                    DosyaAdi = belge.OriginalFileName,
                    YuklemeTarihi = belge.UploadDate,
                    Durum = belge.Status,
                    Rapor = belge.Rapor
                });
            }

            //satıralrı seçili sekmeyi ve arama yazısını sayfaya gönder
            var model = new BasvurularViewModel
            {
                Satirlar = satirlar,
                SecilenDurum = durum,
                Arama = arama
            };
            return View(model);
        }

        // veri seti yükleme sayfası şimdilik sadece arayüz
        public IActionResult VeriSeti()
        {
            return View();
        }

        [HttpPost] // durum değiştirme formu gönderilince çalışır
        public IActionResult DurumDegistir(int id, DocumentStatus yeniDurum) // değişecek olan belggenin id si ve yeni durum girdi olarak girilir
        {
            var belge = _context.Documents.FirstOrDefault(d => d.id == id); // belgeyi id ile bul
            if (belge == null)
            {
                return NotFound(); // böyle bir belge yoksa 404 döndür
            }

            belge.Status = yeniDurum;
            _context.SaveChanges(); // değişikliği veri tabanına yazar

            return RedirectToAction("Basvurular"); // listeye geri dön
        }

        [HttpPost] // silme formu gönderilince çalışır
        public IActionResult Sil(int id)
        {
            var belge = _context.Documents.FirstOrDefault(d => d.id == id); // belgeyi id ile bul
            if (belge == null)
            {
                return NotFound(); // böyle bir belge yoksa 404 döndür
            }

            _context.Documents.Remove(belge); // belgeyi silinecekler arasına işaretler
            _context.SaveChanges(); // veri tabanından gerçekten siler

            return RedirectToAction("Basvurular"); // listeye geri dön
        }
    }
}