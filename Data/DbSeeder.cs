using SmartVibe.Models;

namespace SmartVibe.Data;

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
                Price = 350,
                OriginalPrice = 450,
                Rating = 4.6m,
                Reviews = 128,
                Badge = "الأكثر مبيعاً",
                Image = "https://images.unsplash.com/photo-1550985616-10810253b84d?w=600&q=80",
                Description = "لمبة إضاءة ذكية تتحكم فيها من موبايلك، تدعم أكثر من 16 مليون لون وتتوافق مع تطبيقات المنزل الذكي.",
                Stock = 42,
                IsActive = true
            },
            new Product
            {
                Name = "كاميرا مراقبة ذكية داخلية",
                Slug = "smart-indoor-camera",
                Category = "أمن وحماية",
                Price = 890,
                OriginalPrice = 1100,
                Rating = 4.4m,
                Reviews = 76,
                Badge = "جديد",
                Image = "https://images.unsplash.com/photo-1557324232-b8917d3c3dcb?w=600&q=80",
                Description = "كاميرا مراقبة داخلية بدقة Full HD، رؤية ليلية، وتنبيهات فورية على الموبايل عند اكتشاف حركة.",
                Stock = 25,
                IsActive = true
            },
            new Product
            {
                Name = "قفل باب ذكي ببصمة الإصبع",
                Slug = "smart-door-lock",
                Category = "أمن وحماية",
                Price = 2200,
                OriginalPrice = null,
                Rating = 4.7m,
                Reviews = 54,
                Badge = null,
                Image = "https://images.unsplash.com/photo-1558002038-1055907df827?w=600&q=80",
                Description = "قفل ذكي يفتح بالبصمة أو الكود أو التطبيق، مناسب للشقق والفلل، بطارية تدوم حتى 6 شهور.",
                Stock = 15,
                IsActive = true
            },
            new Product
            {
                Name = "ماكينة قهوة ذكية بالتحكم عن بعد",
                Slug = "smart-coffee-machine",
                Category = "مطبخ ذكي",
                Price = 1750,
                OriginalPrice = 2000,
                Rating = 4.3m,
                Reviews = 39,
                Badge = "عرض",
                Image = "https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=600&q=80",
                Description = "جهزّ قهوتك من موبايلك قبل ما تصحى، تتحكم في الجرعة ودرجة الحرارة عن بعد.",
                Stock = 18,
                IsActive = true
            },
            new Product
            {
                Name = "شاشة تلفزيون ذكية 55 بوصة",
                Slug = "smart-tv-55",
                Category = "ترفيه ذكي",
                Price = 15800,
                OriginalPrice = 17500,
                Rating = 4.8m,
                Reviews = 203,
                Badge = "الأكثر مبيعاً",
                Image = "https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=600&q=80",
                Description = "شاشة ذكية دقة 4K، تدعم كل تطبيقات المشاهدة، وتتحكم فيها بالصوت.",
                Stock = 9,
                IsActive = true
            },
            new Product
            {
                Name = "مكنسة روبوت ذكية",
                Slug = "smart-robot-vacuum",
                Category = "أجهزة منزلية",
                Price = 4200,
                OriginalPrice = 4900,
                Rating = 4.5m,
                Reviews = 91,
                Badge = "جديد",
                Image = "https://images.unsplash.com/photo-1600166898405-da9535204843?w=600&q=80",
                Description = "مكنسة كهربائية ذكية بتنظف البيت لوحدها وتتحكم فيها بالتطبيق أو الصوت.",
                Stock = 12,
                IsActive = true
            }
        };

        db.Products.AddRange(products);
    }
}
