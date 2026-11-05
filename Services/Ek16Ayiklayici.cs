namespace StajYonetimSistemi.Services
{
    // okuyucudan gelen sözlüğü alıp Ek16Bilgi nesnesine çeviren sınıf
    public class Ek16Ayiklayici
    {
        public Ek16Bilgi Ayikla(Dictionary<string, string> alanlar) //içeriye sözlük alıp dışarıya Ek16Bilgi nesnesi veriyoruz
        {
            var bilgi = new Ek16Bilgi(); // boş formu açıyoruz

            bilgi.AdSoyad = DegerAl(alanlar, "ADI SOYADI");
            bilgi.OgrenciNo = DegerAl(alanlar, "ÖĞRENCİ NUMARASI");

            // tc kimlik ve uyruk aynı hücrede bulundugu için ayırıyoruz
            string[] kimlik = DegerAl(alanlar, "T.C. KİMLİK NO/ UYRUĞU").Split('/'); //trim ile boslukları atıp eger deger sayısı 1 den coksa 2. degeri alıyoruz degilse de bos bırakıyoruz
            bilgi.TcNo = kimlik[0].Trim();
            bilgi.Uyruk = kimlik.Length > 1 ? kimlik[1].Trim() : "";

            // telefon ve e-posta aynı hücrede oldugu icin bunlari da ayırıyoruz
            string[] iletisim = DegerAl(alanlar, "E-POSTA ADRESİ /TELEFON NO").Split('/');
            bilgi.Telefon = iletisim[0].Trim();
            bilgi.Eposta = iletisim.Length > 1 ? iletisim[1].Trim() : "";

            return bilgi; // doldurulmuş formu geri veriyoruz
        }

        private string DegerAl(Dictionary<string, string> alanlar, string baslik)
        // sözlükte o başlık varsa değeri verir, yoksa boş yazı verir 
        {
            return alanlar.TryGetValue(baslik, out string? deger) ? deger : "";
        }
    }
}