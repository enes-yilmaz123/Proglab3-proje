namespace StajYonetimSistemi.Services
{
    // bütün belge okuyucuların ortak ana sınıfı 
    public abstract class BelgeOkuyucu
    {
        // dosya yolunu alır belgedeki başlık ve değer çiftlerini döndürür
        // nasıl okunacağını her alt sınıf kendi formatına göre yazacak
        public abstract Dictionary<string, string> Oku(string dosyaYolu);
    }
}