using Microsoft.EntityFrameworkCore; // ef core kütüphanesini ekler
using StajYonetimSistemi.Data; // data base ile bağlantıyı sağlıyan köprüyü kullanmamızı sağlar


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//ne zamana data base ye birşey kaydedilecek olursa bizim yazdığımız ayarları uygulamasını sağlıyan kod 
//projeyi çalıştırdığımda ana klasörün içine staj.db adında bir dosya oluştur ve bütün tabloları o dosyanın içine yaz demek oluyor
builder.Services.AddDbContext<StajDbContext>(options => options.UseSqlite("Data Source=staj.db"));
//                                                  sql lite kullanacağımızı bildiriyoruz



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
