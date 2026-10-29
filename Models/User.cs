//burası genel userların tutulucağı ve genel kuralların olacağı cs dosyası

//id için { get; init; }​ kullanmalıyız çünkü sadece basta yaziliyor


namespace StajYonetimSistemi.Models
{
    public abstract class User // direkt olarak bu sınıftan new nesne oluşturulamamsı için abstract eklendi 
        {
            public int id { get; init; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
}
