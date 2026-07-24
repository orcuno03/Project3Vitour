# Vitour

ASP.NET Core MVC ve MongoDB ile geliştirilmiş tur tanıtım ve rezervasyon sitesi.
Ziyaretçi tarafında tur listeleme, tur detayı, yorum ve rezervasyon; yönetim panelinde
tur, kategori, lokasyon, tur planı, tur görseli, yorum ve rezervasyon yönetimi bulunur.

## Kullanılan teknolojiler

- .NET 6 / ASP.NET Core MVC
- MongoDB (MongoDB.Driver)
- AutoMapper
- ClosedXML (Excel çıktısı), QuestPDF (PDF çıktısı)
- Bootstrap 5, Bootstrap Icons

## Kurulum

1. MongoDB'yi çalıştır ve bağlantı bilgisini `Project3Vitour/appsettings.json`
   içindeki `DatabaseSettingKey` bölümünden kontrol et.
2. Örnek veriyi yükle:

   ```
   mongosh "mongodb://localhost:27017" --file DbSeed/seed-vitourdb.js
   ```

   Ayrıntılar için `DbSeed/README.md` dosyasına bak.
3. Uygulamayı çalıştır:

   ```
   dotnet run --project Project3Vitour
   ```

## Sayfalar

- Site: `/Tour/TourList`, `/Tour/TourDetail/{id}`
- Yönetim paneli: `/AdminTour/TourList` ve diğer `Admin*` ekranları

Arayüz Türkçe ve İngilizce olarak sunulur; dil seçimi çerez tabanlıdır.
