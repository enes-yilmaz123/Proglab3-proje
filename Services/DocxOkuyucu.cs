using DocumentFormat.OpenXml.Packaging; // docx dosyasını açmak için
using DocumentFormat.OpenXml.Wordprocessing; // tablo, satır, hücre gibi parçalar için

namespace StajYonetimSistemi.Services
{
    // docx belgesindeki tabloları okuyup başlık ve değer sözlüğü olarak veren sınıf
    public class DocxOkuyucu : BelgeOkuyucu
    // BelgeOkuyucu dan türetiyoruz kalıtım
    {
        // ana sınıftaki Oku metodunu docx'e göre dolduruyoruz çok biçimlilik
        public override Dictionary<string, string> Oku(string dosyaYolu) // docx dosyasının yolunu alır belgedeki başlık ve değer çiftlerini sözlük olarak döndürür
        {
            var alanlar = new Dictionary<string, string>(); // başlık ve deger çiftlerini alanlar da tutuyoruz

            using (var belge = WordprocessingDocument.Open(dosyaYolu, false)) // docx dosyasını sadece okumak için açıyoruz false değişiklik yapma demek
            {
                var govde = belge.MainDocumentPart!.Document!.Body!; // belgenin ana içerigini burda tutuyoruz sondaki ! ler boş gelemez anlamına gelir

                foreach (var tablo in govde.Elements<Table>())  // belgedeki tabloları geziyoruz
                {
                    foreach (var satir in tablo.Elements<TableRow>()) // satırları sırasıyla geziyoruz
                    {
                        // hücrelerin yazılarını alıp baştaki ve sondaki boşlukları temizliyoruz trim ile
                        var hucreler = satir.Elements<TableCell>().Select(h => h.InnerText.Trim()).ToList();

                        // hücreler başlık değer başlık değer sırasında olduğu için ikişer olarak tutuyoruz
                        for (int i = 0; i + 1 < hucreler.Count; i += 2)
                        {
                            string baslik = hucreler[i];
                            string deger = hucreler[i + 1];

                            // başlığı boş olan hücreleri atlıyoruz
                            if (baslik != "")
                            {
                                alanlar[baslik] = deger; // ve alanlar sözlüğüne atıyoruz
                            }
                        }
                    }
                }
            }

            return alanlar;
        }
    }
}