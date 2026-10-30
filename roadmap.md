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

**Öğrenci 2 (evrak doğrulama) HARUN BOZKURT**

1. repo klonlandı ve doğrulama işleri için evrak-dogrulama adında ayrı bir branch açıldı
2. kuralları önce repo dışında bir deneme konsol projesinde yazıp test ettim, sonra projeye taşıyorum
3. validation klasörü oluşturuldu, tüm kuralların ortak ana sınıfı olan soyut Kural sınıfı yazıldı 
4. doğrulama sonucunu taşımak için Sonuc sınıfı yazıldı
5. kimlik kuralları için KimlikDogrulayici soyut sınıfı yazıldı 11 hane ve sadece rakam kontrolü burada ortak tutuldu
6. tc kimlik ve yabancı kimlik doğrulayıcıları KimlikDogrulayici sınıfından türetildi
7. telefon doğrulayıcı kuralı yazıldı regex ile +90 xxx xxx xx xx formatı kontrol edildi Kural sınıfından türetildi
8. e-posta doğrulayıcı yazıldı, @ işaretinin tek olması, öncesinde karakter bulunması, noktanın @ dan sonra gelmesi, noktanın sonda bulunmaması ve nokta ile @ arasında karakter olması kontrol ediliyor. Kural sınıfından türetildi