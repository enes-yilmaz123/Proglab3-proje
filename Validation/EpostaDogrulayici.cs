namespace StajYonetimSistemi.Validation;

// e-posta adresinin biçimini kontrol eder 
public class EpostaDogrulayici : Kural
// Kural sınıfından türer kalıtım
{
    public override Sonuc Dogrula(string deger)
    // Kural'daki abstract Dogrula'yı override ile e-posta kuralıyla doldurur 
    {
        if (deger.Contains(' ')) // boşluk varsa geçersiz
        {
            return new Sonuc(false, "e-posta bosluk iceremez");
        }

        int at = deger.IndexOf('@');   // @ nin yerini döndürür yoksa -1 döndürür
        if (at <= 0)  // @ yoksa ya da en başta ise geçersiz
        {
            return new Sonuc(false, "e-posta @ işaretinden önce karakter icermeli");
        }

        if (deger.IndexOf('@', at + 1) != -1)  // ikinci bir @ varsa geçersiz 
        {
            return new Sonuc(false, "e-posta tek bir @ icermeli");
        }

        int nokta = deger.LastIndexOf('.'); // son noktanın sırasını döndürür

        if (nokta < at + 2 || nokta == deger.Length - 1) // nokta @ den en az 2 sıra sonra olmalı ve en sonda olmamalı 
        {
            return new Sonuc(false, "e-posta ad@alanadi.uzanti formatinda olmali");
        }

        return new Sonuc(true, "Gecerli");
    }
}