using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using System.Globalization;

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
                
                string email = alan[9].Trim(); // boşlukları siler
                string ogrenciNo = alan[3].Trim();

                // yazıyı tarihe çevirmeey çalışşır başarırsa true döner
                bool dogumDogruMu = DateTime.TryParseExact(alan[4].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dogumTarihi);
                bool gunDogruMu = int.TryParse(alan[8].Trim(), out int stajGunu);

                // aynı eposta ya da öğrenci numarası tabloda zaten varsa tekrar eklenmez
                bool zatenVar = _context.Users.Any(u => u.Email == email) || _context.Students.Any(s => s.StudentNumber == ogrenciNo);

                if (!dogumDogruMu || !gunDogruMu || email == "" || zatenVar)
                {
                    atlanan++;
                    continue;
                }

                // ad soyad tek sütunda son boşluğa kadar olan kısım ad, sonrası soyad
                string adSoyad = alan[0].Trim();
                int sonBosluk = adSoyad.LastIndexOf(' ');
                string ad = sonBosluk > 0 ? adSoyad.Substring(0, sonBosluk) : adSoyad; // iki isimli öğrenciler için son boşluğa bakılıp ikinci ismin soyada kaymasını önler
                string soyad = sonBosluk > 0 ? adSoyad.Substring(sonBosluk + 1) : "";

                _context.Students.Add(new Student
                {
                    FirstName = ad,
                    LastName = soyad,
                    TcKimlikNo = alan[1].Trim(),
                    Uyruk = alan[2].Trim(),
                    StudentNumber = ogrenciNo,
                    DogumTarihi = dogumTarihi,
                    Address = alan[5].Trim(),
                    Department = alan[6].Trim(),
                    Ek16TeslimYili = alan[7].Trim(),
                    StajGunu = stajGunu,
                    Email = email,
                    PhoneNumber = alan[10].Trim(),
                    SaglikYaridimiVarMi = alan[11].Trim().Equals("Evet", StringComparison.OrdinalIgnoreCase)
                });
                _context.SaveChanges(); // her satırı hemen kaydediyoruz ki dosyadaki tekrar eden satırı bir sonraki kontrol görebilsin
                eklenen++;
            }

            return $"{eklenen} öğrenci eklendi, {atlanan} satır atlandı";
        }
    }
}