namespace StajYonetimSistemi.Validation;

// herhangi doğrulama kuralının sonucunu taşıyan sınıf
public class Sonuc
{
    public bool Gecerli { get; }
    //{ get; } sadece okunabilir yapar dışarıdan değiştirilemez bu da kapsülleme 
    // raporda ve arayüzde kullanıcıya doğrudan gösterilir
    public string Mesaj { get; }
    // geçerliyse "Geçerli", değilse neyin yanlış olduğunu anlatan hata metnini tutar
     public string? Uyari { get; } 
     // geçerli durumda kullanıcının bilmesi gereken durumu döndürür yoksa null döner string? bunun için

    public Sonuc(bool gecerli, string mesaj, string? uyari = null)
    // kurucu metot new Sonuc(true, "Geçerli") yazılınca değerleri alanlara koyar
    // değerler sadece burada atandığı için nesne oluştuktan sonra değişmez
    {
        Gecerli = gecerli;
        Mesaj = mesaj;
        Uyari = uyari;
    }
}