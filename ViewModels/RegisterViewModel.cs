using System.ComponentModel.DataAnnotations; // required için gerekli 

// view modeller formdan gelen verileri tutan basit modeller
namespace StajYonetimSistemi.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim zorunludur")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci numarası zorunludur")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin")] // email adresinin biçimi kontrol eder
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")] // yanlış şifre belirlenmemesi için doğrulama
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")] 
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}