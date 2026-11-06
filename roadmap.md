**Öğrenci 1 (backend) ENES YILMAZ**

1. ilk olarak proje şablonu kuruldu bu proje için mvc mimarisini kuruldu 
2. kullanılacak olan entitiler models klasörünün içine eklendi user student admin ve document olmak üzere
3. ardından veri tabanı için entity framework core kuruldu 
4. veri tabanı olarak sqlite seçildi ve bu database bağlantısı için gerekli kodlar pragram.cs e eklendi 
5. initial create migrationı ile veri tabanı taslağı oluşturuldu
6. students için controller oluşturuldu 
7. durum kondrolu için models klasörüne bir DocumentStatus eklendi ennum yapısı ile ve migration alındı
8. şifre servisi oluşturludu SHA256 servisi kullanıldı (256 bit uzunluğunda bir veriye dönüştürür)
9. spam koruması için kilit eklendi 
10. giriş denemelerini tutacak olan loginlog modeli eklendi 
11. auth kontrol mekanizmaları program.cs e eklendi 
12. form verilerinin diğer entityler le karışmaması için view models klasörü oluşturuldu 
13. klasörün içine login sayfasındaki form verilerinin dönmesi için bir loginviewmodels dosyası eklendi
14. authcontroller iskeleti ve sayfayı fırlatan get metodu yazıldı 
15. authcontroller içine post metodlarını çalıştıran fonksiyonlar yazıldı (login ve log out)
16. controller metodlarında yetkilendirme eklendi 
17. register sayfası için view model sınıfı oluşturuldu ve gereken bilgiler için form açıldı
18. document modelindeki eksik bilgiler tamamlandı ve migration alındı
19. register formuna e-posta format kontrolü eklendi
20. register formuna şifre kuralları eklendi
21. isim ve soyisim alanlarına sadece harf kabul eden ve başında ya da sonunda boşluğa izin vermeyen regex eklendi
22. kayıt olan kullanıcı için register sonunda cookie oluşturuldu, kullanıcı tekrar giriş yapmadan ana sayfaya yönlendiriliyor
23. AdminController eklendi, sadece ROLE_ADMIN rolü erişebiliyor
24. giriş yapan kullanıcı admin ise admin sayfasına, öğrenci ise ana sayfaya yönlendiriliyor
25. veri tabanı herkesin bilgisayarında ayrı olduğu için Program.cs e açılışta migration uygulayan ve hiç admin yoksa varsayılan bir admin hesabı oluşturan kod eklendi
26. admin özet sayfasındaki sayıları taşıyan AdminOzetViewModel yazıldı, AdminController daki Index metodu öğrenci ve belge sayılarını veri tabanında Count ile sayıp sayfaya gönderiyor
27. başvuru listesi için BasvuruSatiriViewModel ve BasvurularViewModel yazıldı, durumun Türkçe yazısı ve rengi switch ifadesiyle hesaplanıyor
28. Basvurular metodu yazıldı, belgeler duruma göre sekmeden ve öğrenci adı, soyadı ya da numarasına göre arama kutusundan filtreleniyor, belge ile öğrenci StudentId üzerinden eşleştirilip yükleme tarihine göre yeniden eskiye sıralanıyor
29. veri seti yükleme sayfasını açan VeriSeti metodu eklendi, içe aktarma sonraya bırakıldı
30. Document modeline doğrulama raporunu tutan Rapor alanı eklendi ve migration alındı
31. başvuru listesindeki her satıra Rapor alanı eklendi, AdminController belgenin raporunu satıra dolduruyor admin hatalı belgenin sebebini görebilecek

**Öğrenci 2 (evrak doğrulama) HARUN BOZKURT**

