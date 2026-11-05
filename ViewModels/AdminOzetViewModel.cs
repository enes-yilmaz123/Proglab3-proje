namespace StajYonetimSistemi.ViewModels
{
    // admin özet sayfasındaki sayıları taşır 4 değişkeni tek tek yollamamak için bir model oluşturuyorz
    public class AdminOzetViewModel
    {
        public int OgrenciSayisi { get; set; }
        public int ToplamBelge { get; set; }
        public int HataliEksik { get; set; }
        public int Tamamlanan { get; set; }
    }
}
