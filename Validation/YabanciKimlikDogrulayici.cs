namespace StajYonetimSistemi.Validation;

// yabancı uyruklu öğrenciler için kimlik kontrolü
// bu sınıfta KimlikDogrulayici'dan türer TemelKontrol'ü hazır kullanır kalıtım
public class YabanciKimlikDogrulayici : KimlikDogrulayici
{
    public override Sonuc Dogrula(string deger)
    // Dogrula'yı override ile yabancı kimliğe özel kuralla dolduruyor 
    // aynı isimli metot TC sınıfından farklı bir şekilde çalışıyor
    {
        // ortak kontrol 
        var hata = TemelKontrol(deger);
        if (hata != null)
        {
            return new Sonuc(false, hata);
        }

        // sadece yabancı kimliğe özel kural
        if (!deger.StartsWith("99")) //bu satır girilen degerin 99 ile başlamasına bakar
        {
            return new Sonuc(false, "yabanci kimlik no 99 ile başlamali");
        }

        return new Sonuc(true, "Geçerli");
    }
}