# 🌍 Vitour - Modern Tur Tanıtım & Rezervasyon Platformu

<p align="center">
  <img src="https://img.shields.io/badge/.NET_6-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 6" />
  <img src="https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=for-the-badge&logo=.net&logoColor=white" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white" alt="MongoDB" />
  <img src="https://img.shields.io/badge/Bootstrap_5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" alt="Bootstrap 5" />
  <img src="https://img.shields.io/badge/AutoMapper-000000?style=for-the-badge&logo=nuget&logoColor=white" alt="AutoMapper" />
  <img src="https://img.shields.io/badge/QuestPDF-FF5722?style=for-the-badge&logo=pdf&logoColor=white" alt="QuestPDF" />
</p>

---

## 📌 Proje Hakkında

**Vitour**, ASP.NET Core MVC mimarisi ve NoSQL (MongoDB) veritabanı kullanılarak geliştirilmiş, uçtan uca modern bir **Tur Tanıtım, İnceleme ve Rezervasyon Yönetim Platformudur**. 

Platform; ziyaretçilere turları keşfetme, detaylı tur programlarını inceleme, yorum yapma ve online rezervasyon talebi oluşturma imkanı sunarken; yöneticiler için gelişmiş **Admin Dashboard**, dinamik tur ve lokasyon yönetimi, onay süreçleri ve **PDF/Excel raporlama** altyapısı sağlamaktadır. Ayrıca çerez (cookie) tabanlı Türkçe (TR) ve İngilizce (EN) çoklu dil desteğine (Localization) sahiptir.

---

## 🚀 Öne Çıkan Özellikler

### 👤 Ziyaretçi (Kullanıcı) Arayüzü
- **Dinamik Tur Listeleme & Filtreleme:** Kategori, lokasyon ve fiyat aralığına göre turları arama ve filtreleme.
- **Detaylı Tur İnceleme:** Tur süresi, rotası, kontenjan durumu, harita görseli ve fiyat bilgisi sunan detay sayfaları.
- **Günlük Tur Planı (Itinerary):** Tur kapsamındaki her günün programını adım adım sunan interaktif yapı.
- **Fotoğraf Galerisi:** Tura ait yüksek çözünürlüklü görselleri sergileyen dinamik galeri.
- **Yorum & Puanlama Sistemi:** Kullanıcıların turları derecelendirebildiği ve yorum bırakabildiği interaktif modül.
- **Çevrimiçi Rezervasyon:** Kontenjan kontrolü ile hızlı ve güvenli tur rezervasyon talebi oluşturma.
- **E-Posta Bilgilendirme:** MailKit entegrasyonu ile rezervasyon durumuna özel otomatik mail bildirimi.

### 🛡️ Yönetim Paneli (Admin Dashboard)
- **Kapsamlı İstatistikler:** Aktif turlar, toplam rezervasyonlar, kullanıcı yorumları ve performans metriklerini gösteren dashboard paneli.
- **Tur & İçerik Yönetimi:** Tur ekleme/düzenleme/silme, tur görsellerini yönetme ve günlük tur planlarını (itinerary) oluşturma.
- **Kategori & Lokasyon (Destination) Yönetimi:** Tur kategorilerini ve popüler lokasyonları yönetme.
- **Rezervasyon Yönetimi:** Gelen rezervasyon taleplerini inceleme, onaylama, iptal etme veya durum güncelleme (Beklemede, Onaylandı, İptal).
- **Yorum Onay Modülü:** Kullanıcı yorumlarını denetleme ve yayına alma süreçleri.

### 📊 Raporlama ve Dışa Aktarım (Exporting)
- **📄 QuestPDF Entegrasyonu:** Rezervasyon belgelerini ve tur detaylarını profesyonel PDF formatında oluşturma ve indirme.
- **📊 ClosedXML Entegrasyonu:** Tüm rezervasyon ve tur verilerini rapor halinde Excel (.xlsx) formatında dışa aktarma.

### 🌍 Çoklu Dil (Localization) Desteği
- **Çerez Tabanlı Dil Değiştirimi:** Ziyaretçiler tek tıkla arayüzü **Türkçe (TR)** veya **İngilizce (EN)** dillerine dönüştürebilir.
- **`.resx` Altyapısı:** Arayüzdeki metinler `SharedResource` mekanizması ile katmanlı ve performanslı biçimde yerelleştirilmiştir.

---

## 🛠️ Kullanılan Teknolojiler & Kütüphaneler

### **Backend (Arka Plan)**
* **Framework:** .NET 6 / ASP.NET Core MVC
* **Veritabanı Driver:** `MongoDB.Driver` (NoSQL)
* **Veri Dönüşümü:** `AutoMapper` (DTO - Entity dönüşümleri)
* **E-Posta Servisi:** `MailKit` & `MimeKit` (SMTP Entegrasyonu)
* **Raporlama:** `QuestPDF` (PDF Çıktısı) & `ClosedXML` (Excel Çıktısı)
* **Localization:** `Microsoft.AspNetCore.Localization` (TR/EN Desteği)

### **Database (Veritabanı)**
* **Database Engine:** MongoDB (Document-Based NoSQL)
* **Koleksiyonlar:** `Tours`, `Categories`, `Destinations`, `TourPlans`, `TourImages`, `Reviews`, `Reservations`

