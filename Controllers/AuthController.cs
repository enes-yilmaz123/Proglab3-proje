using Microsoft.AspNetCore.Mvc; // controller çalışması için 
using StajYonetimSistemi.Data;  //context oluşturmak için ef core
using StajYonetimSistemi.Services;     // hashlemek için 
using StajYonetimSistemi.ViewModels; // 

//bu sayfa kullanıcının girdiği bilgileri data basedekilerle karşılaştırıcak olan controller için 

namespace StajYonetimSistemi.Controllers
{
    public class AuthController : Controller
    {
        private readonly StajDbContext _context; // bu mesajın nerede aranacağını taşıyan değişken sadece oluşturulurken yazılıyor 
        private readonly PasswordService _passwordService; // bu da karşılaştırılak olan şifrenin taşındığı değişken 

        // EF core programcs deki ayarlarımıza göre otomatik olarak doldurur bu bilgileri 
        public AuthController(StajDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        
        [HttpGet] // tarayıcının sunucudan bilgi isterse verir fakat bilgi gönderirse lamaz
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }// boş bir giriş sayfası fırlatır

    }
}