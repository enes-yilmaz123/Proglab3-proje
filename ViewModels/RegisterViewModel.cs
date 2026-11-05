using System.ComponentModel.DataAnnotations; // required için gerekli 

// view modeller formdan gelen verileri tutan basit modeller
namespace StajYonetimSistemi.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim zorunludur")]
        [RegularExpression(@"^[A-Za-zÇĞİÖŞÜçğıöşü]+([ '\-][A-Za-zÇĞİÖŞÜçğıöşü]+)*$", ErrorMessage = "İsim sadece harflerden oluşmalı, başında ya da sonunda boşluk olamaz")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad zorunludur")]
        [RegularExpression(@"^[A-Za-zÇĞİÖŞÜçğıöşü]+([ '\-][A-Za-zÇĞİÖŞÜçğıöşü]+)*$", ErrorMessage = "Soyad sadece harflerden oluşmalı, başında ya da sonunda boşluk olamaz")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci numarası zorunludur")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin")] // email adresinin biçimi kontrol eder
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "E-posta adresi ornek@alanadi.com biçiminde olmalı")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalı")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermeli")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")] // yanlış şifre belirlenmemesi için doğrulama
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")] 
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}