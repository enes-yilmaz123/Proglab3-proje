using Microsoft.EntityFrameworkCore; // ef core kütüphanesini ekler 
using StajYonetimSistemi.Models; //models deki entityleri kullanmak için using ekiyoruz 

namespace StajYonetimSistemi.Data
{
    public class StajDbContext : DbContext //data base bağlamak için miras alıyoruz
    {
        // yazdığımız modellri veri tabanındaki listelere dönüştürmek için ef core hallediyor
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Document> Documents { get; set; }

        // bu sınıftan bir nesne oluşturulduğunda ilk çalışıcak olan yapıcı metod 
        // bizim yazdığımız ayarları miraas aldığı classa yani DbContext e taşıyacak 
        // Sql kullanıcağımız gibi ayarlar
        public StajDbContext(DbContextOptions<StajDbContext> options) : base(options)
        {
        }
    }
}