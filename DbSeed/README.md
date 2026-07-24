# VitourDb - Veritabanı Kurulum

MongoDB veritabanını (`VitourDb`) sıfırdan oluşturur ve örnek veriyle doldurur.

## Çalıştırma

MongoDB'nin ayakta olduğundan emin ol, sonra:

```
mongosh "mongodb://localhost:27017" --file DbSeed/seed-vitourdb.js
```

> **Dikkat:** Script koleksiyonları önce `drop()` eder. Mevcut veriyi siler.

## Oluşturduğu veri

| Koleksiyon | Kayıt |
|---|---|
| Categories | 5 |
| Destinations | 12 |
| Tours | 12 (hepsi yayında, tur listesinde 2 sayfa) |
| TourPlans | 37 |
| TourImages | 31 |
| Reviews | 15 |
| Reservations | 17 |

Koleksiyon adları `Project3Vitour/appsettings.json` içindeki `DatabaseSettingKey` bölümüyle,
alan adları ve BSON tipleri `Project3Vitour/Entities/*.cs` ile birebir eşleşir.

## Notlar

- **Karadeniz Yaylalar Turu** kasten kontenjana yakın bırakıldı (kapasite 16, dolu 14);
  rezervasyon ekranındaki kontenjan kontrolünü test edebilmek için.
- **`MapLocationImageUrl` alanları** bölge haritası görsellerini gösterir. Görseller
  `wwwroot/vitour/assets/images/map/` altındadır; admin panelinden
  (Turlar, Düzenle, Bölge Haritası Görseli) değiştirilebilir. Görseller sonradan
  eklenirse `update-map-images.js` ile veriyi silmeden güncelleyebilirsin.

## MongoDB servisi çalışmıyorsa

Yönetici olarak açılmış bir PowerShell'de:

```
net start MongoDB
```
