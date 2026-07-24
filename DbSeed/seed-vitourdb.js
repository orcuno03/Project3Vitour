// VitourDb seed - Case 3 veri modeli
// Collection adlari appsettings.json > DatabaseSettingKey ile birebir uyumlu:
//   Tours / Categories / Reviews / Destinations / TourPlans / TourImages / Reservations
// Alan adlari ve BSON tipleri Entities/*.cs ile birebir eslesir.
//   string   -> string        (Id'ler: [BsonRepresentation(BsonType.ObjectId)] => _id: ObjectId)
//   int      -> NumberInt
//   decimal  -> NumberDecimal (MongoDB.Driver 3.x varsayilani: Decimal128)
//   bool     -> boolean
//   DateTime -> ISODate

const vitour = db.getSiblingDB("VitourDb");

vitour.Categories.drop();
vitour.Destinations.drop();
vitour.Tours.drop();
vitour.TourPlans.drop();
vitour.TourImages.drop();
vitour.Reviews.drop();
vitour.Reservations.drop();

// Categories
const catKultur = ObjectId("650000000000000000000001");
const catDoga   = ObjectId("650000000000000000000002");
const catDeniz  = ObjectId("650000000000000000000003");
const catSehir  = ObjectId("650000000000000000000004");
const catKayak  = ObjectId("650000000000000000000005");

vitour.Categories.insertMany([
  { _id: catKultur, CategoryName: "Kültür Turu",   CategoryStatus: true },
  { _id: catDoga,   CategoryName: "Doğa & Macera", CategoryStatus: true },
  { _id: catDeniz,  CategoryName: "Deniz Tatili",  CategoryStatus: true },
  { _id: catSehir,  CategoryName: "Şehir Turu",    CategoryStatus: true },
  { _id: catKayak,  CategoryName: "Kayak Turu",    CategoryStatus: true },
]);

// Destinations
const desNevsehir = ObjectId("670000000000000000000001");
const desIzmir    = ObjectId("670000000000000000000002");
const desRize     = ObjectId("670000000000000000000003");
const desMugla    = ObjectId("670000000000000000000004");
const desIstanbul = ObjectId("670000000000000000000005");
const desBursa    = ObjectId("670000000000000000000006");

