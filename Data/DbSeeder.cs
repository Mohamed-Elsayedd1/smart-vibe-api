using SmartVibe.Models;

namespace SmartVibe.Data;

/// <summary>
/// بيضيف بيانات تجريبية (Demo Data) لو الداتا بيز فاضية.
/// كده الموقع بيفضل شغال وفيه منتجات حتى لو حد نسي يضيف بيانات حقيقية،
/// أو لو الداتا بيز اتمسحت / اشتراك Railway خلص وبدأنا من جديد.
/// </summary>
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        SeedCategories(db);
        SeedProducts(db);
        db.SaveChanges();
    }

    private static void SeedCategories(AppDbContext db)
    {
        if (db.Categories.Any()) return;

        db.Categories.AddRange(
            new Category { Name = "إضاءة ذكية", Slug = "smart-lighting", Icon = "💡", DisplayOrder = 1 },
            new Category { Name = "أمن وحماية", Slug = "security", Icon = "🛡️", DisplayOrder = 2 },
            new Category { Name = "مطبخ ذكي", Slug = "smart-kitchen", Icon = "🍳", DisplayOrder = 3 },
            new Category { Name = "ترفيه ذكي", Slug = "entertainment", Icon = "🎬", DisplayOrder = 4 },
            new Category { Name = "أجهزة منزلية", Slug = "home-appliances", Icon = "🏠", DisplayOrder = 5 }
        );
    }

    private static void SeedProducts(AppDbContext db)
    {
        if (db.Products.Any()) return;

        var products = new[]
        {
            new Product
            {
                Name = "لمبة LED ذكية متعددة الألوان",
                Slug = "smart-led-bulb",
                Category = "إضاءة ذكية",
                Price = 171,
                OriginalPrice = 220,
                Rating = 4.6m,
                Reviews = 128,
                Badge = "عرض",
                Image = "https://m.media-amazon.com/images/I/71Qfjiyb9+L._AC_SL1500_.jpg",
                Images = "[\"https://m.media-amazon.com/images/I/61lk-wJa52L._AC_SL1000_.jpg\",\"https://m.media-amazon.com/images/I/41ndkKsN43L._AC_.jpg\",\"https://m.media-amazon.com/images/I/518q-7yzV7L._AC_SL1024_.jpg\"]",
                Description = "لمبة إضاءة ذكية تتحكم فيها من موبايلك، تدعم أكثر من 16 مليون لون وتتوافق مع تطبيقات المنزل الذكي.",
                Stock = 42,
                IsActive = true
            },
            new Product
            {
                Name = "كاميرا مراقبة ذكية داخلية",
                Slug = "smart-indoor-camera",
                Category = "أمن وحماية",
                Price = 899,
                OriginalPrice = 1050,
                Rating = 4.4m,
                Reviews = 76,
                Badge = "الأكثر مبيعاً",
                Image = "https://m.media-amazon.com/images/I/51w0Gsm-XbL._AC_SL1000_.jpg",
                Images = "[\"https://m.media-amazon.com/images/I/71QpbOVaP5L._AC_SL1200_.jpg\",\"https://m.media-amazon.com/images/I/61qEhA5weLL._AC_SL1200_.jpg\",\"https://m.media-amazon.com/images/I/61Ox4w-3gHL._AC_SL1200_.jpg\"]",
                Description = "كاميرا مراقبة داخلية بدقة Full HD، رؤية ليلية، وتنبيهات فورية على الموبايل عند اكتشاف حركة.",
                Stock = 25,
                IsActive = true
            },
            new Product
            {
                Name = "قفل باب ذكي ببصمة الإصبع",
                Slug = "smart-door-lock",
                Category = "أمن وحماية",
                Price = 5853,
                OriginalPrice = null,
                Rating = 4.7m,
                Reviews = 54,
                Badge = "جديد",
                Image = "https://m.media-amazon.com/images/I/61HR9a0L0aL._AC_SL1500_.jpg",
                Images = "[\"https://m.media-amazon.com/images/I/61GrVIMCjkL._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/71MRtvdrBbL._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/71PaD6wa+dL._AC_SL1500_.jpg\"]",
                Description = "قفل ذكي يفتح بالبصمة أو الكود أو التطبيق، مناسب للشقق والفلل، بطارية تدوم حتى 6 شهور.",
                Stock = 15,
                IsActive = true
            },
            new Product
            {
                Name = "ماكينة قهوة ذكية بالتحكم عن بعد",
                Slug = "smart-coffee-machine",
                Category = "مطبخ ذكي",
                Price = 100114,
                OriginalPrice = 115000,
                Rating = 4.3m,
                Reviews = 39,
                Badge = "عرض",
                Image = "https://cdn.shopify.com/s/files/1/1032/4987/1193/files/meraki-espresso-machine-3668289.jpg?v=1782860778",
                Images = "[\"https://cdn.shopify.com/s/files/1/1032/4987/1193/files/2gen2_640e2bd7-3d6f-4e15-a086-ac290a961686.jpg?v=1782860778\",\"https://cdn.shopify.com/s/files/1/1032/4987/1193/files/6_d64dda39-6e99-4bdf-83e3-0e9cba633b4f.png?v=1782860779\",\"https://cdn.shopify.com/s/files/1/1032/4987/1193/files/22_2d924caa-0a18-4167-8046-df817d74692a.png?v=1782860779\"]",
                Description = "جهزّ قهوتك من موبايلك قبل ما تصحى، تتحكم في الجرعة ودرجة الحرارة عن بعد.",
                Stock = 18,
                IsActive = true
            },
            new Product
            {
                Name = "شاشة تلفزيون ذكية 55 بوصة",
                Slug = "smart-tv-55",
                Category = "ترفيه ذكي",
                Price = 26999,
                OriginalPrice = 31999,
                Rating = 4.8m,
                Reviews = 203,
                Badge = "الأكثر مبيعاً",
                Image = "https://m.media-amazon.com/images/I/916CAVCYdxL._AC_SL1500_.jpg",
                Images = "[\"https://m.media-amazon.com/images/I/91BDwkv3haL._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/61d2BmdKUsL._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/81it02JdRSL._AC_SL1500_.jpg\"]",
                Description = "شاشة ذكية دقة 4K، تدعم كل تطبيقات المشاهدة، وتتحكم فيها بالصوت.",
                Stock = 9,
                IsActive = true
            },
            new Product
            {
                Name = "مكنسة روبوت ذكية",
                Slug = "smart-robot-vacuum",
                Category = "أجهزة منزلية",
                Price = 6889.15m,
                OriginalPrice = 7999,
                Rating = 4.5m,
                Reviews = 91,
                Badge = "جديد",
                Image = "https://m.media-amazon.com/images/I/619L+4ky64L._AC_SL1500_.jpg",
                Images = "[\"https://m.media-amazon.com/images/I/61vcSjph50L._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/71BW8YKm6eL._AC_SL1500_.jpg\",\"https://m.media-amazon.com/images/I/61sNUa0cEzL._AC_SL1153_.jpg\"]",
                Description = "مكنسة كهربائية ذكية بتنظف البيت لوحدها وتتحكم فيها بالتطبيق أو الصوت.",
                Stock = 12,
                IsActive = true
            }
        };

        db.Products.AddRange(products);
    }
}
