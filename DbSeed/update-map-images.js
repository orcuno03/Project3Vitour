// Mevcut veriyi silmeden sadece Tours.MapLocationImageUrl alanlarini doldurur.
// Seed'i bastan calistirmak koleksiyonlari drop() edecegi icin, gorseller
// sonradan eklendiginde bu script kullanilir.
//
//   mongosh "mongodb://localhost:27017" --file DbSeed/update-map-images.js

const vitour = db.getSiblingDB("VitourDb");

const maps = {
  "660000000000000000000001": "kapadokya",
  "660000000000000000000002": "efes",
  "660000000000000000000003": "karadeniz",
  "660000000000000000000004": "fethiye",
  "660000000000000000000005": "istanbul",
  "660000000000000000000006": "uludag",
  "660000000000000000000007": "antalya",
  "660000000000000000000008": "canakkale",
  "660000000000000000000009": "mardin",
  "66000000000000000000000a": "kars",
  "66000000000000000000000b": "ayvalik",
  "66000000000000000000000c": "kas",
};

let updated = 0;
for (const [id, slug] of Object.entries(maps)) {
  const res = vitour.Tours.updateOne(
    { _id: ObjectId(id) },
    { $set: { MapLocationImageUrl: `/vitour/assets/images/map/${slug}.jpg` } }
  );
  updated += res.modifiedCount;
}

print(`Guncellenen tur: ${updated}`);
print(`Hala bos olan: ${vitour.Tours.countDocuments({ MapLocationImageUrl: "" })}`);
