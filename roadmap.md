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