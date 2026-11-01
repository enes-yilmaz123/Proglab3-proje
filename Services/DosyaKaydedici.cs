namespace StajYonetimSistemi.Services
{
    // öğrencinin yüklediği dosyayı sunucudaki bir klasöre kaydeder.
    public class DosyaKaydedici
    {
        private readonly string kayitKlasoru;
        // klasör bilgisi private alanda tutulur dışarıdan değiştirilemez kapsülleme
        public DosyaKaydedici(string klasor)
        {
            kayitKlasoru = klasor;
        }
        public string Kaydet(IFormFile dosya) //IFormFile yüklenen dosyayi temsil eder sunucuda geçiçi olarak tutulur
        {
            
            Directory.CreateDirectory(kayitKlasoru); // klasör yoksa klasör açar varsa bir şey yapmaz

            string uzanti = Path.GetExtension(dosya.FileName).ToLowerInvariant(); //dosyanin isminden uzantiyi alir kücük harflere çevirir
            string yeniAd = Guid.NewGuid() + uzanti; //dosyanin ismini çakışma olmaması için rastgele bir isme çevirir yanına uzantısını ekler
            string tamYol = Path.Combine(kayitKlasoru, yeniAd); //dosyanin adresini oluşturur işletim sistemlerinde adres arasındaki sembol degistigi için otomatik olarak kendisi koyar

            
            using (var akis = new FileStream(tamYol, FileMode.Create)) //tamYol adresinde yeni bir dosya açar FileMode.Create ile dosya oluşturulur coptTo ile geçici olarak yüklenen dosya buraya kopyalanır
            {
                dosya.CopyTo(akis);
            }

            return yeniAd;
        }
    }
}