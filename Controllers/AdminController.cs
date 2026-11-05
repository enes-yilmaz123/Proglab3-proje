using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajYonetimSistemi.Data;
using StajYonetimSistemi.Models;
using StajYonetimSistemi.ViewModels;

namespace StajYonetimSistemi.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class AdminController : Controller
    {
        private readonly StajDbContext _context; // veri tabanına erişim için
        public AdminController(StajDbContext context) // program cs deki kuralları controlerı oluturuken atııyor
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ozet = new AdminOzetViewModel // tablodaki sutun sayılarını çeker tek tek öğrencileri çekip saymaz
            {
                OgrenciSayisi = _context.Students.Count(),
                ToplamBelge = _context.Documents.Count(),
                HataliEksik = _context.Documents.Count(d => d.Status == DocumentStatus.HataliEksik),
                Tamamlanan = _context.Documents.Count(d => d.Status == DocumentStatus.Tamamlandi)
            };
            return View(ozet);
        }
    }
}