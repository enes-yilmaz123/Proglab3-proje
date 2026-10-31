using Microsoft.AspNetCore.Mvc; // controller çalışması için 
using StajYonetimSistemi.Data;  //context oluşturmak için ef core
using StajYonetimSistemi.Services;     // hashlemek için 
using StajYonetimSistemi.ViewModels;  
using StajYonetimSistemi.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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


        [HttpPost] // form gönderilince çalışıcak metod
        public async Task<IActionResult> Login(LoginViewModel formInfo)// burayı form verilerini ASP.net core dolduruyor ve değişkenin içine atıyor bizde onları erkana veriyorz
        {            
            //cookie kullanabilmek için fonksiyonumuzu async olarak değiştiriyoruz çünkü kütüphaneden gelen bir zorunluluk bize bitmemiş bir iş döndürüyor

            // kullanıcı formu eksik yada hatalı doldurursa tekrar göndericke fonksiyon 
            // ModelState formdan alan bilgileri doğrulama durumu oluyor Isvalid de aralarından biri false ise false döndürüyor 
            // hata mesajınıda ModelState ye atar 
            if (!ModelState.IsValid)
            {
                return View(formInfo);// kullanıcıya aynı sayfayı kendi yazdığı verilerle tekrar gösterir form info zaten loginviewmodel olduğu için 
            }

            // data base de user tablosundaki e maili eşleşen ilk maili bulur yoksa null döner
            var user = _context.Users.FirstOrDefault(u => u.Email == formInfo.Email);

            // kullanıcı yoksa
            if (user == null)
            {
                _context.LoginLogs.Add(new LoginLog { Email = formInfo.Email, IsSuccess = false }); // yanlış giriş denemmesi logu kaydedioyoruz
                _context.SaveChanges(); // veri tabanını günceller bu olmazsa kaydetmez 

                ModelState.AddModelError("", "E-posta veya şifre hatalı"); // hata fırlat
                return View(formInfo);// tekar hatalı şifre ve e posta ile tekrar formu gönderiyoruz
            }
            if (user.IsLocked) //kullanıcı  kilitliyse
            {
                _context.LoginLogs.Add(new LoginLog { Email = formInfo.Email , IsSuccess = false}); //log oluşturur
                _context.SaveChanges();

                ModelState.AddModelError("", "Hesabınız kilitlendi yönetici ile iletişime geçin!!"); //error mesajı fırlatır
                return View(formInfo);
            }
            bool sifreDogruMu = _passwordService.Verify(user.Password , formInfo.Password);
            if (!sifreDogruMu)
            {
                user.FailAttempts ++; //deneme başarısız olursa sayacı arttır 5 e ulaşırsa da kilitle 
                if(user.FailAttempts >= 5)
                {
                    user.IsLocked = true;
                }
                _context.LoginLogs.Add(new LoginLog {Email = formInfo.Email , IsSuccess = false}); // log oluşturur
                _context.SaveChanges();

                ModelState.AddModelError("", "E-posta veya şifre hatalı"); // hata fırlat
                return View(formInfo);// tekar hatalı şifre ve e posta ile tekrar formu gönderiyoruz
            }
            
            // buradan sonra sadece başarılı olduğu durum kalıyor 
            user.FailAttempts = 0;
            _context.LoginLogs.Add(new LoginLog { Email = formInfo.Email , IsSuccess = true});
            _context.SaveChanges();

            string rol; // rol için değişken oluşturuyoruz ki cookie ye atabilelim
            if(user is Admin)
                {rol = "ADMIN_ROLU";}
            else 
                {rol = "OGRENCI_ROLU";}


            var claims = new List<Claim>   // cookie ye yazılacak bilgileri atıyoruz
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, rol)
            };

            var kimlik = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //yukarıdaki kimlik bilgilerini tek bir nesneye atar

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(kimlik));
            //cookie yi oluşturuyoruz async kullanmamızın sebebi burada await kullanmak zorunda olduğumuz için işlemi bitirmeden devam edilebilmesini sağlıyor
            //bu satır çalışınca tarayıcıya bir cookie gider ve her istekte tekrar tekerar o gönderilir

            return RedirectToAction("Index", "Home"); 
            // giriş yapılınca ana sayfaya gönder
        }

    }
}