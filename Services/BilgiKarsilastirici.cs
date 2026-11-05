using System.Globalization; // tr-TR kültürünü kullanmak için
using StajYonetimSistemi.Models; // Student sınıfını kullanmak için

namespace StajYonetimSistemi.Services
{
    // belgeden okunan bilgi ile kayıtlı öğrencinin bilgisini karşılaştırıp uyuşmayanları listeleyen sınıf
    public class BilgiKarsilastirici
    {
        public List<string> Karsilastir(Ek16Bilgi bilgi, Student ogrenci) // belgedeki bilgiyi ve kayıtlı öğrenciyi alıp uyuşmayanların listesini döndürür
        {
            var farklar = new List<string>(); // uyuşmayan alanları burada tutuyoruz liste boş kalırsa her şey uyuşuyor demek

            // ad soyad kayıtta ikiye bölünmüş olduğu için birleştirip karşılaştırıyoruz
            string kayitliAdSoyad = ogrenci.FirstName + " " + ogrenci.LastName;
            if (!AdSoyadAyniMi(bilgi.AdSoyad, kayitliAdSoyad)) // uyuşmuyorsa farklar listesine yazıyoruz
            {
                farklar.Add("Ad soyad uyuşmuyor: belgede \"" + bilgi.AdSoyad + "\", kayıtta \"" + kayitliAdSoyad + "\"");
            }

            // öğrenci numarasını karşılaştırıyoruz
            if (!AyniMi(bilgi.OgrenciNo, ogrenci.StudentNumber))
            {
                farklar.Add("Öğrenci numarası uyuşmuyor: belgede \"" + bilgi.OgrenciNo + "\", kayıtta \"" + ogrenci.StudentNumber + "\"");
            }

            // e-postayı karşılaştırıyoruz
            if (!AyniMi(bilgi.Eposta, ogrenci.Email))
            {
                farklar.Add("E-posta uyuşmuyor: belgede \"" + bilgi.Eposta + "\", kayıtta \"" + ogrenci.Email + "\"");
            }

            return farklar; // boş liste geliyorsa uyuşmazlık yok
        }

        // iki yazıyı baştaki sondaki boşluğa ve büyük küçük harfe bakmadan karşılaştırır numara ve e-posta için kullanıyoruz
        private bool AyniMi(string a, string b)
        {
            return string.Equals(a.Trim(), b.Trim(), StringComparison.OrdinalIgnoreCase); // OrdinalIgnoreCase büyük küçük harf farkını yok sayar
        }

        // ad soyadı kelimelere bölüp sıralayarak karşılaştırır kelimelerin sırası fark etmez üç isimli öğrenciler için de çalışır
        private bool AdSoyadAyniMi(string belgedeki, string kayitli)
        {
            var tr = new CultureInfo("tr-TR"); // i ve İ harflerini doğru çevirsin diye türkçe kültürü kullanıyoruz
            var a = belgedeki.Trim().ToUpper(tr).Split(' ', StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x); // büyük harfe çevirip kelimelere bölüyoruz ve sıralıyoruz
            var b = kayitli.Trim().ToUpper(tr).Split(' ', StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x);
            return a.SequenceEqual(b); // iki sıralı liste birebir aynı mı diye bakar
        }
    }
}