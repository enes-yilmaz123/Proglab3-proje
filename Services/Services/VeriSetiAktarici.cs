using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;

namespace StajYonetimSistemi.Services
{
    // admin in yüklediği csv veri setini okuyup öğrencileri Students tablosuna yazar
    public class VeriSetiAktarici
    {
        private readonly StajDbContext _context; // veri tabanına erişim için

        public VeriSetiAktarici(StajDbContext context)
        {
            _context = context;
        }

        // csv dosyasını okur, sonucu bir mesaj olarak döndürür
        public string Aktar(Stream dosya)
        {
            int eklenen = 0;
            int atlanan = 0;

            using var okuyucu = new StreamReader(dosya);
            okuyucu.ReadLine(); // ilk satır başlık olduğu için okuyup atlıyoruz

            string? satir;
            while ((satir = okuyucu.ReadLine()) != null) // dosya bitene kadar satır satır okur
            {
                if (string.IsNullOrWhiteSpace(satir))
                {
                    continue; // boş satırı atla
                }

                string[] alan = satir.Split(';'); // satırı noktalı virgüllerden parçalar
                if (alan.Length < 12)
                {
                    atlanan++; // eksik sütunlu satırı atla
                    continue;
                }
            }

            return $"{eklenen} öğrenci eklendi, {atlanan} satır atlandı";
        }
    }
}