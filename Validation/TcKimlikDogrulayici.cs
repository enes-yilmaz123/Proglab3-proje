namespace StajYonetimSistemi.Validation;

// T.C. uyruklu öğrenciler için kimlik kontrolü burda yapilir
// KimlikDogrulayici'dan türer TemelKontrol'ü hazır kullanır kalıtım
public class TcKimlikDogrulayici : KimlikDogrulayici
{
    public override Sonuc Dogrula(string deger)
    // kural sınıfındaki abstract Dogrula yı override T.C.'ye özel kuralla dolduruyor 
    {
        // önce iki uyruk için de ortak olan kontrol KimlikDogrulayici sinifindan geliyor
        var hata = TemelKontrol(deger); //eger TemelKontrolden bir metin gelirse hata degiskenine atanır
        if (hata != null)
        {
            return new Sonuc(false, hata);
        }

        // sadece T.C.'ye özel kural ilk hanenin 0 olmasi
        if (deger[0] == '0')
        {
            return new Sonuc(false, "ilk hane 0 olamaz");
        }

        return new Sonuc(true, "Geçerli");
    }
}