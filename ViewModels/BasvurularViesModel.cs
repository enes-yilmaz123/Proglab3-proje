using StajYonetimSistemi.Models;

namespace StajYonetimSistemi.ViewModels
{
    // başvuru tablosundaki tek bir satırın verisi
    public class BasvuruSatiriViewModel
    {
        public int Id { get; set; }
        public string OgrenciAdSoyad { get; set; } = string.Empty;
        public string OgrenciNo { get; set; } = string.Empty;
        public string DosyaAdi { get; set; } = string.Empty;
        public DateTime YuklemeTarihi { get; set; }
        public DocumentStatus Durum { get; set; } // ekranda gösterilen Türkçe durum yazısı
        
        public string DurumMetni => Durum switch //durum neyse o durum mesajını döndürür
        {
            DocumentStatus.Yuklendi => "Yüklendi",
            DocumentStatus.Inceleniyor => "İnceleniyor",
            DocumentStatus.HataliEksik => "Hatalı / Eksik",
            DocumentStatus.Onaylandi => "Onaylandı",
            DocumentStatus.Tamamlandi => "Tamamlandı",
            _ => Durum.ToString()
        };

    }
    public class BasvurularViewModel //controllera göndermek için başvuru satırındaki bilgileri taşıyan model
    {
        public List<BasvuruSatiriViewModel> Satirlar { get; set; } = new();
        public DocumentStatus? SecilenDurum { get; set; }
        public string? Arama { get; set; } //arama kutusuna yazılan metin null olabilsin diye ? var sayfa yenilenince gitmesin diye bunu da yazıyoruz
    }
}