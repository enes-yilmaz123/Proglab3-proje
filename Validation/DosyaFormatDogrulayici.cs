namespace StajYonetimSistemi.Validation;

// yüklenen dosyanın uzantısını kontrol eden kural
public class DosyaFormatDogrulayici : Kural
// Kural sınıfından türer kalıtım
{
    // sadece bu sınıfın içinden erişilip dışardan değiştirilmemesi için private kapsülleme
    private static readonly string[] KabulEdilenUzantilar = { ".doc", ".docx", ".pdf", ".jpg", ".jpeg", ".png" };
    // sınıfın içinde kabul edilen uzantılari belirledik dışardan nesne olusturulmaz bunun icin static, kabul edilen uzantıların degistirilmemesi icinse readonly kullandık
    public override Sonuc Dogrula(string deger)
    // Kural'daki abstract Dogrula'yı override ile dosya kuralıyla doldurur
    {
        string uzanti = Path.GetExtension(deger).ToLowerInvariant(); //eger baglanti büyük harfli yazıldıysa kabul edilmesi icin kucuk harfe cevrilir getextension ile uzantı bilgisi alınır

        if (!KabulEdilenUzantilar.Contains(uzanti))
        // uzantı kabul listesinde yoksa reddedilir
        {
            return new Sonuc(false, "yalnizca .doc, .docx, .pdf, .jpg, .jpeg ve .png dosyalari yuklenebilir");
        }

        return new Sonuc(true, "Gecerli");
    }
}