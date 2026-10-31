namespace StajYonetimSistemi.Validation;

// kimlik numarası kurallarının ortak ana sınıfı 
// kural sınıfından türer TC ve yabancı doğrulayıcılar da bu sınıftan türer kalıtım 
// abstract doğrudan nesnesi üretilmez dogrula metodunu alt sınıflar yazar
public abstract class KimlikDogrulayici : Kural
{
    // iki uyruk için de aynı olan kontroller burada yapılır 
    protected string? TemelKontrol(string kimlik)
    // protected sadece bu sınıf ve alt sınıfları çağırabilir dışarıdan çağrılamaz kapsülleme 
    // hata varsa hata metni, hata yoksa null döner string? null dönebilcegini gösterir
    {
        // uzunluk 11 değilse hata
        if (kimlik.Length != 11) //kimlik.Length girilenin uzunlugunu verir
        {
            return "11 haneli olmali";
        }

        // karakterlerin hepsi rakam değilse hata
        if (!kimlik.All(char.IsDigit)) //bu satır kelimenin hepsinin rakam olup olamadıgını kontrol eder
        {
            return "sadece rakam olmali";
        }

        return null;
    }
}