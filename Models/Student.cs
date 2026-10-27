//burası öğrencilerin tutulucağü ve user ın alt ı olarak yazılacak cs dosyası

// öğranci numarasını string tutuyoruz çünkü herhangi bir toplama bölem gibi işlemler yapılmayacak sadece karşılaştırma amaçlı

namespace StajYonetimSistemi.Models
{
    public class Student : User
    {
        public string FirstName {get ; set;}
        public string LastName {get ; set;}
        public string StudentNumber {get ; set;}
    }
}