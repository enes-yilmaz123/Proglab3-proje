//burası öğrencilerin tutulucağü ve user ın alt ı olarak yazılacak cs dosyası

// öğranci numarasını string tutuyoruz çünkü herhangi bir toplama bölem gibi işlemler yapılmayacak sadece karşılaştırma amaçlı

namespace StajYonetimSistemi.Models
{
    public class Student : User
    {
        public string FirstName {get ; set;} = string.Empty;  
        public string LastName {get ; set;} = string.Empty;  
        public string StudentNumber {get ; set;} = string.Empty;  
        public string TcKimlikNo { get; set; } = string.Empty;       //string çünkü hesap yapılmıyor
        public string Uyruk { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public string Address { get; set; } = string.Empty;          // ikametgâh adresi
        public string Department { get; set; } = string.Empty;      // okuduğu bölüm
        public string Ek16TeslimYili { get; set; } = string.Empty;
        public int StajGunu { get; set; }  // daha önce yaptığı staj günü
        public string PhoneNumber { get; set; } = string.Empty;
        public bool SaglikYaridimiVarMi { get; set; } 
    }
}