vitour.Destinations.insertMany([
  { _id: desNevsehir, City: "Nevşehir", Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/kapadokya.jpg", DestinationStatus: true },
  { _id: desIzmir,    City: "İzmir",    Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/efes.jpg", DestinationStatus: true },
  { _id: desRize,     City: "Rize",     Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/karadeniz.jpg", DestinationStatus: true },
  { _id: desMugla,    City: "Muğla",    Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/fethiye.jpg", DestinationStatus: true },
  { _id: desIstanbul, City: "İstanbul", Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/istanbul.jpg", DestinationStatus: true },
  { _id: desBursa,    City: "Bursa",    Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/uludag.jpg", DestinationStatus: true },
]);

// Tours
const kapadokya = ObjectId("660000000000000000000001");
const efes      = ObjectId("660000000000000000000002");
const karadeniz = ObjectId("660000000000000000000003");
const fethiye   = ObjectId("660000000000000000000004");
const istanbul  = ObjectId("660000000000000000000005");
const uludag    = ObjectId("660000000000000000000006");

// MapLocationImageUrl: bolge haritasi gorselleri; dosyalar
// wwwroot/vitour/assets/images/map/ altinda.
vitour.Tours.insertMany([
  {
    _id: kapadokya,
    Title: "Kapadokya Balon ve Peribacaları Turu",
    Description: "Göreme Açık Hava Müzesi, Uçhisar Kalesi ve gün doğumunda sıcak hava balonu deneyimi. Konaklama mağara otelde, kahvaltı dahil.",
    CoverImageUrl: "/vitour/assets/images/tours/kapadokya.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/kapadokya.jpg",
    Badge: "Popular",
    DayCount: NumberInt(3),
    Capacity: NumberInt(20),
    Price: NumberDecimal("7500"),
    IsStatus: true,
    CategoryId: catKultur.toString(),
    DestinationId: desNevsehir.toString(),
  },
  {
    _id: efes,
    Title: "Efes Antik Kenti & Şirince Kültür Turu",
    Description: "Celsus Kütüphanesi, Büyük Tiyatro ve Meryem Ana Evi rehberli gezi. Dönüşte Şirince köyünde şarap tadımı.",
    CoverImageUrl: "/vitour/assets/images/tours/efes.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/efes.jpg",
    Badge: "New",
    DayCount: NumberInt(2),
    Capacity: NumberInt(30),
    Price: NumberDecimal("4250.50"),
    IsStatus: true,
    CategoryId: catKultur.toString(),
    DestinationId: desIzmir.toString(),
  },
  {
    _id: karadeniz,
    Title: "Karadeniz Yaylalar Turu",
    Description: "Ayder, Pokut ve Uzungöl rotası. Sis altında yayla evleri, Fırtına Vadisi ve yöresel mutfak deneyimi.",
    CoverImageUrl: "/vitour/assets/images/tours/karadeniz.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/karadeniz.jpg",
    Badge: "Limited",
    DayCount: NumberInt(5),
    Capacity: NumberInt(16),
    Price: NumberDecimal("12900"),
    IsStatus: true,
    CategoryId: catDoga.toString(),
    DestinationId: desRize.toString(),
  },
  {
    _id: fethiye,
    Title: "Fethiye Ölüdeniz Mavi Tur",
    Description: "12 Adalar koyları boyunca tekne turu, Kelebekler Vadisi ve Ölüdeniz'de yamaç paraşütü opsiyonu.",
    CoverImageUrl: "/vitour/assets/images/tours/fethiye.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/fethiye.jpg",
    Badge: "Popular",
    DayCount: NumberInt(4),
    Capacity: NumberInt(24),
    Price: NumberDecimal("9800"),
    IsStatus: true,
    CategoryId: catDeniz.toString(),
    DestinationId: desMugla.toString(),
  },
  {
    _id: istanbul,
    Title: "İstanbul Tarihi Yarımada Turu",
    Description: "Ayasofya, Topkapı Sarayı, Yerebatan Sarnıcı ve Kapalıçarşı. Boğaz'da akşam yemeği ile kapanış.",
    CoverImageUrl: "/vitour/assets/images/tours/istanbul.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/istanbul.jpg",
    Badge: "",
    DayCount: NumberInt(1),
    Capacity: NumberInt(40),
    Price: NumberDecimal("1750"),
    IsStatus: true,
    CategoryId: catSehir.toString(),
    DestinationId: desIstanbul.toString(),
  },
  {
    _id: uludag,
    Title: "Uludağ Kayak Kampı",
    Description: "Otel + skipass dahil kayak paketi. Başlangıç seviyesi için eğitmen desteği ve ekipman kiralama.",
    CoverImageUrl: "/vitour/assets/images/tours/uludag.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/uludag.jpg",
    Badge: "Limited",
    DayCount: NumberInt(3),
    Capacity: NumberInt(18),
    Price: NumberDecimal("6400"),
    IsStatus: true,
    CategoryId: catKayak.toString(),
    DestinationId: desBursa.toString(),
  },
]);

// TourPlans: her turun DayCount'u kadar gun plani
function plan(tourId, day, title, desc) {
  return { DayNumber: NumberInt(day), Title: title, Description: desc, TourId: tourId.toString() };
}

vitour.TourPlans.insertMany([
  // Kapadokya - 3 gun
  plan(kapadokya, 1, "Varış ve Göreme", "Nevşehir Kapadokya Havalimanı'nda karşılama, mağara otele yerleşme. Öğleden sonra Göreme Açık Hava Müzesi ve kaya kiliseleri gezisi. Akşam yemeği otelde."),
  plan(kapadokya, 2, "Balon Turu ve Kızıl Vadi", "Gün doğumundan önce sıcak hava balonu uçuşu (hava şartlarına bağlı). Kahvaltı sonrası Kızıl Vadi yürüyüşü, Paşabağ ve Devrent Vadisi. Akşam Türk gecesi programı."),
  plan(kapadokya, 3, "Uçhisar ve Dönüş", "Uçhisar Kalesi'nden panoramik manzara, çömlek atölyesi ziyareti ve el sanatları alışverişi. Öğleden sonra havalimanına transfer."),

  // Efes - 2 gun
  plan(efes, 1, "Efes Antik Kenti", "İzmir'den hareket, Efes Antik Kenti'nde rehberli gezi: Celsus Kütüphanesi, Büyük Tiyatro, Hadrian Tapınağı. Öğle yemeği Selçuk'ta. Akşam otele yerleşme."),
  plan(efes, 2, "Meryem Ana ve Şirince", "Sabah Meryem Ana Evi ziyareti. Ardından Şirince köyünde serbest zaman, yerel şarap tadımı ve köy pazarı. Akşamüstü İzmir'e dönüş."),

  // Karadeniz - 5 gun
  plan(karadeniz, 1, "Trabzon'a Varış", "Trabzon Havalimanı karşılama, şehir merkezine yerleşme. Atatürk Köşkü ve Boztepe'den şehir manzarası."),
  plan(karadeniz, 2, "Sümela ve Uzungöl", "Sümela Manastırı ziyareti, ardından Uzungöl'e geçiş. Göl çevresinde yürüyüş ve yöresel akşam yemeği."),
  plan(karadeniz, 3, "Ayder Yaylası", "Rize üzerinden Ayder Yaylası'na hareket. Çay bahçeleri, Fırtına Vadisi ve Zil Kale ziyareti."),
  plan(karadeniz, 4, "Pokut ve Sal Yaylası", "4x4 araçlarla Pokut Yaylası'na çıkış. Sis denizi manzarası, ahşap yayla evlerinde konaklama deneyimi."),
  plan(karadeniz, 5, "Dönüş", "Sabah yayla kahvaltısı, hediyelik alışverişi ve Trabzon Havalimanı'na transfer."),

  // Fethiye - 4 gun
  plan(fethiye, 1, "Fethiye'ye Varış", "Dalaman Havalimanı karşılama, Fethiye'de otele yerleşme. Akşam Fethiye limanında serbest zaman."),
  plan(fethiye, 2, "12 Adalar Tekne Turu", "Tam gün tekne turu: Yassıca Adaları, Tersane Koyu ve Aquarium Koyu'nda yüzme molaları. Öğle yemeği teknede."),
  plan(fethiye, 3, "Ölüdeniz ve Kelebekler Vadisi", "Ölüdeniz plajında serbest zaman, isteğe bağlı yamaç paraşütü (ekstra ücretli). Öğleden sonra Kelebekler Vadisi'ne tekne ile geçiş."),
  plan(fethiye, 4, "Kayaköy ve Dönüş", "Terk edilmiş Kayaköy'de rehberli yürüyüş. Öğleden sonra Dalaman Havalimanı'na transfer."),

  // Istanbul - 1 gun
  plan(istanbul, 1, "Tarihi Yarımada", "Sultanahmet'te buluşma. Ayasofya, Topkapı Sarayı ve Yerebatan Sarnıcı rehberli gezi. Kapalıçarşı'da serbest zaman. Akşam Boğaz'da yemek ile kapanış."),

  // Uludag - 3 gun
  plan(uludag, 1, "Bursa ve Uludağ", "Bursa'da karşılama, teleferik ile Uludağ'a çıkış ve otele yerleşme. Ekipman teslimi."),
  plan(uludag, 2, "Kayak Eğitimi", "Sabah başlangıç seviyesi eğitmenli kayak dersi. Öğleden sonra pistlerde serbest kayak. Akşam otelde apre-ski."),
  plan(uludag, 3, "Serbest Kayak ve Dönüş", "Sabah serbest kayak, öğleden sonra ekipman teslimi ve Bursa'ya dönüş transferi."),
]);

// TourImages: her tura ait galeri fotograflari
function img(tourId, title, url) {
  return { Title: title, ImageUrl: url, TourId: tourId.toString() };
}

vitour.TourImages.insertMany([
  img(kapadokya, "Peribacaları", "/vitour/assets/images/tours/kapadokya.jpg"),
  img(kapadokya, "Balon Uçuşu", "/vitour/assets/images/tours/kapadokya.jpg"),
  img(kapadokya, "Mağara Otel", "/vitour/assets/images/tours/kapadokya.jpg"),
  img(kapadokya, "Göreme Vadisi", "/vitour/assets/images/tours/kapadokya.jpg"),

  img(efes, "Celsus Kütüphanesi", "/vitour/assets/images/tours/efes.jpg"),
  img(efes, "Büyük Tiyatro", "/vitour/assets/images/tours/efes.jpg"),
  img(efes, "Şirince Köyü", "/vitour/assets/images/tours/efes.jpg"),

  img(karadeniz, "Ayder Yaylası", "/vitour/assets/images/tours/karadeniz.jpg"),
  img(karadeniz, "Sis Denizi", "/vitour/assets/images/tours/karadeniz.jpg"),
  img(karadeniz, "Fırtına Vadisi", "/vitour/assets/images/tours/karadeniz.jpg"),
  img(karadeniz, "Uzungöl", "/vitour/assets/images/tours/karadeniz.jpg"),

  img(fethiye, "Ölüdeniz", "/vitour/assets/images/tours/fethiye.jpg"),
  img(fethiye, "12 Adalar", "/vitour/assets/images/tours/fethiye.jpg"),
  img(fethiye, "Kelebekler Vadisi", "/vitour/assets/images/tours/fethiye.jpg"),

  img(istanbul, "Ayasofya", "/vitour/assets/images/tours/istanbul.jpg"),
  img(istanbul, "Boğaz Manzarası", "/vitour/assets/images/tours/istanbul.jpg"),
  img(istanbul, "Kapalıçarşı", "/vitour/assets/images/tours/istanbul.jpg"),

  img(uludag, "Uludağ Pistleri", "/vitour/assets/images/tours/uludag.jpg"),
  img(uludag, "Kayak Kampı", "/vitour/assets/images/tours/uludag.jpg"),
]);

// Reviews: Rehber / Konaklama / Ulasim / Konfor puanlari (her biri 1-5)
function review(tourId, name, detail, guide, acc, trans, comfort, date, status) {
  return {
    NameSurname: name,
    Detail: detail,
    GuideScore: NumberInt(guide),
    AccommodationScore: NumberInt(acc),
    TransportScore: NumberInt(trans),
    ComfortScore: NumberInt(comfort),
    ReviewDate: date,
    Status: status,
    TourId: tourId.toString(),
  };
}

vitour.Reviews.insertMany([
  review(kapadokya, "Ahmet Yılmaz", "Balon turu hayatımın en güzel deneyimiydi, rehberimiz çok ilgiliydi. Kesinlikle tavsiye ederim.", 5, 5, 4, 5, ISODate("2026-05-14T09:30:00Z"), true),
  review(kapadokya, "Elif Demir", "Mağara otel beklediğimden çok daha konforluydu. Tek eksik kahvaltının biraz erken olması.", 4, 5, 4, 4, ISODate("2026-05-21T16:05:00Z"), true),
  review(kapadokya, "Burak Şahin", "Hava muhalefeti nedeniyle balon uçamadı ama ekip alternatif program sundu, iletişimleri iyiydi.", 4, 3, 3, 3, ISODate("2026-06-02T11:45:00Z"), false),

  review(efes, "Zeynep Kaya", "Efes'te rehberin anlatımı çok doyurucuydu. Şirince'de serbest zaman biraz kısa geldi.", 5, 4, 4, 4, ISODate("2026-04-28T13:20:00Z"), true),
  review(efes, "Mert Aydın", "Fiyat/performans olarak gayet başarılı bir tur, otobüs konforluydu.", 5, 4, 5, 5, ISODate("2026-05-03T08:10:00Z"), true),

  review(karadeniz, "Selin Arslan", "Yaylalarda sis manzarası inanılmazdı. Yollar virajlı, aracı tutanlar ilaç almayı unutmasın.", 5, 5, 3, 5, ISODate("2026-06-11T18:40:00Z"), true),
  review(karadeniz, "Onur Çetin", "Program yoğundu, dinlenmeye pek vakit kalmadı. Doğa harikaydı ama tempo düşürülebilir.", 4, 3, 3, 2, ISODate("2026-06-19T10:15:00Z"), true),

  review(fethiye, "Deniz Koç", "Tekne turu muhteşemdi, koylar tertemizdi. Öğle yemeği de dahil, ekstra ödeme çıkmadı.", 5, 5, 5, 5, ISODate("2026-06-25T14:00:00Z"), true),
  review(fethiye, "Gamze Polat", "Yamaç paraşütü opsiyonu ayrı ücretliydi, açıklamada daha net belirtilse iyi olur.", 4, 4, 4, 4, ISODate("2026-07-01T09:05:00Z"), false),

  review(istanbul, "Kerem Doğan", "Tek günde çok yer gezdik, rehber zamanı iyi yönetti. Kapalıçarşı için ekstra zaman isterdim.", 5, 3, 4, 4, ISODate("2026-03-17T17:30:00Z"), true),
  review(istanbul, "İrem Yıldız", "Boğaz'daki akşam yemeği turun en güzel kısmıydı. Ayasofya'da sıra biraz uzundu.", 5, 4, 4, 5, ISODate("2026-03-29T20:10:00Z"), true),

  review(uludag, "Caner Öztürk", "Eğitmen desteği yeni başlayanlar için çok işe yaradı. Ekipman kiralama biraz pahalı.", 5, 4, 4, 4, ISODate("2026-02-08T12:25:00Z"), true),
]);

// Reservations
// Kontenjan kontrolu icin: Karadeniz turu (kapasite 16) bilerek dolmaya yakin birakildi.
function reservation(tourId, name, email, phone, personCount, date, note, status) {
  return {
    NameSurname: name,
    Email: email,
    Phone: phone,
    PersonCount: NumberInt(personCount),
    ReservationDate: date,
    Note: note,
    ReservationStatus: status,
    TourId: tourId.toString(),
  };
}

vitour.Reservations.insertMany([
  reservation(kapadokya, "Ahmet Yılmaz", "ahmet.yilmaz@example.com", "0532 111 22 33", 2, ISODate("2026-05-10T10:00:00Z"), "Balon turu opsiyonu isteniyor.", "Onaylandı"),
  reservation(kapadokya, "Elif Demir", "elif.demir@example.com", "0533 222 33 44", 3, ISODate("2026-05-18T14:30:00Z"), "", "Onaylandı"),
  reservation(kapadokya, "Burak Şahin", "burak.sahin@example.com", "0534 333 44 55", 2, ISODate("2026-06-01T09:15:00Z"), "Vejetaryen menü talebi.", "Onay Bekliyor"),

  reservation(efes, "Zeynep Kaya", "zeynep.kaya@example.com", "0535 444 55 66", 4, ISODate("2026-04-25T11:20:00Z"), "", "Onaylandı"),
  reservation(efes, "Mert Aydın", "mert.aydin@example.com", "0536 555 66 77", 2, ISODate("2026-05-01T16:45:00Z"), "Şarap tadımına katılmayacağız.", "Onaylandı"),

  // Karadeniz: kapasite 16, iptal harici toplam 14 kisi -> sadece 2 kisilik yer kaldi
  reservation(karadeniz, "Selin Arslan", "selin.arslan@example.com", "0537 666 77 88", 6, ISODate("2026-06-05T08:00:00Z"), "Aile grubu.", "Onaylandı"),
  reservation(karadeniz, "Onur Çetin", "onur.cetin@example.com", "0538 777 88 99", 5, ISODate("2026-06-12T13:10:00Z"), "", "Onaylandı"),
  reservation(karadeniz, "Hakan Uz", "hakan.uz@example.com", "0539 888 99 00", 3, ISODate("2026-06-15T17:25:00Z"), "Yayla evinde konaklama tercihi.", "Onay Bekliyor"),
  reservation(karadeniz, "Pelin Ak", "pelin.ak@example.com", "0530 999 00 11", 4, ISODate("2026-06-16T12:00:00Z"), "Vazgeçildi.", "İptal Edildi"),

  reservation(fethiye, "Deniz Koç", "deniz.koc@example.com", "0531 121 31 41", 2, ISODate("2026-06-20T10:40:00Z"), "Yamaç paraşütü eklenecek.", "Onaylandı"),
  reservation(fethiye, "Gamze Polat", "gamze.polat@example.com", "0532 151 61 71", 3, ISODate("2026-06-28T15:55:00Z"), "", "Onay Bekliyor"),

  reservation(istanbul, "Kerem Doğan", "kerem.dogan@example.com", "0533 181 91 01", 2, ISODate("2026-03-15T09:30:00Z"), "", "Onaylandı"),
  reservation(istanbul, "İrem Yıldız", "irem.yildiz@example.com", "0534 212 22 32", 5, ISODate("2026-03-25T18:20:00Z"), "Grup indirimi sorulacak.", "Onaylandı"),

  reservation(uludag, "Caner Öztürk", "caner.ozturk@example.com", "0535 242 52 62", 2, ISODate("2026-02-01T11:05:00Z"), "Ekipman kiralama dahil.", "Onaylandı"),
]);

// Ek turlar: toplam 12 yayindaki tur => sayfalamada 2 sayfa (sayfa basina 6)

const desAntalya   = ObjectId("670000000000000000000007");
const desCanakkale = ObjectId("670000000000000000000008");
const desMardin    = ObjectId("670000000000000000000009");
const desKars      = ObjectId("67000000000000000000000a");
const desAyvalik   = ObjectId("67000000000000000000000b");
const desKas       = ObjectId("67000000000000000000000c");

vitour.Destinations.insertMany([
  { _id: desAntalya,   City: "Antalya",   Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/antalya.jpg", DestinationStatus: true },
  { _id: desCanakkale, City: "Çanakkale", Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/canakkale.jpg", DestinationStatus: true },
  { _id: desMardin,    City: "Mardin",    Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/mardin.jpg", DestinationStatus: true },
  { _id: desKars,      City: "Kars",      Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/kars.jpg", DestinationStatus: true },
  { _id: desAyvalik,   City: "Ayvalık",   Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/ayvalik.jpg", DestinationStatus: true },
  { _id: desKas,       City: "Kaş",       Country: "Türkiye", ImageUrl: "/vitour/assets/images/tours/kas.jpg", DestinationStatus: true },
]);

const antalya   = ObjectId("660000000000000000000007");
const canakkale = ObjectId("660000000000000000000008");
const mardin    = ObjectId("660000000000000000000009");
const kars      = ObjectId("66000000000000000000000a");
const ayvalik   = ObjectId("66000000000000000000000b");
const kas       = ObjectId("66000000000000000000000c");

vitour.Tours.insertMany([
  {
    _id: antalya,
    Title: "Antalya Antik Kentler Turu",
    Description: "Perge, Aspendos ve Side antik kentleri rehberli gezi. Düden Şelalesi ve Kaleiçi'nde serbest zaman.",
    CoverImageUrl: "/vitour/assets/images/tours/antalya.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/antalya.jpg",
    Badge: "Popular",
    DayCount: NumberInt(3),
    Capacity: NumberInt(28),
    Price: NumberDecimal("5900"),
    IsStatus: true,
    CategoryId: catKultur.toString(),
    DestinationId: desAntalya.toString(),
  },
  {
    _id: canakkale,
    Title: "Çanakkale ve Truva Turu",
    Description: "Gelibolu Yarımadası şehitlikleri, Anzak Koyu ve Truva Antik Kenti. Rehber eşliğinde tam gün program.",
    CoverImageUrl: "/vitour/assets/images/tours/canakkale.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/canakkale.jpg",
    Badge: "",
    DayCount: NumberInt(2),
    Capacity: NumberInt(35),
    Price: NumberDecimal("3400"),
    IsStatus: true,
    CategoryId: catKultur.toString(),
    DestinationId: desCanakkale.toString(),
  },
  {
    _id: mardin,
    Title: "Mardin & Midyat Taş Evler Turu",
    Description: "Mardin eski şehir, Deyrulzafaran Manastırı ve Midyat'ta gümüş işçiliği atölyeleri. Mezopotamya ovası manzarası.",
    CoverImageUrl: "/vitour/assets/images/tours/mardin.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/mardin.jpg",
    Badge: "New",
    DayCount: NumberInt(4),
    Capacity: NumberInt(22),
    Price: NumberDecimal("8300"),
    IsStatus: true,
    CategoryId: catKultur.toString(),
    DestinationId: desMardin.toString(),
  },
  {
    _id: kars,
    Title: "Kars Doğu Ekspresi ve Ani Harabeleri",
    Description: "Doğu Ekspresi ile Kars'a yolculuk, Ani Harabeleri ve Çıldır Gölü'nde atlı kızak deneyimi.",
    CoverImageUrl: "/vitour/assets/images/tours/kars.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/kars.jpg",
    Badge: "Limited",
    DayCount: NumberInt(5),
    Capacity: NumberInt(14),
    Price: NumberDecimal("11200"),
    IsStatus: true,
    CategoryId: catDoga.toString(),
    DestinationId: desKars.toString(),
  },
  {
    _id: ayvalik,
    Title: "Ayvalık & Cunda Adası Turu",
    Description: "Cunda Adası'nda taş sokaklar, Şeytan Sofrası'nda gün batımı ve zeytinyağı üretim tesisi ziyareti.",
    CoverImageUrl: "/vitour/assets/images/tours/ayvalik.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/ayvalik.jpg",
    Badge: "",
    DayCount: NumberInt(2),
    Capacity: NumberInt(26),
    Price: NumberDecimal("4100"),
    IsStatus: true,
    CategoryId: catDeniz.toString(),
    DestinationId: desAyvalik.toString(),
  },
  {
    _id: kas,
    Title: "Kaş Dalış ve Batık Şehir Turu",
    Description: "Kekova batık şehri tekne turu, Kaş'ta tüplü dalış deneyimi ve Simena Kalesi yürüyüşü.",
    CoverImageUrl: "/vitour/assets/images/tours/kas.jpg",
    MapLocationImageUrl: "/vitour/assets/images/map/kas.jpg",
    Badge: "Popular",
    DayCount: NumberInt(3),
    Capacity: NumberInt(20),
    Price: NumberDecimal("7100"),
    IsStatus: true,
    CategoryId: catDeniz.toString(),
    DestinationId: desKas.toString(),
  },
]);

vitour.TourPlans.insertMany([
  plan(antalya, 1, "Perge ve Aspendos", "Antalya'da karşılama, Perge Antik Kenti ve Aspendos Tiyatrosu rehberli gezi."),
  plan(antalya, 2, "Side ve Manavgat", "Side Apollon Tapınağı, Manavgat Şelalesi ve sahilde serbest zaman."),
  plan(antalya, 3, "Kaleiçi ve Dönüş", "Kaleiçi'nde yürüyüş, Düden Şelalesi ve havalimanına transfer."),

  plan(canakkale, 1, "Gelibolu Şehitlikleri", "Anzak Koyu, Conkbayırı ve Şehitler Abidesi rehberli ziyaret."),
  plan(canakkale, 2, "Truva Antik Kenti", "Truva Antik Kenti ve müzesi gezisi, ardından dönüş."),

  plan(mardin, 1, "Mardin Eski Şehir", "Taş evler, Ulu Cami ve çarşıda serbest zaman."),
  plan(mardin, 2, "Deyrulzafaran Manastırı", "Manastır ziyareti ve Mezopotamya ovası manzarası."),
  plan(mardin, 3, "Midyat", "Midyat konakları ve gümüş işçiliği atölyeleri."),
  plan(mardin, 4, "Hasankeyf ve Dönüş", "Hasankeyf ziyareti, ardından havalimanına transfer."),

  plan(kars, 1, "Doğu Ekspresi", "Ankara'dan Doğu Ekspresi ile Kars'a yolculuk."),
  plan(kars, 2, "Kars Şehir Turu", "Kars Kalesi, Kümbet Camii ve peynir tadımı."),
  plan(kars, 3, "Ani Harabeleri", "Ani Ören Yeri rehberli gezi."),
  plan(kars, 4, "Çıldır Gölü", "Donmuş göl üzerinde atlı kızak ve Eskimo usulü balık avı."),
  plan(kars, 5, "Dönüş", "Sarıkamış'ta serbest zaman ve dönüş."),

  plan(ayvalik, 1, "Cunda Adası", "Taş sokaklarda yürüyüş, Taksiyarhis Kilisesi ve sahilde öğle yemeği."),
  plan(ayvalik, 2, "Şeytan Sofrası", "Zeytinyağı tesisi ziyareti ve Şeytan Sofrası'nda gün batımı."),

  plan(kas, 1, "Kaş'a Varış", "Otele yerleşme ve Kaş çarşısında serbest zaman."),
  plan(kas, 2, "Kekova Batık Şehir", "Tekne ile Kekova turu, Simena Kalesi yürüyüşü."),
  plan(kas, 3, "Dalış ve Dönüş", "Eğitmen eşliğinde tüplü dalış, ardından dönüş transferi."),
]);

vitour.TourImages.insertMany([
  img(antalya, "Aspendos Tiyatrosu", "/vitour/assets/images/tours/antalya.jpg"),
  img(antalya, "Kaleiçi", "/vitour/assets/images/tours/antalya.jpg"),
  img(canakkale, "Truva Atı", "/vitour/assets/images/tours/canakkale.jpg"),
  img(canakkale, "Şehitler Abidesi", "/vitour/assets/images/tours/canakkale.jpg"),
  img(mardin, "Mardin Taş Evler", "/vitour/assets/images/tours/mardin.jpg"),
  img(mardin, "Mezopotamya Ovası", "/vitour/assets/images/tours/mardin.jpg"),
  img(kars, "Ani Harabeleri", "/vitour/assets/images/tours/kars.jpg"),
  img(kars, "Çıldır Gölü", "/vitour/assets/images/tours/kars.jpg"),
  img(ayvalik, "Cunda Adası", "/vitour/assets/images/tours/ayvalik.jpg"),
  img(ayvalik, "Şeytan Sofrası", "/vitour/assets/images/tours/ayvalik.jpg"),
  img(kas, "Kekova", "/vitour/assets/images/tours/kas.jpg"),
  img(kas, "Dalış", "/vitour/assets/images/tours/kas.jpg"),
]);

vitour.Reviews.insertMany([
  review(antalya, "Serkan Bulut", "Aspendos'ta akustik gerçekten inanılmaz. Rehberimiz tarihi çok iyi anlattı.", 5, 4, 4, 4, ISODate("2026-05-08T10:00:00Z"), true),
  review(kas, "Ece Turan", "Kekova turu harikaydı, su berraktı. Dalış eğitmeni çok sabırlıydı.", 5, 4, 5, 5, ISODate("2026-06-22T15:30:00Z"), true),
  review(mardin, "Yusuf Erdem", "Mardin'in dokusu büyüleyici. Otel konumu biraz uzaktı ama servis vardı.", 4, 3, 4, 4, ISODate("2026-04-19T12:00:00Z"), true),
]);

vitour.Reservations.insertMany([
  reservation(antalya, "Serkan Bulut", "serkan.bulut@example.com", "0536 313 23 33", 2, ISODate("2026-05-02T09:00:00Z"), "", "Onaylandı"),
  reservation(kas, "Ece Turan", "ece.turan@example.com", "0537 343 53 63", 4, ISODate("2026-06-18T14:00:00Z"), "Dalış paketi dahil.", "Onaylandı"),
  reservation(mardin, "Yusuf Erdem", "yusuf.erdem@example.com", "0538 373 83 93", 3, ISODate("2026-04-12T11:30:00Z"), "", "Onay Bekliyor"),
]);

// Ozet
print("Categories  : " + vitour.Categories.countDocuments({}));
print("Destinations: " + vitour.Destinations.countDocuments({}));
print("Tours       : " + vitour.Tours.countDocuments({}));
print("TourPlans   : " + vitour.TourPlans.countDocuments({}));
print("TourImages  : " + vitour.TourImages.countDocuments({}));
print("Reviews     : " + vitour.Reviews.countDocuments({}));
print("Reservations: " + vitour.Reservations.countDocuments({}));
