using System.Text.RegularExpressions; // regex sınıfını kullanabilmek için

namespace StajYonetimSistemi.Validation;

// telefon numarasının "+90 xxx xxx xx xx" formatında olup olmadığını kontrol eden sınıf
public class TelefonDogrulayici : Kural
// Kural sınıfından türer kalıtım
{
    public override Sonuc Dogrula(string deger)
    // Kural sınıfındaki abstract Dogrula'yı override telefon kuralıyla doldurur
    {
        if (!Regex.IsMatch(deger, @"^\+90 \d{3} \d{3} \d{2} \d{2}$")) // Regex kalıbı metnin başı (^), "+90" boşluk 3 rakam boşluk 3 rakam boşluk 2 rakam boşluk 2 rakam metnin sonu ($)
        // \+90 degerin başı tam olarak +90 mı diye bakar sonrdaki \d{3} ve 2 olanlar ise boşluklardan sonra rakam sayılarını gösterir
        {
            return new Sonuc(false, "telefon +90 xxx xxx xx xx formatinda olmali");
        }

        return new Sonuc(true, "Gecerli");
    }
}