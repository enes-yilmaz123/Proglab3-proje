using StajYonetimSistemi.Models;

namespace StajYonetimSistemi.ViewModels
{
    // öğrencinin Belgelerim sayfasındaki tek bir satırın verisi
    public class BelgeSatiriViewModel
    {
        public int Id { get; set; }
        public string DosyaAdi { get; set; } = string.Empty;
        public DateTime YuklemeTarihi { get; set; }
        public DocumentStatus Durum { get; set; }
        public string Rapor { get; set; } = string.Empty; // kontrol satırları, \n ile ayrılı

        // ekranda gösterilecek Türkçe durum yazısı
        public string DurumMetni => Durum switch
        {
            DocumentStatus.Yuklendi => "Yüklendi",
            DocumentStatus.Inceleniyor => "İnceleniyor",
            DocumentStatus.HataliEksik => "Hatalı / Eksik",
            DocumentStatus.Onaylandi => "Onaylandı",
            DocumentStatus.Tamamlandi => "Tamamlandı",
            _ => Durum.ToString()
        };

        // raporu satır satır liste yapar, sayfada <li> olarak göstermek kolay olsun diye
        public List<string> RaporSatirlari => Rapor
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }

    // sayfaya giden ana model: öğrencinin tüm belgeleri
    public class BelgelerimViewModel
    {
        public List<BelgeSatiriViewModel> Belgeler { get; set; } = new();
    }
}