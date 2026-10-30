//projede başarısız  giriş denemelerinin de loglanması isteniyor o yüzden bir entity oluşturuyoruz
//direkt userın mirası olarak kullanamıyoruz çünkü data basede kullanıcı olarak tutuluyor loglar bir kullanıcı değil kullanıcının olduğu için 

namespace StajYonetimSistemi.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;   // denenen e-posta 
        public DateTime AttemptTime { get; set; } = DateTime.Now;
        public bool IsSuccess { get; set; }                 // giriş başarılı mı
    }
}
// eğerki sonlarına string.Empty eklemezsek çalışırken uyarı fırlatıyor nullable ile ilgili
// date time de de geçerli 