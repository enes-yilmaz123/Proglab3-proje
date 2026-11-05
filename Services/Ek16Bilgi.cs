namespace StajYonetimSistemi.Services
{
    // belgeden okunan bilgileri düzenli şekilde tutan sınıf
    public class Ek16Bilgi
    {
        public string AdSoyad { get; set; } = ""; // get ile degeri okuyup set ile degeri yazarız başlangıç degeri boş atıyoruz null uyarısı vermemesi icin
        public string TcNo { get; set; } = "";
        public string Uyruk { get; set; } = "";
        public string OgrenciNo { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string Eposta { get; set; } = "";
    }
}