namespace StajYonetimSistemi.Validation;

// staj yapılan yerdeki mühendis sayısını kontrol eder 
// mühendisin mezun olduğu bölümün sorulmasını sağlar
public class MuhendisSayisiDogrulayici : Kural
// Kural sınıfından türer kalıtım
{
    // Kural'daki abstract Dogrula'yı override ile bu kuralla doldurur 
    public override Sonuc Dogrula(string deger)
    {
        // metin sayıya çevrilemiyorsa hata döndürür
        if (!int.TryParse(deger, out int sayi)) // TryParse ile metni sayıya çeviriyoruz sayi degiskenine atıyoruz out ile ise hem true false hem sayinin kendisinin çıktısını alıyoruz
        {
            return new Sonuc(false, "muhendis sayisi sayi olmali");
        }

        // 0 ve negatif sayılar geçersiz en az 1 olmalı
        if (sayi < 1)
        {
            return new Sonuc(false, "muhendis sayisi en az 1 olmali");
        }

        // 1-4 arası geçerli ama uyarı var mezun olunan bölüm sorulacak
        if (sayi < 5)
        {
            return new Sonuc(true, "Gecerli", "muhendis sayisi 5'in altinda, muhendisin mezun olduğu bolumu belirtin");
        }

        return new Sonuc(true, "Gecerli");
    }
}