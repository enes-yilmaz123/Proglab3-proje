using System.ComponentModel.DataAnnotations; // [Required] kullanabilmek için eklediğimiz bir kütüphane

namespace StajYonetimSistemi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta zorunludur")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Password { get; set; } = string.Empty;
    }
}
//nullable olmadıkları için string.Empty yazdık yoksa uyarı fırlatıyor

//form verilerinin boş gönderilmemesi için [required] eklendi eğerki boş bırakılırsa error mesajı fırlatılıyor 