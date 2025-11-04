using AdminHubApi.Data;
using AdminHubApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data.Seeders
{
    public static class ProductSeeder
    {
        public static async Task SeedProductsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                if (!await context.Products.AnyAsync())
                {
                    logger.LogInformation("Seeding products...");

                    var products = new List<Product>
                    {
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Wireless Bluetooth Headphones",
                            Sku = "WBH-001",
                            Description = "Premium wireless headphones with active noise cancellation and 30-hour battery life",
                            Price = 199.99m,
                            CompareAtPrice = 249.99m,
                            CostPrice = 120.00m,
                            StockQuantity = 150,
                            LowStockThreshold = 20,
                            Category = "Electronics",
                            Brand = "AudioTech",
                            ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e",
                            Tags = "audio,wireless,bluetooth,headphones",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Smart Fitness Watch",
                            Sku = "SFW-002",
                            Description = "Track your fitness goals with heart rate monitoring, GPS, and sleep tracking",
                            Price = 299.99m,
                            CompareAtPrice = 349.99m,
                            CostPrice = 180.00m,
                            StockQuantity = 85,
                            LowStockThreshold = 15,
                            Category = "Wearables",
                            Brand = "FitTrack",
                            ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30",
                            Tags = "fitness,smartwatch,health,wearable",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-25)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "USB-C Charging Cable (2m)",
                            Sku = "USBC-003",
                            Description = "Durable braided USB-C cable with fast charging support",
                            Price = 19.99m,
                            CostPrice = 5.00m,
                            StockQuantity = 500,
                            LowStockThreshold = 50,
                            Category = "Accessories",
                            Brand = "TechConnect",
                            ImageUrl = "https://images.unsplash.com/photo-1583863788434-e58a36330cf0",
                            Tags = "cable,usb-c,charging,accessories",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "4K Webcam Pro",
                            Sku = "WCP-004",
                            Description = "Professional 4K webcam with auto-focus and built-in microphone",
                            Price = 149.99m,
                            CompareAtPrice = 189.99m,
                            CostPrice = 85.00m,
                            StockQuantity = 12,
                            LowStockThreshold = 10,
                            Category = "Electronics",
                            Brand = "VisionTech",
                            ImageUrl = "https://images.unsplash.com/photo-1587825140708-dfaf72ae4b04",
                            Tags = "webcam,4k,video,streaming",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-18)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Mechanical Gaming Keyboard",
                            Sku = "MGK-005",
                            Description = "RGB backlit mechanical keyboard with customizable keys and wrist rest",
                            Price = 129.99m,
                            CompareAtPrice = 159.99m,
                            CostPrice = 70.00m,
                            StockQuantity = 45,
                            LowStockThreshold = 15,
                            Category = "Gaming",
                            Brand = "GameGear",
                            ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3",
                            Tags = "keyboard,gaming,mechanical,rgb",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Portable Power Bank 20000mAh",
                            Sku = "PPB-006",
                            Description = "High-capacity power bank with dual USB ports and fast charging",
                            Price = 49.99m,
                            CostPrice = 25.00m,
                            StockQuantity = 200,
                            LowStockThreshold = 30,
                            Category = "Accessories",
                            Brand = "PowerPlus",
                            ImageUrl = "https://images.unsplash.com/photo-1609091839311-d5365f9ff1c5",
                            Tags = "powerbank,charging,portable,battery",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-12)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Laptop Stand Adjustable",
                            Sku = "LSA-007",
                            Description = "Ergonomic aluminum laptop stand with adjustable height and angle",
                            Price = 39.99m,
                            CostPrice = 15.00m,
                            StockQuantity = 120,
                            LowStockThreshold = 20,
                            Category = "Office",
                            Brand = "ErgoDesk",
                            ImageUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46",
                            Tags = "laptop,stand,ergonomic,office",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-10)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Wireless Mouse Ergonomic",
                            Sku = "WME-008",
                            Description = "Ergonomic wireless mouse with adjustable DPI and rechargeable battery",
                            Price = 59.99m,
                            CompareAtPrice = 79.99m,
                            CostPrice = 30.00m,
                            StockQuantity = 8,
                            LowStockThreshold = 10,
                            Category = "Accessories",
                            Brand = "ClickPro",
                            ImageUrl = "https://images.unsplash.com/photo-1527814050087-3793815479db",
                            Tags = "mouse,wireless,ergonomic,accessories",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-8)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Smart LED Desk Lamp",
                            Sku = "SLED-009",
                            Description = "WiFi-enabled LED desk lamp with adjustable brightness and color temperature",
                            Price = 79.99m,
                            CostPrice = 40.00m,
                            StockQuantity = 65,
                            LowStockThreshold = 15,
                            Category = "Office",
                            Brand = "LightWorks",
                            ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c",
                            Tags = "lamp,led,smart,office",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-7)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Portable SSD 1TB",
                            Sku = "PSSD-010",
                            Description = "Ultra-fast portable SSD with USB 3.2 Gen 2 and shock resistance",
                            Price = 149.99m,
                            CostPrice = 85.00m,
                            StockQuantity = 55,
                            LowStockThreshold = 10,
                            Category = "Storage",
                            Brand = "DataSpeed",
                            ImageUrl = "https://images.unsplash.com/photo-1531492746076-161ca9bcad58",
                            Tags = "ssd,storage,portable,external",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-5)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Monitor Screen 27 inch 4K",
                            Sku = "MS27-011",
                            Description = "Professional 4K monitor with HDR support and USB-C connectivity",
                            Price = 499.99m,
                            CompareAtPrice = 599.99m,
                            CostPrice = 300.00m,
                            StockQuantity = 28,
                            LowStockThreshold = 5,
                            Category = "Electronics",
                            Brand = "ViewPro",
                            ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf",
                            Tags = "monitor,4k,display,screen",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-3)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Smartphone Case Clear",
                            Sku = "SCC-012",
                            Description = "Crystal clear protective case with military-grade drop protection",
                            Price = 24.99m,
                            CostPrice = 8.00m,
                            StockQuantity = 300,
                            LowStockThreshold = 40,
                            Category = "Accessories",
                            Brand = "ShieldCase",
                            ImageUrl = "https://images.unsplash.com/photo-1601593346740-925612772716",
                            Tags = "case,phone,protection,accessories",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow.AddDays(-2)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Bluetooth Speaker Portable",
                            Sku = "BSP-013",
                            Description = "Waterproof Bluetooth speaker with 360-degree sound and 12-hour battery",
                            Price = 89.99m,
                            CompareAtPrice = 119.99m,
                            CostPrice = 45.00m,
                            StockQuantity = 95,
                            LowStockThreshold = 20,
                            Category = "Audio",
                            Brand = "SoundWave",
                            ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1",
                            Tags = "speaker,bluetooth,portable,audio",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-1)
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Webcam Privacy Cover Set",
                            Sku = "WPC-014",
                            Description = "Pack of 6 ultra-thin sliding webcam covers for laptops and tablets",
                            Price = 9.99m,
                            CostPrice = 2.00m,
                            StockQuantity = 5,
                            LowStockThreshold = 10,
                            Category = "Security",
                            Brand = "PrivacyGuard",
                            ImageUrl = "https://images.unsplash.com/photo-1614624532983-4ce03382d63d",
                            Tags = "privacy,webcam,security,accessories",
                            IsActive = true,
                            IsFeatured = false,
                            CreatedAt = DateTime.UtcNow
                        },
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Graphics Tablet Digital",
                            Sku = "GTD-015",
                            Description = "Professional drawing tablet with 8192 pressure levels and tilt support",
                            Price = 349.99m,
                            CostPrice = 200.00m,
                            StockQuantity = 32,
                            LowStockThreshold = 8,
                            Category = "Creative",
                            Brand = "ArtTech",
                            ImageUrl = "https://images.unsplash.com/photo-1612815154858-60aa4c59eaa6",
                            Tags = "tablet,drawing,graphics,creative",
                            IsActive = true,
                            IsFeatured = true,
                            CreatedAt = DateTime.UtcNow
                        }
                    };

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully seeded {products.Count} products");
                }
                else
                {
                    logger.LogInformation("Products already seeded, skipping...");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error seeding products");
                throw;
            }
        }
    }
}