1. repo klonlandı ve doğrulama işleri için evrak-dogrulama adında ayrı bir branch açıldı
2. kuralları önce repo dışında bir deneme konsol projesinde yazıp test ettim, sonra projeye taşıyorum
3. validation klasörü oluşturuldu, tüm kuralların ortak ana sınıfı olan soyut Kural sınıfı yazıldı 
4. doğrulama sonucunu taşımak için Sonuc sınıfı yazıldı
5. kimlik kuralları için KimlikDogrulayici soyut sınıfı yazıldı 11 hane ve sadece rakam kontrolü burada ortak tutuldu
6. tc kimlik ve yabancı kimlik doğrulayıcıları KimlikDogrulayici sınıfından türetildi
7. telefon doğrulayıcı kuralı yazıldı regex ile +90 xxx xxx xx xx formatı kontrol edildi Kural sınıfından türetildi
8. e-posta doğrulayıcı yazıldı, @ işaretinin tek olması, öncesinde karakter bulunması, noktanın @ dan sonra gelmesi, noktanın sonda bulunmaması ve nokta ile @ arasında karakter olması kontrol ediliyor. Kural sınıfından türetildi
9. sonuç sınıfına uyarı bilgisi eklendi, mühendis sayısı doğrulayıcı yazıldı 
10. dosya formatı kuralı yazıldı doc docx pdf jpg jpeg ve png uzantıları kabul ediliyor diğerleri reddediliyor 
11. şu anda başta ve sondaki boşluklar silinmiyor bu işlem en son kuralları çağıran katmanda tek yerde yapılacak
12. edestek forumundaki cevaba göre vizede evrak yükleme kontrolleri sadece dosya formatı ve uzantı kontrolünü kapsıyor, bu yüzden adres, öğretim yılı, teslim tarihi ve staj günü gibi diğer doğrulama kurallarına vizeden sonra devam edilecek
13. yüklenen dosyayı sunucuya kaydeden DosyaKaydedici servisi yazıldı, dosya Guid ile yeni isim alıp uploads klasörüne kaydediliyor, gerçek adı veri tabanında tutuluyor
14. uploads klasörü gitignore a eklendi
15. DocumentsController yazıldı, sadece öğrenci rolü girebiliyor, dosya uzantısı kontrol edilip kaydediliyor ve Document tablosuna ekleniyor
16. Upload.cshtml yükleme sayfası eklendi, tasarımı frontend arkadaşa bırakıldı, hata ve başarı mesajları sayfada gösteriliyor
17. controller dosya adı DocumentsController.cs olarak düzeltildi, mesajlardaki yazım hataları düzeltildi
18. login ve yükleme akışı ayrı bir kopyada uçtan uca denendi, giriş, rol kontrolü, geçerli pdf kaydı, geçersiz uzantı ve boş dosya reddi çalışıyor
19. forumdaki cevaba göre belgeden okunan bilgi ile kullanıcının bilgisi karşılaştırılacak, bunun için docx okuma işine başlandı, önce deneme projesinde yapıp sonra projeye taşındı
20. DocumentFormat.OpenXml paketi eklendi, soyut BelgeOkuyucu sınıfı yazıldı, DocxOkuyucu bundan türetildi (soyutlama, kalıtım, çok biçimlilik), docx deki tabloları okuyup başlık ve değer çiftlerini sözlük olarak veriyor
21. Ek16Bilgi sınıfı yazıldı (ad soyad, tc, uyruk, öğrenci no, telefon, e-posta), Ek16Ayiklayici sözlüğü bu sınıfa çeviriyor, tc ile uyruk ve telefon ile e-posta aynı hücrede olduğu için / işaretinden bölünüyor
22. yüklenen docx belgesi okunup kimlik, telefon ve e-posta kurallarına veriliyor, sonuç yükleme sayfasında liste olarak gösteriliyor, uyruğa göre TC veya yabancı kimlik kuralı seçiliyor
23. uyruk boşsa kimlik kontrolü yapılmıyor ve rapora uyruk okunamadı yazılıyor, bozuk docx yüklenirse try catch ile site çökmeden belge okunamadı yazıyor
24. sayfa yenilenince (F5) belgenin ikinci kez yüklenmesi sorunu düzeltildi, yükleme sonrası RedirectToAction ve TempData kullanıldı
25. geçersiz belgelerle denendi: tc 0 ile başlayan, hatalı telefon, hatalı e-posta, yabancı kimlik no 99 ile başlamıyor, bozuk docx, hepsinde beklenen mesajlar çıktı
26. pdf, doc ve resim dosyaları kabul edilip kaydediliyor ama içleri okunmuyor, ekranda bu formatta otomatik okuma desteklenmiyor yazıyor, pdf için PdfPig ile okuma sonraya bırakıldı, resimler için OCR final aşamasında




**Öğrenci 3 (frontend) TAHA ÇELİK**

