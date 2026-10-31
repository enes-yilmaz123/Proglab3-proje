using Microsoft.AspNetCore.Mvc; // controller çalışması için 
using StajYonetimSistemi.Data;  //context oluşturmak için ef core
using StajYonetimSistemi.Services;     // hashlemek için 
using StajYonetimSistemi.ViewModels;  
using StajYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;// yetkilendirme ile alakalı
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
                {rol = "ROLE_ADMIN";}
            else 
                {rol = "ROLE_STUDENT";}


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
        [Authorize]//giriş yapmamış kullanıcıların logout metodunu çalıştırmaması lazım
        [HttpPost]// çıkış yapılınca cookie yi göndermeyi bırakır
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // az önce kullandığımız fonksiyonun out versiyonu
            return RedirectToAction("Login"); // tekrardan giriş sayfasına yönlendiriyoruz
        }
        public IActionResult AccessDenied() // erişim reddeildi  sayfası 
        {
            return StatusCode(403, "Bu sayfaya erişim yetkiniz yok");
            //front end tasarımında sayfa eklenicek buraya return view() ile 
        }

        [HttpGet] // kayıt ekranını fırlatan fonksiyon 
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost] // ekranında bilgileri alan fonksiyon
        public IActionResult Register(RegisterViewModel formInfo)
        {
            if (!ModelState.IsValid) //boşsa formu  tekrar gönder
            {
                return View(formInfo);
            }

            // aynı e-posta ile kayıt var mı
            bool emailVarMi = _context.Users.Any(u => u.Email == formInfo.Email);
            if (emailVarMi) // eğerki data base de aynı epostadan başka varsa hatalı formu tekrar gönderir ve hata mesajı fırlatır
            {
                ModelState.AddModelError("", "Bu e-posta ile zaten bir hesap var");
                return View(formInfo);
            }

            var student = new Student  //data baseye göndermek için controller içine atıyoruz bunun içinde önce boş bir nesne oluşturuyoruz
            {
                FirstName = formInfo.FirstName,
                LastName = formInfo.LastName,
                StudentNumber = formInfo.StudentNumber,
                Email = formInfo.Email,
                Password = _passwordService.Hash(formInfo.Password) // şifreyi direkt yüklemek yerine  hasleyip gönderiyoruz
            };

            _context.Students.Add(student); /// buda databaseye eklemesi için controllıra verdiğimiz kod sonrada yenileyip data baseye kaydediyoruz
            _context.SaveChanges();

            return RedirectToAction("Login"); // başarılı registerdan sonra /Auth/Login adresine yönlendirir
        }
    }
}