### **Frontend (Arayüz)**
* **Arayüz Framework:** HTML5, CSS3, JavaScript (ES6+)
* **Tasarım & UI:** Bootstrap 5, Bootstrap Icons
* **Tema & Template:** Modern Responsive Vitour Template & Custom CSS

---

## 📁 Proje Mimarisi & Klasör Yapısı

```
Project3Vitour/
│
├── DbSeed/                       # MongoDB başlangıç verisi (seed scriptleri)
│   ├── seed-vitourdb.js          # Tüm koleksiyonlar için hazır seed verisi
│   └── update-map-images.js      # Harita görselleri güncelleme betiği
│
├── Project3Vitour/
│   ├── Controllers/              # MVC Controller sınıfları (Site & Admin)
│   │   ├── AdminTourController.cs
│   │   ├── AdminReservationController.cs
│   │   ├── TourController.cs
│   │   ├── ReservationController.cs
│   │   └── ...
│   ├── Entities/                 # MongoDB BSON nesne modelleri
│   │   ├── Tour.cs
│   │   ├── Reservation.cs
│   │   └── ...
│   ├── Dtos/                     # Data Transfer Objects (Katmanlar arası veri taşıma)
│   ├── Services/                 # İş Mantığı Servisleri (BLL & Data Layer)
│   │   ├── TourServices/
│   │   ├── ReservationServices/
│   │   ├── ExportServices/       # QuestPDF & ClosedXML servisleri
│   │   └── ...
│   ├── Resources/                # Çoklu Dil (.resx) Dosyaları (SharedResource.tr.resx)
│   ├── Views/                    # Razor View şablonları
│   └── wwwroot/                  # Statik dosyalar (CSS, JS, Görseller, Haritalar)
│
└── Project3Vitour.sln            # Solution Dosyası
```

---

## ⚙️ Kurulum ve Çalıştırma Rehberi

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları sırasıyla uygulayabilirsiniz:

### 1. Ön Gereksinimler
* [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
* [MongoDB Community Server](https://www.mongodb.com/try/download/community) & [MongoDB Shell (`mongosh`)](https://www.mongodb.com/try/download/shell)
* IDE: Visual Studio 2022 / VS Code / JetBrains Rider

### 2. Repoyu Klonlayın
```bash
git clone https://github.com/orcuno03/Project3Vitour.git
cd Project3Vitour
```

### 3. MongoDB Veritabanını Hazırlama (Seed Data)
MongoDB servisinizin çalıştığından emin olduktan sonra terminal veya komut satırında hazır seed scriptini çalıştırarak veritabanını örnek verilerle doldurabilirsiniz:

```bash
mongosh "mongodb://localhost:27017" --file DbSeed/seed-vitourdb.js
```
*(Alternatif olarak Windows servis kontrolü: `net start MongoDB`)*

### 4. Bağlantı Ayarlarını Kontrol Edin
`Project3Vitour/appsettings.json` dosyasındaki MongoDB bağlantı cümlesini gözden geçirin:
```json
"DatabaseSettingKey": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "VitourDb"
}
```

### 5. Projeyi Çalıştırın
```bash
dotnet run --project Project3Vitour
```
Uygulama başladığında tarayıcınızda `https://localhost:7048` veya `http://localhost:5048` adresine giderek platformu inceleyebilirsiniz.

---

## 📸 Ekran Görüntüleri

*Aşağıdaki alanlara kendi ekran görüntülerinizi sürükleyip bırakarak yükleyebilirsiniz:*

| Ziyaretçi Vitrin Sayfası | Tur Detay & Günlük Plan |
| :---: | :---: |
<img width="1905" height="910" alt="image" src="https://github.com/user-attachments/assets/e06cf8dd-9d7d-4aa7-a3fa-69386960376c" />
<img width="1350" height="1935" alt="localhost_7257_Tour_TourDetail_660000000000000000000004" src="https://github.com/user-attachments/assets/a3736bd5-5a18-4fc4-ad1a-66f4414febf0" />
<img width="1918" height="900" alt="image" src="https://github.com/user-attachments/assets/253643eb-3222-4649-99e2-e51d90aa0e02" />
<img width="1639" height="793" alt="image" src="https://github.com/user-attachments/assets/22d16df4-5848-49fe-8af9-90adc9e66457" />
<img width="1350" height="2650" alt="localhost_7257_Tour_TourList" src="https://github.com/user-attachments/assets/ed3ba365-df9a-45a3-befc-d7f56598cc76" />


| Admin Dashboard | Rezervasyon & Raporlama |
| :---: | :---: |
| <img width="1903" height="831" alt="image" src="https://github.com/user-attachments/assets/fcaeb933-bde2-4e9c-9b15-ae1467cc4839" /> |
| <img width="1900" height="875" alt="image" src="https://github.com/user-attachments/assets/5ff5e5e0-5568-439c-a097-aae499eacbb1" /> |
| <img width="1898" height="856" alt="image" src="https://github.com/user-attachments/assets/16e78d80-0536-4967-8682-6f01979fc77c" /> |

---

## 👨‍💻 Yazar & İletişim

Geliştirici hakkında detaylı bilgi almak veya iletişime geçmek için:

* **GitHub:** [@orcuno03](https://github.com/orcuno03)
* **LinkedIn:** [Orçun](https://www.linkedin.com/in/orcunozsen/)

---

⭐ Projeyi beğendiyseniz GitHub üzerinde yıldız (star) vermeyi unutmayın!