1. repo klonlandı
2. arayüz işleri için frontend adında ayrı bir branch açıldı
3. ortak sayfa iskeleti (_Layout.cshtml) düzenlendi, menü ve alt bilgi alanı burada toplandı
4. KOÜ amblemi wwwroot/img klasörüne eklendi, menüde site adının yanında ve tarayıcı sekmesinde simge olarak gösterildi
5. site.css dosyası yazıldı, KOÜ yeşili renk değişkeni olarak tanımlandı, butonlar yeşil temaya çevrildi
6. amblemin yeşili beyaz yazıyla yeterince okunmadığı için buton ve bağlantılarda daha koyu bir yeşil kullanıldı
7. menünün altına ince bir yeşil çizgi eklendi
8. sayfa açılırken hafif belirme ve kartların üzerine gelince hafif yükselme animasyonu eklendi, hareketi azaltmak isteyen kullanıcılar için animasyonlar kapatıldı
9. giriş sayfası (Login.cshtml) yazıldı, form alanları LoginViewModel ile eşleştirildi
10. giriş hataları sayfada kutu içinde, "Hata" yazısıyla gösteriliyor
11. giriş ve kayıt sayfasına şifre göster/gizle düğmesi eklendi
12. giriş ve kayıt kartlarının üstüne yeşil çizgi ve küçük amblem eklendi
13. kayıt sayfası (Register.cshtml) yazıldı, RegisterViewModel alanlarıyla eşleştirildi
14. kayıt sayfasına şifre kuralları listesi eklendi (8 karakter, büyük harf, küçük harf, rakam), kullanıcı yazdıkça kurallar canlı olarak işaretleniyor
15. ana sayfa tasarlandı: büyük amblem, site adı ve kısa açıklama
16. ana sayfaya üç adımlı "nasıl çalışır" kartları eklendi (kayıt ol, belgeni yükle, sonucu gör)
17. ana sayfadaki butonlar kullanıcıya göre değişiyor: giriş yapmamışsa Giriş Yap ve Kayıt Ol, öğrenciyse Belge Yükle ve Belgelerim, yöneticiyse Yönetim Paneline Git
18. menü kullanıcının rolüne göre değişiyor: öğrenci için Belge Yükle ve Belgelerim, yönetici için Yönetim Paneli, Başvurular, Öğrenciler ve Veri Seti
19. kullanıcının bulunduğu sayfa menüde kalın yazılıyor
20. belge yükleme sayfası tasarlandı, Harun'un yazdığı zorunlu kısımlar korundu (enctype, name="dosya", mesaj ve hata alanları)
21. yükleme sayfasına sürükle bırak alanı ve dosya seç butonu eklendi
22. kabul edilen biçimler rozet olarak gösterildi (DOC, DOCX, PDF, JPG, JPEG, PNG)
23. seçilen dosya için kart eklendi, dosya adı ve boyutu gösteriliyor, kaldır butonuyla seçim geri alınıyor
24. yanlış uzantılı dosya seçilince sayfa gönderilmeden javascript ile uyarı veriliyor, sunucu kontrolü yine de yapılıyor
25. yükle butonuna basınca buton kilitleniyor ve "Yükleniyor" yazıyor
26. yükleme sonrası "Başarılı" kutusu ve Belgelerim sayfasına giden bağlantı gösteriliyor
27. yükleme sonrası kontrol sonucu ayrı bir kartta listeleniyor, her satırın başında geçerli, hatalı ya da bilgi işareti ve yazısı var, hata varsa düzeltip yeniden yükleme notu çıkıyor
28. Belgelerim sayfası (Documents/Index.cshtml) yazıldı, öğrencinin belgeleri dosya adı, yükleme tarihi, durum rozeti ve açılır kontrol sonucuyla listeleniyor
29. öğrenci listesi sayfası (Students/Index.cshtml) düzenlendi: başlık, öğrenci sayısı, tablo ve boş liste mesajı
30. hata sayfası (Error.cshtml) Türkçeleştirildi
31. yetkisiz erişim için erişim izni yok sayfası (AccessDenied.cshtml) yazıldı
32. yönetim paneli (Admin/Index.cshtml) yazıldı, öğrenci, toplam belge, hatalı ve tamamlanan sayıları ile işlem kartları gösteriliyor
33. başvurular sayfası (Admin/Basvurular.cshtml) yazıldı, belgeler duruma göre sekmelerde listeleniyor
34. başvurular sayfasına öğrenci adı ya da numarasına göre arama kutusu eklendi
35. başvurular sayfasına her belgenin kontrol sonucunu gösteren sütun eklendi
36. başvurular sayfasına her belge için durum değiştirme listesi eklendi
37. başvurular sayfasına belge silme butonu eklendi, silmeden önce onay penceresi soruyor
38. veri seti sayfası (Admin/VeriSeti.cshtml) yazıldı, CSV yükleme formu ve beklenen 12 sütunu gösteren dosya biçimi tablosu eklendi, .csv olmayan dosya seçilince uyarı veriliyor
39. veri seti yüklenince sonuç mesajı yeşil "Başarılı" kutusunda, hata kırmızı "Hata" kutusunda gösteriliyor