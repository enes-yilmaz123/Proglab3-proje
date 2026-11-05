using Microsoft.EntityFrameworkCore; // ef core kütüphanesini ekler
using StajYonetimSistemi.Data; // data base ile bağlantıyı sağlıyan köprüyü kullanmamızı sağlar
using StajYonetimSistemi.Services; // PasswordService i kaydetmek için
using StajYonetimSistemi.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
//bir kullanıcının giriş bilgilerini karşılaşitırdıktan sonra eğerki doğru ise sunucu tarafından bir cookie göndeririz
//içinde eposta gibi bilgiler bulunur sunucu her istekde cookie ye bakar kullanıcıdan sürekli giriş verisi istemez çıkış yapana kadar saklanır


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//ne zamana data base ye birşey kaydedilecek olursa bizim yazdığımız ayarları uygulamasını sağlıyan kod 
//projeyi çalıştırdığımda ana klasörün içine staj.db adında bir dosya oluştur ve bütün tabloları o dosyanın içine yaz demek oluyor
builder.Services.AddDbContext<StajDbContext>(options => options.UseSqlite("Data Source=staj.db"));
//                                                  sql lite kullanacağımızı bildiriyoruz

// AuthController şifre hashlemek için PasswordService ister, kayıtlı değilse sayfa açılmaz
builder.Services.AddScoped<PasswordService>();


// admin sayfası yada öğrencilerin evrak yükleme sayfası gibi korumalı sayfalara girince buraya yönlendirilir
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";              // giriş yapmamışsa
    options.AccessDeniedPath = "/Auth/AccessDenied"; // giriş yapmış ama rolü uygun değilse
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())// kod ilk çalıştığında herhangi bir scope olmadığı için elle bir scope açıyoruz
{
    var db = scope.ServiceProvider.GetRequiredService<StajDbContext>();
    db.Database.Migrate(); // eksik tabloları kendisi oluşturur, dotnet ef database update gerekmez

    if (!db.Admins.Any()) // hiç admin yoksa bir tane oluştur
    {
        var sifre = app.Configuration["Admin:Sifre"]; // şifre kodda durmaz, ayar dosyasından okunur
        if (!string.IsNullOrEmpty(sifre))
        {
            var sifreci = scope.ServiceProvider.GetRequiredService<PasswordService>();
            db.Admins.Add(new Admin { Email = "admin@test.com", Password = sifreci.Hash(sifre) });
            db.SaveChanges();
        }
    }
}// geçici bir kod bloğu olucak büyük ihtimalle 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication(); //cookie yi inceler ve bu kim olduğuna göre yetkisi olup olmadığına karar verir
app.UseAuthorization(); // bu fonksiyon kim oluğunu öğrenir

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
