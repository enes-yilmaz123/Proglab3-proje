using Microsoft.AspNetCore.Mvc; // controller kalıtımı için gerekli kütüphane
using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using System.Linq; //  veri tabanından veri çekerken kullanılacak fonksiyonlar için 

// bu controller data baseye erişim sağlıyacak , veri çekicek ve bize servis edicek yapı

namespace StajYonetimSistemi.Controllers
{
    public class StudentsController : Controller
    {
        // veriyi çekicek olan data baseye erişimi olan yapıyı oluşturuyoruz
        private readonly StajDbContext _context;

        // erişim sağlamak için oluşturulduğunda gerekli bilgilerin atanması (bilgilerin nerede tutulduğu gibi)
        public StudentsController(StajDbContext context)
        {
            _context = context;
        }

        // bir web isteği geldiğinde veri tabanından listeyi çeker ve html sayfasının içine koyup verir
        public IActionResult Index()
        {
            // veri tabanından student listesini çeker ve değişkene atar
            var ogrenciListesi = _context.Students.ToList();
            
            // listeyi ekrana basmak için html sayfasına atar
            return View(ogrenciListesi);
        }
    }
}