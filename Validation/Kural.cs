namespace StajYonetimSistemi.Validation;

// tüm doğrulama kurallarının ortak ana sınıfı
// kimlik, telefon, e-posta gibi her kural bu sınıftan türer kalıtım
public abstract class Kural
// abstract olduğu için nesne üretilemez sadece şablondur soyutlama örneği
{
    // verilen değeri kontrol edip sonucu döndürür.
    // hepsi aynı isimle çağrılır ama her biri farklı davranır çok biçimlilik
    public abstract Sonuc Dogrula(string deger);
    // abstract metot gövdesi yok her alt sınıf bunu kendi kuralına göre yazmak zorunda
}