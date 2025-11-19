using AdminHubApi.Data;
using AdminHubApi.Entities.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data.Seeders
{
    public static class AntdDataSeeder
    {
        public static async Task SeedAntdDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                // Seed Antd Projects (10 rows)
                if (!await context.AntdProjects.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd projects data...");
                    var projects = new List<AntdProject>
                    {
                        new AntdProject
                        {
                            ProjectName = "E-Commerce Platform Redesign",
                            StartDate = DateTime.UtcNow.AddDays(-60),
                            EndDate = DateTime.UtcNow.AddDays(30),
                            Budget = "USD",
                            ProjectManager = "John Smith",
                            ClientName = "TechCorp Inc",
                            Status = "in progress",
                            Priority = "high",
                            TeamSize = 12,
                            ProjectDescription = "Complete redesign of the e-commerce platform with modern UI/UX, improved performance, and mobile-first approach.",
                            ProjectLocation = "New York",
                            ProjectType = "development",
                            ProjectCategory = "enterprise",
                            ProjectDuration = 3.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Mobile Banking App",
                            StartDate = DateTime.UtcNow.AddDays(-30),
                            EndDate = DateTime.UtcNow.AddDays(90),
                            Budget = "EUR",
                            ProjectManager = "Sarah Johnson",
                            ClientName = "FinanceFirst Bank",
                            Status = "in progress",
                            Priority = "high",
                            TeamSize = 8,
                            ProjectDescription = "Development of a secure mobile banking application with biometric authentication and real-time transactions.",
                            ProjectLocation = "London",
                            ProjectType = "development",
                            ProjectCategory = "finance",
                            ProjectDuration = 4.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Healthcare Portal Migration",
                            StartDate = DateTime.UtcNow.AddDays(-90),
                            EndDate = DateTime.UtcNow.AddDays(-10),
                            Budget = "USD",
                            ProjectManager = "Michael Chen",
                            ClientName = "MedCare Solutions",
                            Status = "completed",
                            Priority = "medium",
                            TeamSize = 6,
                            ProjectDescription = "Migration of legacy healthcare portal to cloud infrastructure with enhanced security compliance.",
                            ProjectLocation = "San Francisco",
                            ProjectType = "migration",
                            ProjectCategory = "healthcare",
                            ProjectDuration = 2.5m
                        },
                        new AntdProject
                        {
                            ProjectName = "Marketing Campaign Dashboard",
                            StartDate = DateTime.UtcNow.AddDays(-15),
                            EndDate = DateTime.UtcNow.AddDays(45),
                            Budget = "GBP",
                            ProjectManager = "Emily Davis",
                            ClientName = "AdVenture Media",
                            Status = "in progress",
                            Priority = "medium",
                            TeamSize = 4,
                            ProjectDescription = "Real-time marketing analytics dashboard with campaign tracking, ROI calculation, and automated reporting.",
                            ProjectLocation = "Manchester",
                            ProjectType = "marketing",
                            ProjectCategory = "advertising",
                            ProjectDuration = 2.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Government Document System",
                            StartDate = DateTime.UtcNow.AddDays(-120),
                            EndDate = DateTime.UtcNow.AddDays(60),
                            Budget = "USD",
                            ProjectManager = "Robert Wilson",
                            ClientName = "State Department",
                            Status = "on hold",
                            Priority = "high",
                            TeamSize = 15,
                            ProjectDescription = "Secure document management system for government agencies with audit trails and compliance reporting.",
                            ProjectLocation = "Washington DC",
                            ProjectType = "development",
                            ProjectCategory = "government",
                            ProjectDuration = 6.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Inventory Management System",
                            StartDate = DateTime.UtcNow.AddDays(-45),
                            EndDate = DateTime.UtcNow.AddDays(15),
                            Budget = "CAD",
                            ProjectManager = "Lisa Anderson",
                            ClientName = "RetailMax",
                            Status = "in progress",
                            Priority = "medium",
                            TeamSize = 5,
                            ProjectDescription = "Automated inventory tracking with barcode scanning, stock alerts, and supplier integration.",
                            ProjectLocation = "Toronto",
                            ProjectType = "development",
                            ProjectCategory = "retail",
                            ProjectDuration = 2.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Employee Training Platform",
                            StartDate = DateTime.UtcNow.AddDays(-10),
                            EndDate = DateTime.UtcNow.AddDays(80),
                            Budget = "USD",
                            ProjectManager = "David Brown",
                            ClientName = "GlobalTech Corp",
                            Status = "in progress",
                            Priority = "low",
                            TeamSize = 7,
                            ProjectDescription = "Interactive e-learning platform with video courses, quizzes, certifications, and progress tracking.",
                            ProjectLocation = "Chicago",
                            ProjectType = "development",
                            ProjectCategory = "education",
                            ProjectDuration = 3.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Supply Chain Analytics",
                            StartDate = DateTime.UtcNow.AddDays(-75),
                            EndDate = DateTime.UtcNow.AddDays(-5),
                            Budget = "EUR",
                            ProjectManager = "Anna Martinez",
                            ClientName = "LogiFlow Systems",
                            Status = "completed",
                            Priority = "high",
                            TeamSize = 9,
                            ProjectDescription = "Advanced analytics platform for supply chain optimization with predictive modeling and demand forecasting.",
                            ProjectLocation = "Berlin",
                            ProjectType = "analytics",
                            ProjectCategory = "logistics",
                            ProjectDuration = 2.3m
                        },
                        new AntdProject
                        {
                            ProjectName = "Customer Support Chatbot",
                            StartDate = DateTime.UtcNow.AddDays(-20),
                            EndDate = DateTime.UtcNow.AddDays(40),
                            Budget = "USD",
                            ProjectManager = "James Taylor",
                            ClientName = "ServicePro Inc",
                            Status = "in progress",
                            Priority = "medium",
                            TeamSize = 3,
                            ProjectDescription = "AI-powered chatbot for customer support with natural language processing and ticket escalation.",
                            ProjectLocation = "Austin",
                            ProjectType = "ai",
                            ProjectCategory = "customer service",
                            ProjectDuration = 2.0m
                        },
                        new AntdProject
                        {
                            ProjectName = "Real Estate Portal",
                            StartDate = DateTime.UtcNow.AddDays(-5),
                            EndDate = DateTime.UtcNow.AddDays(120),
                            Budget = "AUD",
                            ProjectManager = "Sophie Lee",
                            ClientName = "PropertyHub",
                            Status = "in progress",
                            Priority = "high",
                            TeamSize = 10,
                            ProjectDescription = "Comprehensive real estate listing platform with virtual tours, mortgage calculator, and agent management.",
                            ProjectLocation = "Sydney",
                            ProjectType = "development",
                            ProjectCategory = "real estate",
                            ProjectDuration = 4.0m
                        }
                    };

                    context.AntdProjects.AddRange(projects);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd projects data seeded successfully");
                }

                // Seed Antd Clients (10 rows)
                if (!await context.AntdClients.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd clients data...");
                    var clients = new List<AntdClient>
                    {
                        new AntdClient
                        {
                            FirstName = "Twyla",
                            LastName = "Leggett",
                            Email = "tleggett@techcorp.com",
                            PhoneNumber = "839-632-0876",
                            PurchaseDate = DateTime.UtcNow.AddDays(-180),
                            ProductName = "Enterprise Software License",
                            Quantity = 50,
                            UnitPrice = 299.99m,
                            TotalPrice = 14999.50m,
                            Country = "United States"
                        },
                        new AntdClient
                        {
                            FirstName = "Marcus",
                            LastName = "Chen",
                            Email = "mchen@globalfinance.com",
                            PhoneNumber = "415-555-0198",
                            PurchaseDate = DateTime.UtcNow.AddDays(-120),
                            ProductName = "Cloud Storage Plan",
                            Quantity = 100,
                            UnitPrice = 49.99m,
                            TotalPrice = 4999.00m,
                            Country = "Canada"
                        },
                        new AntdClient
                        {
                            FirstName = "Sophie",
                            LastName = "Williams",
                            Email = "swilliams@eurotech.eu",
                            PhoneNumber = "44-20-7946-0958",
                            PurchaseDate = DateTime.UtcNow.AddDays(-90),
                            ProductName = "Security Suite",
                            Quantity = 25,
                            UnitPrice = 199.99m,
                            TotalPrice = 4999.75m,
                            Country = "United Kingdom"
                        },
                        new AntdClient
                        {
                            FirstName = "Hiroshi",
                            LastName = "Tanaka",
                            Email = "htanaka@tokyosoft.jp",
                            PhoneNumber = "81-3-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-60),
                            ProductName = "Developer Tools",
                            Quantity = 15,
                            UnitPrice = 599.99m,
                            TotalPrice = 8999.85m,
                            Country = "Japan"
                        },
                        new AntdClient
                        {
                            FirstName = "Elena",
                            LastName = "Rodriguez",
                            Email = "erodriguez@latamtech.mx",
                            PhoneNumber = "52-55-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-45),
                            ProductName = "Database License",
                            Quantity = 10,
                            UnitPrice = 899.99m,
                            TotalPrice = 8999.90m,
                            Country = "Mexico"
                        },
                        new AntdClient
                        {
                            FirstName = "Oliver",
                            LastName = "Schmidt",
                            Email = "oschmidt@berlindata.de",
                            PhoneNumber = "49-30-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-30),
                            ProductName = "Analytics Platform",
                            Quantity = 5,
                            UnitPrice = 1499.99m,
                            TotalPrice = 7499.95m,
                            Country = "Germany"
                        },
                        new AntdClient
                        {
                            FirstName = "Priya",
                            LastName = "Patel",
                            Email = "ppatel@mumbaitech.in",
                            PhoneNumber = "91-22-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-20),
                            ProductName = "API Gateway",
                            Quantity = 3,
                            UnitPrice = 2999.99m,
                            TotalPrice = 8999.97m,
                            Country = "India"
                        },
                        new AntdClient
                        {
                            FirstName = "Lucas",
                            LastName = "Silva",
                            Email = "lsilva@brazilsoft.br",
                            PhoneNumber = "55-11-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-15),
                            ProductName = "Monitoring Tools",
                            Quantity = 20,
                            UnitPrice = 149.99m,
                            TotalPrice = 2999.80m,
                            Country = "Brazil"
                        },
                        new AntdClient
                        {
                            FirstName = "Emma",
                            LastName = "Dubois",
                            Email = "edubois@paristech.fr",
                            PhoneNumber = "33-1-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-10),
                            ProductName = "Collaboration Suite",
                            Quantity = 75,
                            UnitPrice = 79.99m,
                            TotalPrice = 5999.25m,
                            Country = "France"
                        },
                        new AntdClient
                        {
                            FirstName = "James",
                            LastName = "O'Brien",
                            Email = "jobrien@sydneydata.au",
                            PhoneNumber = "61-2-1234-5678",
                            PurchaseDate = DateTime.UtcNow.AddDays(-5),
                            ProductName = "Backup Solution",
                            Quantity = 30,
                            UnitPrice = 249.99m,
                            TotalPrice = 7499.70m,
                            Country = "Australia"
                        }
                    };

                    context.AntdClients.AddRange(clients);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd clients data seeded successfully");
                }

                // Seed Antd Products (10 rows)
                if (!await context.AntdProducts.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd products data...");
                    var products = new List<AntdProduct>
                    {
                        new AntdProduct { ProductName = "Wireless Bluetooth Headphones", Brand = "AudioTech", Price = 89.99m, QuantitySold = 1523, Category = "electronics", ExpirationDate = null, CustomerReviews = 892, AverageRating = 4.5m, IsFeatured = true, ImageUrl = "" },
                        new AntdProduct { ProductName = "Organic Green Tea", Brand = "NatureBrew", Price = 24.99m, QuantitySold = 2341, Category = "food", ExpirationDate = DateTime.UtcNow.AddMonths(12), CustomerReviews = 567, AverageRating = 4.2m, IsFeatured = true, ImageUrl = "" },
                        new AntdProduct { ProductName = "Cotton Bath Towel Set", Brand = "HomeComfort", Price = 45.99m, QuantitySold = 987, Category = "household", ExpirationDate = null, CustomerReviews = 234, AverageRating = 4.0m, IsFeatured = false, ImageUrl = "" },
                        new AntdProduct { ProductName = "Stainless Steel Water Bottle", Brand = "HydroLife", Price = 29.99m, QuantitySold = 1876, Category = "household", ExpirationDate = null, CustomerReviews = 445, AverageRating = 4.7m, IsFeatured = true, ImageUrl = "" },
                        new AntdProduct { ProductName = "Vitamin D Supplements", Brand = "HealthPlus", Price = 19.99m, QuantitySold = 3245, Category = "health", ExpirationDate = DateTime.UtcNow.AddMonths(18), CustomerReviews = 678, AverageRating = 4.3m, IsFeatured = false, ImageUrl = "" },
                        new AntdProduct { ProductName = "LED Desk Lamp", Brand = "BrightLight", Price = 54.99m, QuantitySold = 765, Category = "electronics", ExpirationDate = null, CustomerReviews = 189, AverageRating = 4.1m, IsFeatured = false, ImageUrl = "" },
                        new AntdProduct { ProductName = "Organic Honey", Brand = "BeeNatural", Price = 15.99m, QuantitySold = 2890, Category = "food", ExpirationDate = DateTime.UtcNow.AddMonths(24), CustomerReviews = 723, AverageRating = 4.8m, IsFeatured = true, ImageUrl = "" },
                        new AntdProduct { ProductName = "Yoga Mat", Brand = "FlexFit", Price = 35.99m, QuantitySold = 1234, Category = "fitness", ExpirationDate = null, CustomerReviews = 456, AverageRating = 4.4m, IsFeatured = false, ImageUrl = "" },
                        new AntdProduct { ProductName = "Coffee Maker", Brand = "BrewMaster", Price = 79.99m, QuantitySold = 654, Category = "electronics", ExpirationDate = null, CustomerReviews = 321, AverageRating = 4.6m, IsFeatured = true, ImageUrl = "" },
                        new AntdProduct { ProductName = "Almond Butter", Brand = "NutriSpread", Price = 12.99m, QuantitySold = 1567, Category = "food", ExpirationDate = DateTime.UtcNow.AddMonths(6), CustomerReviews = 289, AverageRating = 4.0m, IsFeatured = false, ImageUrl = "" }
                    };
                    context.AntdProducts.AddRange(products);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd products data seeded successfully");
                }

                // Seed Antd Sellers (10 rows)
                if (!await context.AntdSellers.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd sellers data...");
                    var sellers = new List<AntdSeller>
                    {
                        new AntdSeller { FirstName = "Elysee", LastName = "Tomadoni", Age = 55, Email = "etomadoni@sales.com", Country = "Canada", PostalCode = "G6E", FavoriteColor = "green", SalesVolume = 514474.35m, TotalSales = 882329.71m, CustomerSatisfaction = 85.76m, SalesRegion = "North America" },
                        new AntdSeller { FirstName = "Thain", LastName = "Moro", Age = 36, Email = "tmoro@sales.com", Country = "Portugal", PostalCode = "3550-053", FavoriteColor = "blue", SalesVolume = 326044.12m, TotalSales = 544544.06m, CustomerSatisfaction = 90.57m, SalesRegion = "Europe" },
                        new AntdSeller { FirstName = "Maria", LastName = "Santos", Age = 42, Email = "msantos@sales.com", Country = "Brazil", PostalCode = "01310-100", FavoriteColor = "red", SalesVolume = 445678.90m, TotalSales = 723456.78m, CustomerSatisfaction = 88.34m, SalesRegion = "South America" },
                        new AntdSeller { FirstName = "James", LastName = "Wilson", Age = 38, Email = "jwilson@sales.com", Country = "USA", PostalCode = "10001", FavoriteColor = "blue", SalesVolume = 612345.67m, TotalSales = 945678.90m, CustomerSatisfaction = 92.15m, SalesRegion = "North America" },
                        new AntdSeller { FirstName = "Yuki", LastName = "Tanaka", Age = 31, Email = "ytanaka@sales.com", Country = "Japan", PostalCode = "100-0001", FavoriteColor = "purple", SalesVolume = 378901.23m, TotalSales = 612345.67m, CustomerSatisfaction = 94.28m, SalesRegion = "Asia" },
                        new AntdSeller { FirstName = "Anna", LastName = "Mueller", Age = 45, Email = "amueller@sales.com", Country = "Germany", PostalCode = "10115", FavoriteColor = "green", SalesVolume = 489012.34m, TotalSales = 789012.34m, CustomerSatisfaction = 87.91m, SalesRegion = "Europe" },
                        new AntdSeller { FirstName = "Chen", LastName = "Wei", Age = 29, Email = "cwei@sales.com", Country = "China", PostalCode = "100000", FavoriteColor = "red", SalesVolume = 567890.12m, TotalSales = 890123.45m, CustomerSatisfaction = 89.67m, SalesRegion = "Asia" },
                        new AntdSeller { FirstName = "Sarah", LastName = "Johnson", Age = 33, Email = "sjohnson@sales.com", Country = "Australia", PostalCode = "2000", FavoriteColor = "yellow", SalesVolume = 423456.78m, TotalSales = 678901.23m, CustomerSatisfaction = 91.43m, SalesRegion = "Oceania" },
                        new AntdSeller { FirstName = "Ahmed", LastName = "Hassan", Age = 40, Email = "ahassan@sales.com", Country = "UAE", PostalCode = "00000", FavoriteColor = "gold", SalesVolume = 534567.89m, TotalSales = 812345.67m, CustomerSatisfaction = 86.52m, SalesRegion = "Middle East" },
                        new AntdSeller { FirstName = "Emma", LastName = "Brown", Age = 27, Email = "ebrown@sales.com", Country = "UK", PostalCode = "SW1A 1AA", FavoriteColor = "pink", SalesVolume = 398765.43m, TotalSales = 645678.90m, CustomerSatisfaction = 93.78m, SalesRegion = "Europe" }
                    };
                    context.AntdSellers.AddRange(sellers);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd sellers data seeded successfully");
                }

                // Seed Antd Orders (10 rows)
                if (!await context.AntdOrders.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd orders data...");
                    var orders = new List<AntdOrder>
                    {
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 57, Price = 453.42m, OrderDate = DateTime.UtcNow.AddDays(-30), ShippingAddress = "123 Main St", City = "New York", State = "NY", PostalCode = "10001", Country = "USA", PaymentMethod = "credit card", Status = "delivered", TrackingNumber = 6663178901, ShippingCost = 25.77m, Tax = 42.99m, ProductName = "Wireless Headphones", CustomerName = "John Smith" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 20, Price = 652.79m, OrderDate = DateTime.UtcNow.AddDays(-25), ShippingAddress = "456 Oak Ave", City = "Los Angeles", State = "CA", PostalCode = "90001", Country = "USA", PaymentMethod = "paypal", Status = "processing", TrackingNumber = 9972778109, ShippingCost = 35.28m, Tax = 54.16m, ProductName = "Coffee Maker", CustomerName = "Jane Doe" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 15, Price = 234.56m, OrderDate = DateTime.UtcNow.AddDays(-20), ShippingAddress = "789 Pine Rd", City = "Chicago", State = "IL", PostalCode = "60601", Country = "USA", PaymentMethod = "credit card", Status = "shipped", TrackingNumber = 1234567890, ShippingCost = 18.99m, Tax = 21.45m, ProductName = "Yoga Mat", CustomerName = "Mike Wilson" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 100, Price = 1299.99m, OrderDate = DateTime.UtcNow.AddDays(-15), ShippingAddress = "321 Elm St", City = "Toronto", State = "ON", PostalCode = "M5V 3L9", Country = "Canada", PaymentMethod = "bank transfer", Status = "delivered", TrackingNumber = 2345678901, ShippingCost = 45.00m, Tax = 168.99m, ProductName = "Office Chairs", CustomerName = "Sarah Lee" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 5, Price = 89.95m, OrderDate = DateTime.UtcNow.AddDays(-10), ShippingAddress = "654 Maple Dr", City = "London", State = "", PostalCode = "SW1A 1AA", Country = "UK", PaymentMethod = "credit card", Status = "processing", TrackingNumber = 3456789012, ShippingCost = 12.50m, Tax = 17.99m, ProductName = "Green Tea Set", CustomerName = "Emma Brown" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 30, Price = 567.80m, OrderDate = DateTime.UtcNow.AddDays(-8), ShippingAddress = "987 Cedar Ln", City = "Sydney", State = "NSW", PostalCode = "2000", Country = "Australia", PaymentMethod = "paypal", Status = "shipped", TrackingNumber = 4567890123, ShippingCost = 28.00m, Tax = 56.78m, ProductName = "Water Bottles", CustomerName = "James Taylor" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 12, Price = 345.67m, OrderDate = DateTime.UtcNow.AddDays(-5), ShippingAddress = "147 Birch Way", City = "Berlin", State = "", PostalCode = "10115", Country = "Germany", PaymentMethod = "credit card", Status = "pending", TrackingNumber = 5678901234, ShippingCost = 22.00m, Tax = 65.48m, ProductName = "LED Lamps", CustomerName = "Anna Mueller" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 8, Price = 199.99m, OrderDate = DateTime.UtcNow.AddDays(-3), ShippingAddress = "258 Walnut Blvd", City = "Tokyo", State = "", PostalCode = "100-0001", Country = "Japan", PaymentMethod = "credit card", Status = "processing", TrackingNumber = 6789012345, ShippingCost = 30.00m, Tax = 20.00m, ProductName = "Vitamin Supplements", CustomerName = "Yuki Tanaka" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 25, Price = 425.50m, OrderDate = DateTime.UtcNow.AddDays(-2), ShippingAddress = "369 Spruce Ct", City = "Paris", State = "", PostalCode = "75001", Country = "France", PaymentMethod = "bank transfer", Status = "shipped", TrackingNumber = 7890123456, ShippingCost = 19.50m, Tax = 85.10m, ProductName = "Organic Honey", CustomerName = "Pierre Dubois" },
                        new AntdOrder { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 3, Price = 799.97m, OrderDate = DateTime.UtcNow.AddDays(-1), ShippingAddress = "741 Ash St", City = "Dubai", State = "", PostalCode = "00000", Country = "UAE", PaymentMethod = "credit card", Status = "pending", TrackingNumber = 8901234567, ShippingCost = 40.00m, Tax = 0.00m, ProductName = "Bluetooth Speakers", CustomerName = "Ahmed Hassan" }
                    };
                    context.AntdOrders.AddRange(orders);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd orders data seeded successfully");
                }

                // Seed Antd Campaign Ads (10 rows)
                if (!await context.AntdCampaignAds.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd campaign ads data...");
                    var campaignAds = new List<AntdCampaignAd>
                    {
                        new AntdCampaignAd { AdSource = "Instagram", AdCampaign = "Back to School", AdGroup = "Women's Clothing", AdType = "Banner Ad", Impressions = 922427, Clicks = 5956, Conversions = 47, Cost = 613.11m, ConversionRate = 0.0958m, Revenue = 2463.28m, Roi = 3.54m, StartDate = DateTime.UtcNow.AddDays(-90) },
                        new AntdCampaignAd { AdSource = "LinkedIn", AdCampaign = "Holiday Promotion", AdGroup = "Electronics", AdType = "Sponsored Post", Impressions = 920343, Clicks = 1680, Conversions = 17, Cost = 254.11m, ConversionRate = 0.0797m, Revenue = 2894.63m, Roi = 10.39m, StartDate = DateTime.UtcNow.AddDays(-60) },
                        new AntdCampaignAd { AdSource = "Facebook", AdCampaign = "Summer Sale", AdGroup = "Home & Garden", AdType = "Carousel Ad", Impressions = 1543678, Clicks = 12453, Conversions = 234, Cost = 1245.67m, ConversionRate = 0.0188m, Revenue = 8976.45m, Roi = 6.20m, StartDate = DateTime.UtcNow.AddDays(-45) },
                        new AntdCampaignAd { AdSource = "Google Ads", AdCampaign = "Black Friday", AdGroup = "Electronics", AdType = "Search Ad", Impressions = 2345678, Clicks = 34567, Conversions = 567, Cost = 3456.78m, ConversionRate = 0.0164m, Revenue = 23456.78m, Roi = 5.78m, StartDate = DateTime.UtcNow.AddDays(-30) },
                        new AntdCampaignAd { AdSource = "Twitter", AdCampaign = "New Product Launch", AdGroup = "Tech Gadgets", AdType = "Promoted Tweet", Impressions = 567890, Clicks = 4567, Conversions = 89, Cost = 567.89m, ConversionRate = 0.0195m, Revenue = 3456.78m, Roi = 5.08m, StartDate = DateTime.UtcNow.AddDays(-25) },
                        new AntdCampaignAd { AdSource = "YouTube", AdCampaign = "Brand Awareness", AdGroup = "Lifestyle", AdType = "Video Ad", Impressions = 3456789, Clicks = 23456, Conversions = 345, Cost = 4567.89m, ConversionRate = 0.0147m, Revenue = 12345.67m, Roi = 1.70m, StartDate = DateTime.UtcNow.AddDays(-20) },
                        new AntdCampaignAd { AdSource = "TikTok", AdCampaign = "Viral Challenge", AdGroup = "Youth Fashion", AdType = "In-Feed Ad", Impressions = 4567890, Clicks = 45678, Conversions = 678, Cost = 2345.67m, ConversionRate = 0.0148m, Revenue = 15678.90m, Roi = 5.68m, StartDate = DateTime.UtcNow.AddDays(-15) },
                        new AntdCampaignAd { AdSource = "Pinterest", AdCampaign = "Home Decor Ideas", AdGroup = "Interior Design", AdType = "Pin Ad", Impressions = 789012, Clicks = 6789, Conversions = 123, Cost = 789.01m, ConversionRate = 0.0181m, Revenue = 4567.89m, Roi = 4.79m, StartDate = DateTime.UtcNow.AddDays(-10) },
                        new AntdCampaignAd { AdSource = "Snapchat", AdCampaign = "Flash Sale", AdGroup = "Accessories", AdType = "Story Ad", Impressions = 1234567, Clicks = 9876, Conversions = 156, Cost = 1234.56m, ConversionRate = 0.0158m, Revenue = 6789.01m, Roi = 4.50m, StartDate = DateTime.UtcNow.AddDays(-5) },
                        new AntdCampaignAd { AdSource = "Reddit", AdCampaign = "Community Engagement", AdGroup = "Gaming", AdType = "Promoted Post", Impressions = 456789, Clicks = 3456, Conversions = 67, Cost = 456.78m, ConversionRate = 0.0194m, Revenue = 2345.67m, Roi = 4.13m, StartDate = DateTime.UtcNow.AddDays(-2) }
                    };
                    context.AntdCampaignAds.AddRange(campaignAds);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd campaign ads data seeded successfully");
                }

                // Seed Antd Social Media Stats (6 rows)
                if (!await context.AntdSocialMediaStats.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd social media stats data...");
                    var socialMediaStats = new List<AntdSocialMediaStats>
                    {
                        new AntdSocialMediaStats { Title = "facebook", Followers = 11374, Following = 53756, Posts = 98954, Likes = 98992, Comments = 41318, EngagementRate = 3413.93m },
                        new AntdSocialMediaStats { Title = "twitter", Followers = 45020, Following = 79850, Posts = 90705, Likes = 40821, Comments = 14429, EngagementRate = 7672.22m },
                        new AntdSocialMediaStats { Title = "instagram", Followers = 89234, Following = 12456, Posts = 45678, Likes = 156789, Comments = 23456, EngagementRate = 5234.67m },
                        new AntdSocialMediaStats { Title = "linkedin", Followers = 23456, Following = 8976, Posts = 12345, Likes = 34567, Comments = 5678, EngagementRate = 2345.89m },
                        new AntdSocialMediaStats { Title = "youtube", Followers = 156789, Following = 234, Posts = 1234, Likes = 456789, Comments = 78901, EngagementRate = 8901.23m },
                        new AntdSocialMediaStats { Title = "tiktok", Followers = 234567, Following = 5678, Posts = 23456, Likes = 789012, Comments = 45678, EngagementRate = 12345.67m }
                    };
                    context.AntdSocialMediaStats.AddRange(socialMediaStats);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd social media stats data seeded successfully");
                }

                // Seed Antd Social Media Activities (10 rows)
                if (!await context.AntdSocialMediaActivities.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd social media activities data...");
                    var activities = new List<AntdSocialMediaActivity>
                    {
                        new AntdSocialMediaActivity { Author = "Adolpho Ibbison", UserId = Guid.NewGuid().ToString(), ActivityType = "post", Timestamp = DateTime.UtcNow.AddDays(-180), PostContent = "Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.", Platform = "linkedin", UserLocation = "Temorlorong", UserAge = 66, UserGender = "Male", UserInterests = "dapibus augue vel accumsan tellus nisi eu orci mauris lacinia sapien quis libero", UserFriendsCount = 396 },
                        new AntdSocialMediaActivity { Author = "Paquito Mullany", UserId = Guid.NewGuid().ToString(), ActivityType = "comment", Timestamp = DateTime.UtcNow.AddDays(-150), PostContent = "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.", Platform = "facebook", UserLocation = "Banarankrajan", UserAge = 57, UserGender = "Male", UserInterests = "nunc commodo placerat praesent blandit nam nulla integer pede justo lacinia eget tincidunt eget", UserFriendsCount = 957 },
                        new AntdSocialMediaActivity { Author = "Sarah Chen", UserId = Guid.NewGuid().ToString(), ActivityType = "share", Timestamp = DateTime.UtcNow.AddDays(-120), PostContent = "Great article on sustainable living! Everyone should read this.", Platform = "twitter", UserLocation = "San Francisco", UserAge = 34, UserGender = "Female", UserInterests = "sustainability environment green living eco-friendly", UserFriendsCount = 1234 },
                        new AntdSocialMediaActivity { Author = "Marcus Johnson", UserId = Guid.NewGuid().ToString(), ActivityType = "post", Timestamp = DateTime.UtcNow.AddDays(-90), PostContent = "Just launched our new product line! Check it out at our website.", Platform = "instagram", UserLocation = "New York", UserAge = 42, UserGender = "Male", UserInterests = "business entrepreneurship marketing startups", UserFriendsCount = 2567 },
                        new AntdSocialMediaActivity { Author = "Emma Wilson", UserId = Guid.NewGuid().ToString(), ActivityType = "like", Timestamp = DateTime.UtcNow.AddDays(-60), PostContent = "Amazing sunset photo from my trip to Bali!", Platform = "instagram", UserLocation = "London", UserAge = 28, UserGender = "Female", UserInterests = "travel photography adventure nature", UserFriendsCount = 876 },
                        new AntdSocialMediaActivity { Author = "David Kim", UserId = Guid.NewGuid().ToString(), ActivityType = "comment", Timestamp = DateTime.UtcNow.AddDays(-45), PostContent = "This is exactly what I was looking for. Thanks for sharing!", Platform = "youtube", UserLocation = "Seoul", UserAge = 31, UserGender = "Male", UserInterests = "technology gadgets reviews tutorials", UserFriendsCount = 543 },
                        new AntdSocialMediaActivity { Author = "Lisa Anderson", UserId = Guid.NewGuid().ToString(), ActivityType = "post", Timestamp = DateTime.UtcNow.AddDays(-30), PostContent = "New recipe alert! Vegan chocolate cake that tastes amazing.", Platform = "pinterest", UserLocation = "Chicago", UserAge = 39, UserGender = "Female", UserInterests = "cooking recipes food vegan healthy", UserFriendsCount = 1789 },
                        new AntdSocialMediaActivity { Author = "James Brown", UserId = Guid.NewGuid().ToString(), ActivityType = "share", Timestamp = DateTime.UtcNow.AddDays(-15), PostContent = "Important news about climate change. Please read and share.", Platform = "facebook", UserLocation = "Toronto", UserAge = 45, UserGender = "Male", UserInterests = "news politics environment activism", UserFriendsCount = 2345 },
                        new AntdSocialMediaActivity { Author = "Maria Garcia", UserId = Guid.NewGuid().ToString(), ActivityType = "post", Timestamp = DateTime.UtcNow.AddDays(-7), PostContent = "Excited to announce my new book is now available!", Platform = "twitter", UserLocation = "Madrid", UserAge = 52, UserGender = "Female", UserInterests = "writing books literature publishing", UserFriendsCount = 4567 },
                        new AntdSocialMediaActivity { Author = "Alex Thompson", UserId = Guid.NewGuid().ToString(), ActivityType = "comment", Timestamp = DateTime.UtcNow.AddDays(-2), PostContent = "Great tips for improving productivity. I'll definitely try these!", Platform = "linkedin", UserLocation = "Sydney", UserAge = 36, UserGender = "Male", UserInterests = "productivity self-improvement career business", UserFriendsCount = 678 }
                    };
                    context.AntdSocialMediaActivities.AddRange(activities);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd social media activities data seeded successfully");
                }

                // Seed Antd Scheduled Posts (10 rows)
                if (!await context.AntdScheduledPosts.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd scheduled posts data...");
                    var scheduledPosts = new List<AntdScheduledPost>
                    {
                        new AntdScheduledPost { Title = "New Product Launch Announcement", Content = "We're excited to announce the launch of our latest product line. Stay tuned for more details!", ScheduledDate = DateTime.UtcNow.AddDays(7), ScheduledTime = 10, Author = "Oren Stretton", Category = "Promotions", Tags = "launch product new announcement", LikesCount = 511, CommentsCount = 488, SharesCount = 79, ImageUrl = "", Link = "https://example.com/product-launch", Location = "New York", Hashtags = "#newproduct #launch #exciting", Platform = "Instagram" },
                        new AntdScheduledPost { Title = "Holiday Sale Event", Content = "Don't miss our biggest sale of the year! Up to 50% off on selected items.", ScheduledDate = DateTime.UtcNow.AddDays(14), ScheduledTime = 9, Author = "Dianne Delooze", Category = "Events", Tags = "sale holiday discount", LikesCount = 410, CommentsCount = 400, SharesCount = 74, ImageUrl = "", Link = "https://example.com/holiday-sale", Location = "Los Angeles", Hashtags = "#sale #holiday #discount", Platform = "Facebook" },
                        new AntdScheduledPost { Title = "Behind the Scenes", Content = "Take a look at how we create our products. From design to delivery.", ScheduledDate = DateTime.UtcNow.AddDays(3), ScheduledTime = 14, Author = "Michael Chen", Category = "Content", Tags = "behind scenes making process", LikesCount = 234, CommentsCount = 156, SharesCount = 45, ImageUrl = "", Link = "https://example.com/bts", Location = "San Francisco", Hashtags = "#behindthescenes #process #creation", Platform = "YouTube" },
                        new AntdScheduledPost { Title = "Customer Success Story", Content = "Read how our customer achieved amazing results using our solution.", ScheduledDate = DateTime.UtcNow.AddDays(5), ScheduledTime = 11, Author = "Sarah Johnson", Category = "Testimonials", Tags = "customer success story testimonial", LikesCount = 567, CommentsCount = 234, SharesCount = 123, ImageUrl = "", Link = "https://example.com/success-story", Location = "Chicago", Hashtags = "#success #customer #results", Platform = "LinkedIn" },
                        new AntdScheduledPost { Title = "Weekly Tips and Tricks", Content = "This week's tips for maximizing productivity and efficiency.", ScheduledDate = DateTime.UtcNow.AddDays(1), ScheduledTime = 8, Author = "Emily Davis", Category = "Education", Tags = "tips tricks productivity weekly", LikesCount = 345, CommentsCount = 178, SharesCount = 67, ImageUrl = "", Link = "https://example.com/tips", Location = "Boston", Hashtags = "#tips #productivity #efficiency", Platform = "Twitter" },
                        new AntdScheduledPost { Title = "Community Spotlight", Content = "Featuring amazing work from our community members this month.", ScheduledDate = DateTime.UtcNow.AddDays(10), ScheduledTime = 15, Author = "Robert Wilson", Category = "Community", Tags = "community spotlight feature", LikesCount = 289, CommentsCount = 134, SharesCount = 56, ImageUrl = "", Link = "https://example.com/community", Location = "Seattle", Hashtags = "#community #spotlight #feature", Platform = "Instagram" },
                        new AntdScheduledPost { Title = "Industry News Roundup", Content = "Catch up on the latest news and trends in our industry.", ScheduledDate = DateTime.UtcNow.AddDays(2), ScheduledTime = 12, Author = "Lisa Anderson", Category = "News", Tags = "news industry trends roundup", LikesCount = 456, CommentsCount = 267, SharesCount = 89, ImageUrl = "", Link = "https://example.com/news", Location = "Denver", Hashtags = "#news #industry #trends", Platform = "LinkedIn" },
                        new AntdScheduledPost { Title = "Flash Sale Alert", Content = "24-hour flash sale starting now! Limited quantities available.", ScheduledDate = DateTime.UtcNow.AddDays(4), ScheduledTime = 6, Author = "David Brown", Category = "Promotions", Tags = "flash sale alert limited", LikesCount = 678, CommentsCount = 345, SharesCount = 156, ImageUrl = "", Link = "https://example.com/flash-sale", Location = "Miami", Hashtags = "#flashsale #limited #deal", Platform = "Facebook" },
                        new AntdScheduledPost { Title = "Tutorial: Getting Started", Content = "Step-by-step guide for new users to get the most out of our platform.", ScheduledDate = DateTime.UtcNow.AddDays(6), ScheduledTime = 13, Author = "Anna Martinez", Category = "Education", Tags = "tutorial getting started guide", LikesCount = 234, CommentsCount = 123, SharesCount = 78, ImageUrl = "", Link = "https://example.com/tutorial", Location = "Austin", Hashtags = "#tutorial #guide #newuser", Platform = "YouTube" },
                        new AntdScheduledPost { Title = "Team Introduction", Content = "Meet the amazing team behind our products and services.", ScheduledDate = DateTime.UtcNow.AddDays(8), ScheduledTime = 16, Author = "James Taylor", Category = "About Us", Tags = "team introduction meet", LikesCount = 189, CommentsCount = 98, SharesCount = 34, ImageUrl = "", Link = "https://example.com/team", Location = "Portland", Hashtags = "#team #meettheteam #aboutus", Platform = "Instagram" }
                    };
                    context.AntdScheduledPosts.AddRange(scheduledPosts);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd scheduled posts data seeded successfully");
                }

                // Seed Antd Live Auctions (10 rows)
                if (!await context.AntdLiveAuctions.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd live auctions data...");
                    var auctions = new List<AntdLiveAuction>
                    {
                        new AntdLiveAuction { NftName = "Cosmic Dreams #42", NftImage = "https://images.unsplash.com/photo-1644361566696-3d442b5b482a", SellerUsername = "rsingleton0", BuyerUsername = "fandretti0", StartPrice = 33611.73m, EndPrice = 95441.53m, StartDate = DateTime.UtcNow.AddDays(-60), EndDate = DateTime.UtcNow.AddDays(10), Status = "active", IsHighestBidMine = false, WinningBid = 140.94m, TimeLeft = "8:20 PM" },
                        new AntdLiveAuction { NftName = "Digital Horizon", NftImage = "https://images.unsplash.com/photo-1645731505493-7a13123374fd", SellerUsername = "atroyes1", BuyerUsername = "lkiehnlt1", StartPrice = 1311.85m, EndPrice = 55786.0m, StartDate = DateTime.UtcNow.AddDays(-90), EndDate = DateTime.UtcNow.AddDays(2), Status = "ending soon", IsHighestBidMine = true, WinningBid = 389.21m, TimeLeft = "8:31 PM" },
                        new AntdLiveAuction { NftName = "Neon Nights Collection", NftImage = "https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe", SellerUsername = "cryptoartist", BuyerUsername = "nftcollector", StartPrice = 5000.00m, EndPrice = 25000.00m, StartDate = DateTime.UtcNow.AddDays(-30), EndDate = DateTime.UtcNow.AddDays(15), Status = "active", IsHighestBidMine = false, WinningBid = 7500.00m, TimeLeft = "3:45 PM" },
                        new AntdLiveAuction { NftName = "Abstract Reality #7", NftImage = "https://images.unsplash.com/photo-1634986666676-ec8fd927c23d", SellerUsername = "digitalmaster", BuyerUsername = "artlover99", StartPrice = 2500.00m, EndPrice = 15000.00m, StartDate = DateTime.UtcNow.AddDays(-45), EndDate = DateTime.UtcNow.AddDays(5), Status = "active", IsHighestBidMine = true, WinningBid = 4200.00m, TimeLeft = "11:00 AM" },
                        new AntdLiveAuction { NftName = "Pixel Perfect", NftImage = "https://images.unsplash.com/photo-1620641788421-7a1c342ea42e", SellerUsername = "pixelking", BuyerUsername = "collector42", StartPrice = 800.00m, EndPrice = 5000.00m, StartDate = DateTime.UtcNow.AddDays(-20), EndDate = DateTime.UtcNow.AddDays(8), Status = "active", IsHighestBidMine = false, WinningBid = 1200.00m, TimeLeft = "6:30 PM" },
                        new AntdLiveAuction { NftName = "Cyberpunk City", NftImage = "https://images.unsplash.com/photo-1633356122544-f134324a6cee", SellerUsername = "futureart", BuyerUsername = "techbro", StartPrice = 10000.00m, EndPrice = 50000.00m, StartDate = DateTime.UtcNow.AddDays(-15), EndDate = DateTime.UtcNow.AddDays(1), Status = "ending soon", IsHighestBidMine = false, WinningBid = 35000.00m, TimeLeft = "2:15 PM" },
                        new AntdLiveAuction { NftName = "Ocean Depths", NftImage = "https://images.unsplash.com/photo-1618172193622-ae2d025f4032", SellerUsername = "naturenft", BuyerUsername = "sealover", StartPrice = 1500.00m, EndPrice = 8000.00m, StartDate = DateTime.UtcNow.AddDays(-25), EndDate = DateTime.UtcNow.AddDays(12), Status = "active", IsHighestBidMine = true, WinningBid = 2800.00m, TimeLeft = "9:45 AM" },
                        new AntdLiveAuction { NftName = "Golden Age", NftImage = "https://images.unsplash.com/photo-1639762681485-074b7f938ba0", SellerUsername = "vintagearts", BuyerUsername = "goldcollector", StartPrice = 20000.00m, EndPrice = 100000.00m, StartDate = DateTime.UtcNow.AddDays(-10), EndDate = DateTime.UtcNow.AddDays(20), Status = "active", IsHighestBidMine = false, WinningBid = 45000.00m, TimeLeft = "4:00 PM" },
                        new AntdLiveAuction { NftName = "Space Explorer", NftImage = "https://images.unsplash.com/photo-1634193295627-1cdddf751ebf", SellerUsername = "spacenft", BuyerUsername = "astronaut", StartPrice = 3000.00m, EndPrice = 18000.00m, StartDate = DateTime.UtcNow.AddDays(-35), EndDate = DateTime.UtcNow.AddDays(3), Status = "ending soon", IsHighestBidMine = true, WinningBid = 12500.00m, TimeLeft = "7:00 PM" },
                        new AntdLiveAuction { NftName = "Retro Waves", NftImage = "https://images.unsplash.com/photo-1618172193763-c511deb635ca", SellerUsername = "retromaster", BuyerUsername = "80slover", StartPrice = 600.00m, EndPrice = 3500.00m, StartDate = DateTime.UtcNow.AddDays(-40), EndDate = DateTime.UtcNow.AddDays(7), Status = "active", IsHighestBidMine = false, WinningBid = 950.00m, TimeLeft = "1:30 PM" }
                    };
                    context.AntdLiveAuctions.AddRange(auctions);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd live auctions data seeded successfully");
                }

                // Seed Antd Auction Creators (10 rows)
                if (!await context.AntdAuctionCreators.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd auction creators data...");
                    var creators = new List<AntdAuctionCreator>
                    {
                        new AntdAuctionCreator { FirstName = "Eda", LastName = "Crighton", Age = 84, Email = "ecrighton0@aboutads.info", Country = "Indonesia", PostalCode = "", FavoriteColor = "blue", SalesCount = 46, TotalSales = "Rupiah" },
                        new AntdAuctionCreator { FirstName = "Page", LastName = "Nickoles", Age = 83, Email = "pnickoles1@ycombinator.com", Country = "Thailand", PostalCode = "49120", FavoriteColor = "blue", SalesCount = 736, TotalSales = "Baht" },
                        new AntdAuctionCreator { FirstName = "Marcus", LastName = "Chen", Age = 35, Email = "mchen@nftcreator.com", Country = "USA", PostalCode = "10001", FavoriteColor = "green", SalesCount = 523, TotalSales = "USD" },
                        new AntdAuctionCreator { FirstName = "Sofia", LastName = "Rodriguez", Age = 28, Email = "srodriguez@artworld.com", Country = "Spain", PostalCode = "28001", FavoriteColor = "red", SalesCount = 312, TotalSales = "EUR" },
                        new AntdAuctionCreator { FirstName = "Yuki", LastName = "Tanaka", Age = 42, Email = "ytanaka@digitalart.jp", Country = "Japan", PostalCode = "100-0001", FavoriteColor = "purple", SalesCount = 891, TotalSales = "JPY" },
                        new AntdAuctionCreator { FirstName = "Oliver", LastName = "Smith", Age = 31, Email = "osmith@nftmarket.uk", Country = "UK", PostalCode = "SW1A 1AA", FavoriteColor = "blue", SalesCount = 445, TotalSales = "GBP" },
                        new AntdAuctionCreator { FirstName = "Emma", LastName = "Johnson", Age = 39, Email = "ejohnson@crypto.au", Country = "Australia", PostalCode = "2000", FavoriteColor = "yellow", SalesCount = 267, TotalSales = "AUD" },
                        new AntdAuctionCreator { FirstName = "Lucas", LastName = "Mueller", Age = 45, Email = "lmueller@kunst.de", Country = "Germany", PostalCode = "10115", FavoriteColor = "black", SalesCount = 634, TotalSales = "EUR" },
                        new AntdAuctionCreator { FirstName = "Aria", LastName = "Kim", Age = 26, Email = "akim@nftseoul.kr", Country = "South Korea", PostalCode = "03171", FavoriteColor = "pink", SalesCount = 178, TotalSales = "KRW" },
                        new AntdAuctionCreator { FirstName = "Carlos", LastName = "Santos", Age = 52, Email = "csantos@artbr.com", Country = "Brazil", PostalCode = "01310-100", FavoriteColor = "orange", SalesCount = 389, TotalSales = "BRL" }
                    };
                    context.AntdAuctionCreators.AddRange(creators);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd auction creators data seeded successfully");
                }

                // Seed Antd Bidding Top Sellers (10 rows)
                if (!await context.AntdBiddingTopSellers.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd bidding top sellers data...");
                    var topSellers = new List<AntdBiddingTopSeller>
                    {
                        new AntdBiddingTopSeller { Title = "Cosmic Dreams", Artist = "Garnette Meneo", Volume = 3413, Status = 54, OwnersCount = 49762, Description = "A stunning cosmic landscape with vibrant colors.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-180), Edition = 608, Price = 786529.23m, Owner = "Ellyn Fyall", Collection = "Abstract", Verified = false },
                        new AntdBiddingTopSeller { Title = "Ocean Waves", Artist = "Lazare Bonhill", Volume = 1972, Status = 45, OwnersCount = 12407, Description = "Mesmerizing ocean waves in digital art.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-300), Edition = 929, Price = 376868.39m, Owner = "Cathe Yitzhak", Collection = "Animals", Verified = true },
                        new AntdBiddingTopSeller { Title = "Neon City", Artist = "Marcus Chen", Volume = 4521, Status = 78, OwnersCount = 85342, Description = "Futuristic cityscape with neon lights.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-120), Edition = 150, Price = 1234567.89m, Owner = "John Smith", Collection = "Sci-Fi", Verified = true },
                        new AntdBiddingTopSeller { Title = "Forest Spirit", Artist = "Sofia Rodriguez", Volume = 2876, Status = 62, OwnersCount = 34521, Description = "Mystical forest spirit artwork.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-90), Edition = 500, Price = 456789.12m, Owner = "Emma Wilson", Collection = "Fantasy", Verified = true },
                        new AntdBiddingTopSeller { Title = "Digital Dreams", Artist = "Yuki Tanaka", Volume = 5234, Status = 89, OwnersCount = 98765, Description = "Abstract digital dreamscape.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-60), Edition = 200, Price = 987654.32m, Owner = "David Kim", Collection = "Abstract", Verified = true },
                        new AntdBiddingTopSeller { Title = "Mountain Peak", Artist = "Oliver Smith", Volume = 1543, Status = 34, OwnersCount = 23456, Description = "Majestic mountain peak at sunset.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-150), Edition = 750, Price = 234567.89m, Owner = "Lisa Anderson", Collection = "Nature", Verified = false },
                        new AntdBiddingTopSeller { Title = "Pixel Art Hero", Artist = "Emma Johnson", Volume = 3987, Status = 71, OwnersCount = 67890, Description = "Retro pixel art hero character.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-45), Edition = 100, Price = 567890.12m, Owner = "James Brown", Collection = "Gaming", Verified = true },
                        new AntdBiddingTopSeller { Title = "Abstract Flow", Artist = "Lucas Mueller", Volume = 2345, Status = 56, OwnersCount = 45678, Description = "Flowing abstract patterns.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-200), Edition = 300, Price = 345678.90m, Owner = "Maria Garcia", Collection = "Abstract", Verified = false },
                        new AntdBiddingTopSeller { Title = "Cyber Punk", Artist = "Aria Kim", Volume = 4123, Status = 83, OwnersCount = 78901, Description = "Cyberpunk character design.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-30), Edition = 250, Price = 789012.34m, Owner = "Alex Thompson", Collection = "Sci-Fi", Verified = true },
                        new AntdBiddingTopSeller { Title = "Golden Sunset", Artist = "Carlos Santos", Volume = 1876, Status = 48, OwnersCount = 34567, Description = "Beautiful golden sunset over the ocean.", ImageUrl = "", CreationDate = DateTime.UtcNow.AddDays(-250), Edition = 450, Price = 456123.78m, Owner = "Sarah Lee", Collection = "Nature", Verified = false }
                    };
                    context.AntdBiddingTopSellers.AddRange(topSellers);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd bidding top sellers data seeded successfully");
                }

                // Seed Antd Bidding Transactions (10 rows)
                if (!await context.AntdBiddingTransactions.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd bidding transactions data...");
                    var transactions = new List<AntdBiddingTransaction>
                    {
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-30), Seller = "kbellamy0", Buyer = "rhalpen0", PurchasePrice = 694.08m, SalePrice = 426.64m, Profit = -267.44m, Quantity = 89, ShippingAddress = "530 Linden Drive", State = "", Country = "Albania", TransactionType = "transfer" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-25), Seller = "wdunseith1", Buyer = "ctondeur1", PurchasePrice = 605.77m, SalePrice = 204.8m, Profit = -400.97m, Quantity = 79, ShippingAddress = "202 Raven Park", State = "", Country = "China", TransactionType = "refund" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-20), Seller = "jsmith", Buyer = "mwilson", PurchasePrice = 1500.00m, SalePrice = 2300.00m, Profit = 800.00m, Quantity = 5, ShippingAddress = "123 Main St", State = "NY", Country = "USA", TransactionType = "sale" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-15), Seller = "erodriguez", Buyer = "dkim", PurchasePrice = 850.00m, SalePrice = 1200.00m, Profit = 350.00m, Quantity = 12, ShippingAddress = "456 Oak Ave", State = "CA", Country = "USA", TransactionType = "sale" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-10), Seller = "ytanaka", Buyer = "osmith", PurchasePrice = 2000.00m, SalePrice = 3500.00m, Profit = 1500.00m, Quantity = 3, ShippingAddress = "789 Pine Rd", State = "", Country = "Japan", TransactionType = "sale" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-8), Seller = "ejohnson", Buyer = "lmueller", PurchasePrice = 450.00m, SalePrice = 380.00m, Profit = -70.00m, Quantity = 25, ShippingAddress = "321 Elm St", State = "NSW", Country = "Australia", TransactionType = "refund" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-5), Seller = "akim", Buyer = "csantos", PurchasePrice = 1800.00m, SalePrice = 2500.00m, Profit = 700.00m, Quantity = 8, ShippingAddress = "654 Maple Dr", State = "", Country = "South Korea", TransactionType = "sale" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-3), Seller = "mchen", Buyer = "slee", PurchasePrice = 3200.00m, SalePrice = 4100.00m, Profit = 900.00m, Quantity = 2, ShippingAddress = "987 Cedar Ln", State = "", Country = "UK", TransactionType = "sale" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-2), Seller = "jbrown", Buyer = "mgarcia", PurchasePrice = 750.00m, SalePrice = 750.00m, Profit = 0.00m, Quantity = 15, ShippingAddress = "147 Birch Way", State = "", Country = "Germany", TransactionType = "transfer" },
                        new AntdBiddingTransaction { Image = "", ProductId = Guid.NewGuid().ToString(), TransactionDate = DateTime.UtcNow.AddDays(-1), Seller = "athompson", Buyer = "rsingleton", PurchasePrice = 1100.00m, SalePrice = 1650.00m, Profit = 550.00m, Quantity = 10, ShippingAddress = "258 Walnut Blvd", State = "", Country = "Brazil", TransactionType = "sale" }
                    };
                    context.AntdBiddingTransactions.AddRange(transactions);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd bidding transactions data seeded successfully");
                }

                // Seed Antd Courses (10 rows)
                if (!await context.AntdCourses.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd courses data...");
                    var courses = new List<AntdCourse>
                    {
                        new AntdCourse { Name = "Introduction to Web Development", Code = "WEB101", Description = "Learn the fundamentals of HTML, CSS, and JavaScript to build modern websites.", InstructorName = "Dr. Sarah Chen", StartDate = DateTime.UtcNow.AddDays(-60), EndDate = DateTime.UtcNow.AddDays(30), CreditHours = 3, Department = "Computer Science", Prerequisites = "None", CourseLocation = "Room 101, Tech Building", TotalLessons = 24, CurrentLessons = 18, FavoriteColor = "blue" },
                        new AntdCourse { Name = "Data Structures and Algorithms", Code = "CS201", Description = "Master essential data structures and algorithmic problem-solving techniques.", InstructorName = "Prof. Michael Johnson", StartDate = DateTime.UtcNow.AddDays(-45), EndDate = DateTime.UtcNow.AddDays(45), CreditHours = 4, Department = "Computer Science", Prerequisites = "CS101", CourseLocation = "Room 205, Science Hall", TotalLessons = 32, CurrentLessons = 20, FavoriteColor = "green" },
                        new AntdCourse { Name = "Digital Marketing Fundamentals", Code = "MKT110", Description = "Explore digital marketing strategies including SEO, social media, and content marketing.", InstructorName = "Emily Rodriguez", StartDate = DateTime.UtcNow.AddDays(-30), EndDate = DateTime.UtcNow.AddDays(60), CreditHours = 3, Department = "Business", Prerequisites = "None", CourseLocation = "Room 302, Business Center", TotalLessons = 20, CurrentLessons = 10, FavoriteColor = "purple" },
                        new AntdCourse { Name = "Machine Learning Basics", Code = "AI301", Description = "Introduction to machine learning algorithms and their practical applications.", InstructorName = "Dr. James Liu", StartDate = DateTime.UtcNow.AddDays(-20), EndDate = DateTime.UtcNow.AddDays(70), CreditHours = 4, Department = "Computer Science", Prerequisites = "CS201, MATH201", CourseLocation = "Room 401, AI Lab", TotalLessons = 28, CurrentLessons = 8, FavoriteColor = "red" },
                        new AntdCourse { Name = "Project Management Professional", Code = "PM200", Description = "Learn industry-standard project management methodologies and tools.", InstructorName = "Lisa Anderson", StartDate = DateTime.UtcNow.AddDays(-90), EndDate = DateTime.UtcNow.AddDays(-10), CreditHours = 3, Department = "Business", Prerequisites = "None", CourseLocation = "Room 201, Admin Building", TotalLessons = 16, CurrentLessons = 16, FavoriteColor = "orange" },
                        new AntdCourse { Name = "UX/UI Design Principles", Code = "DES150", Description = "Master user experience and interface design principles for digital products.", InstructorName = "Alex Thompson", StartDate = DateTime.UtcNow.AddDays(-15), EndDate = DateTime.UtcNow.AddDays(75), CreditHours = 3, Department = "Design", Prerequisites = "None", CourseLocation = "Room 105, Design Studio", TotalLessons = 22, CurrentLessons = 5, FavoriteColor = "pink" },
                        new AntdCourse { Name = "Database Management Systems", Code = "CS250", Description = "Learn relational database design, SQL, and database administration.", InstructorName = "Dr. Robert Wilson", StartDate = DateTime.UtcNow.AddDays(-50), EndDate = DateTime.UtcNow.AddDays(40), CreditHours = 4, Department = "Computer Science", Prerequisites = "CS101", CourseLocation = "Room 303, Tech Building", TotalLessons = 26, CurrentLessons = 15, FavoriteColor = "cyan" },
                        new AntdCourse { Name = "Financial Analysis", Code = "FIN220", Description = "Develop skills in financial statement analysis and valuation techniques.", InstructorName = "Prof. Maria Santos", StartDate = DateTime.UtcNow.AddDays(-40), EndDate = DateTime.UtcNow.AddDays(50), CreditHours = 3, Department = "Finance", Prerequisites = "ACC101", CourseLocation = "Room 402, Business Center", TotalLessons = 18, CurrentLessons = 12, FavoriteColor = "gold" },
                        new AntdCourse { Name = "Cloud Computing Fundamentals", Code = "IT310", Description = "Explore cloud platforms, services, and deployment strategies.", InstructorName = "David Kim", StartDate = DateTime.UtcNow.AddDays(-10), EndDate = DateTime.UtcNow.AddDays(80), CreditHours = 3, Department = "Information Technology", Prerequisites = "IT101", CourseLocation = "Room 501, Cloud Lab", TotalLessons = 20, CurrentLessons = 3, FavoriteColor = "lightblue" },
                        new AntdCourse { Name = "Business Communication", Code = "COM100", Description = "Enhance professional writing and presentation skills for business contexts.", InstructorName = "Jennifer Brown", StartDate = DateTime.UtcNow.AddDays(-75), EndDate = DateTime.UtcNow.AddDays(15), CreditHours = 2, Department = "Communications", Prerequisites = "None", CourseLocation = "Room 102, Arts Building", TotalLessons = 14, CurrentLessons = 12, FavoriteColor = "teal" }
                    };
                    context.AntdCourses.AddRange(courses);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd courses data seeded successfully");
                }

                // Seed Antd Study Statistics (10 rows)
                if (!await context.AntdStudyStatistics.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd study statistics data...");
                    var studyStats = new List<AntdStudyStatistic>
                    {
                        new AntdStudyStatistic { Value = 27.1m, Category = "Mathematics", Month = "Feb" },
                        new AntdStudyStatistic { Value = 15.2m, Category = "Science", Month = "Mar" },
                        new AntdStudyStatistic { Value = 42.6m, Category = "Foreign Language", Month = "Apr" },
                        new AntdStudyStatistic { Value = 33.8m, Category = "History", Month = "Jan" },
                        new AntdStudyStatistic { Value = 51.3m, Category = "Computer Science", Month = "May" },
                        new AntdStudyStatistic { Value = 28.9m, Category = "Physics", Month = "Jun" },
                        new AntdStudyStatistic { Value = 45.2m, Category = "Chemistry", Month = "Jul" },
                        new AntdStudyStatistic { Value = 19.7m, Category = "Biology", Month = "Aug" },
                        new AntdStudyStatistic { Value = 38.4m, Category = "English", Month = "Sep" },
                        new AntdStudyStatistic { Value = 22.5m, Category = "Art", Month = "Oct" }
                    };
                    context.AntdStudyStatistics.AddRange(studyStats);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd study statistics data seeded successfully");
                }

                // Seed Antd Recommended Courses (10 rows)
                if (!await context.AntdRecommendedCourses.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd recommended courses data...");
                    var recommendedCourses = new List<AntdRecommendedCourse>
                    {
                        new AntdRecommendedCourse { Name = "Advanced Python Programming", Description = "Master advanced Python concepts including decorators, generators, and async programming.", Duration = 8, Level = "advanced", Price = 299.99m, Category = "Computer Science", Instructor = "Perry Maclaine", StartDate = DateTime.UtcNow.AddDays(7), CourseLanguage = "English", FavoriteColor = "silver", Lessons = 72 },
                        new AntdRecommendedCourse { Name = "Introduction to Data Science", Description = "Learn data analysis, visualization, and basic machine learning concepts.", Duration = 12, Level = "beginner", Price = 199.99m, Category = "Data Science", Instructor = "Kirbie Vannuchi", StartDate = DateTime.UtcNow.AddDays(14), CourseLanguage = "English", FavoriteColor = "yellow", Lessons = 40 },
                        new AntdRecommendedCourse { Name = "React.js Masterclass", Description = "Build modern web applications with React, Redux, and Next.js.", Duration = 10, Level = "intermediate", Price = 349.99m, Category = "Web Development", Instructor = "Sarah Chen", StartDate = DateTime.UtcNow.AddDays(3), CourseLanguage = "English", FavoriteColor = "blue", Lessons = 85 },
                        new AntdRecommendedCourse { Name = "Cloud Architecture with AWS", Description = "Design and implement scalable cloud solutions using Amazon Web Services.", Duration = 15, Level = "advanced", Price = 449.99m, Category = "Cloud Computing", Instructor = "Michael Johnson", StartDate = DateTime.UtcNow.AddDays(21), CourseLanguage = "English", FavoriteColor = "orange", Lessons = 95 },
                        new AntdRecommendedCourse { Name = "Spanish for Beginners", Description = "Start your journey to Spanish fluency with interactive lessons.", Duration = 6, Level = "beginner", Price = 149.99m, Category = "Languages", Instructor = "Maria Rodriguez", StartDate = DateTime.UtcNow.AddDays(5), CourseLanguage = "Spanish", FavoriteColor = "red", Lessons = 30 },
                        new AntdRecommendedCourse { Name = "Digital Marketing Strategy", Description = "Create effective digital marketing campaigns across multiple channels.", Duration = 8, Level = "intermediate", Price = 279.99m, Category = "Marketing", Instructor = "Emily Davis", StartDate = DateTime.UtcNow.AddDays(10), CourseLanguage = "English", FavoriteColor = "purple", Lessons = 50 },
                        new AntdRecommendedCourse { Name = "Mobile App Development with Flutter", Description = "Build cross-platform mobile apps with Dart and Flutter.", Duration = 11, Level = "intermediate", Price = 329.99m, Category = "Mobile Development", Instructor = "David Kim", StartDate = DateTime.UtcNow.AddDays(8), CourseLanguage = "English", FavoriteColor = "cyan", Lessons = 68 },
                        new AntdRecommendedCourse { Name = "Financial Planning Basics", Description = "Learn personal finance, budgeting, and investment fundamentals.", Duration = 4, Level = "beginner", Price = 99.99m, Category = "Finance", Instructor = "Robert Wilson", StartDate = DateTime.UtcNow.AddDays(12), CourseLanguage = "English", FavoriteColor = "green", Lessons = 24 },
                        new AntdRecommendedCourse { Name = "UI/UX Design Bootcamp", Description = "Master user interface and experience design with Figma and Adobe XD.", Duration = 9, Level = "intermediate", Price = 399.99m, Category = "Design", Instructor = "Lisa Anderson", StartDate = DateTime.UtcNow.AddDays(6), CourseLanguage = "English", FavoriteColor = "pink", Lessons = 55 },
                        new AntdRecommendedCourse { Name = "Cybersecurity Fundamentals", Description = "Understand security threats and learn to protect digital assets.", Duration = 7, Level = "beginner", Price = 249.99m, Category = "Security", Instructor = "James Taylor", StartDate = DateTime.UtcNow.AddDays(15), CourseLanguage = "English", FavoriteColor = "black", Lessons = 42 }
                    };
                    context.AntdRecommendedCourses.AddRange(recommendedCourses);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd recommended courses data seeded successfully");
                }

                // Seed Antd Exams (10 rows)
                if (!await context.AntdExams.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd exams data...");
                    var exams = new List<AntdExam>
                    {
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Aimee Dalzell", Email = "adalzell0@yahoo.com", Course = "Foreign Language", CourseCode = "LANG101", ExamDate = DateTime.UtcNow.AddDays(-30), ExamTime = 103, ExamDuration = 153, ExamScore = 32 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Rhianna Leeman", Email = "rleeman1@ftc.gov", Course = "Science", CourseCode = "SCI101", ExamDate = DateTime.UtcNow.AddDays(-25), ExamTime = 58, ExamDuration = 101, ExamScore = 53 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Marcus Chen", Email = "mchen@university.edu", Course = "Mathematics", CourseCode = "MATH201", ExamDate = DateTime.UtcNow.AddDays(-20), ExamTime = 90, ExamDuration = 120, ExamScore = 87 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Sarah Johnson", Email = "sjohnson@college.edu", Course = "Computer Science", CourseCode = "CS301", ExamDate = DateTime.UtcNow.AddDays(-15), ExamTime = 75, ExamDuration = 90, ExamScore = 92 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "David Kim", Email = "dkim@student.edu", Course = "Physics", CourseCode = "PHYS201", ExamDate = DateTime.UtcNow.AddDays(-10), ExamTime = 120, ExamDuration = 180, ExamScore = 78 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Emma Wilson", Email = "ewilson@mail.edu", Course = "Chemistry", CourseCode = "CHEM101", ExamDate = DateTime.UtcNow.AddDays(-8), ExamTime = 85, ExamDuration = 120, ExamScore = 65 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "James Brown", Email = "jbrown@academy.edu", Course = "History", CourseCode = "HIST101", ExamDate = DateTime.UtcNow.AddDays(-5), ExamTime = 60, ExamDuration = 90, ExamScore = 71 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Lisa Anderson", Email = "landerson@school.edu", Course = "English", CourseCode = "ENG201", ExamDate = DateTime.UtcNow.AddDays(-3), ExamTime = 95, ExamDuration = 120, ExamScore = 88 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Robert Taylor", Email = "rtaylor@edu.com", Course = "Biology", CourseCode = "BIO101", ExamDate = DateTime.UtcNow.AddDays(-2), ExamTime = 110, ExamDuration = 150, ExamScore = 74 },
                        new AntdExam { StudentId = Guid.NewGuid(), FullName = "Jennifer Martinez", Email = "jmartinez@univ.edu", Course = "Art History", CourseCode = "ART101", ExamDate = DateTime.UtcNow.AddDays(-1), ExamTime = 70, ExamDuration = 90, ExamScore = 81 }
                    };
                    context.AntdExams.AddRange(exams);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd exams data seeded successfully");
                }

                // Seed Antd Community Groups (10 rows)
                if (!await context.AntdCommunityGroups.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd community groups data...");
                    var communityGroups = new List<AntdCommunityGroup>
                    {
                        new AntdCommunityGroup { Name = "Tech Enthusiasts", Description = "A community for technology lovers to discuss latest trends and innovations.", Image = "", Category = "Education", Location = "New York", Size = 231, Leader = "Nada Aimer", StartDate = DateTime.UtcNow.AddDays(-180), MeetingTime = "12:36 PM", MemberAgeRange = 18, MemberInterests = "technology programming AI machine learning", FavoriteColor = "teal" },
                        new AntdCommunityGroup { Name = "Fitness Warriors", Description = "Join us for workout sessions and health tips.", Image = "", Category = "Health", Location = "Los Angeles", Size = 681, Leader = "Clarinda Graeme", StartDate = DateTime.UtcNow.AddDays(-150), MeetingTime = "6:00 AM", MemberAgeRange = 32, MemberInterests = "fitness health nutrition exercise wellness", FavoriteColor = "green" },
                        new AntdCommunityGroup { Name = "Book Club Central", Description = "Monthly book discussions and literary events.", Image = "", Category = "Education", Location = "Chicago", Size = 156, Leader = "Marcus Chen", StartDate = DateTime.UtcNow.AddDays(-120), MeetingTime = "7:00 PM", MemberAgeRange = 25, MemberInterests = "books reading literature fiction non-fiction", FavoriteColor = "purple" },
                        new AntdCommunityGroup { Name = "Startup Founders", Description = "Network with entrepreneurs and share startup experiences.", Image = "", Category = "Business", Location = "San Francisco", Size = 423, Leader = "Sarah Johnson", StartDate = DateTime.UtcNow.AddDays(-90), MeetingTime = "5:30 PM", MemberAgeRange = 28, MemberInterests = "entrepreneurship startups business innovation funding", FavoriteColor = "blue" },
                        new AntdCommunityGroup { Name = "Photography Lovers", Description = "Share your photos and learn new techniques.", Image = "", Category = "Art", Location = "Seattle", Size = 312, Leader = "David Kim", StartDate = DateTime.UtcNow.AddDays(-75), MeetingTime = "10:00 AM", MemberAgeRange = 22, MemberInterests = "photography cameras editing portraits landscapes", FavoriteColor = "orange" },
                        new AntdCommunityGroup { Name = "Language Exchange", Description = "Practice languages with native speakers.", Image = "", Category = "Education", Location = "Miami", Size = 534, Leader = "Maria Rodriguez", StartDate = DateTime.UtcNow.AddDays(-60), MeetingTime = "3:00 PM", MemberAgeRange = 24, MemberInterests = "languages culture travel communication exchange", FavoriteColor = "red" },
                        new AntdCommunityGroup { Name = "Gaming Guild", Description = "Connect with gamers and join tournaments.", Image = "", Category = "Entertainment", Location = "Austin", Size = 892, Leader = "James Taylor", StartDate = DateTime.UtcNow.AddDays(-45), MeetingTime = "8:00 PM", MemberAgeRange = 20, MemberInterests = "gaming esports streaming videos entertainment", FavoriteColor = "cyan" },
                        new AntdCommunityGroup { Name = "Cooking Masters", Description = "Share recipes and cooking tips with fellow food enthusiasts.", Image = "", Category = "Food", Location = "Denver", Size = 267, Leader = "Lisa Anderson", StartDate = DateTime.UtcNow.AddDays(-30), MeetingTime = "11:00 AM", MemberAgeRange = 35, MemberInterests = "cooking recipes food baking cuisine restaurants", FavoriteColor = "yellow" },
                        new AntdCommunityGroup { Name = "Hiking Adventures", Description = "Explore trails and outdoor activities together.", Image = "", Category = "Sports", Location = "Portland", Size = 445, Leader = "Robert Wilson", StartDate = DateTime.UtcNow.AddDays(-20), MeetingTime = "7:30 AM", MemberAgeRange = 30, MemberInterests = "hiking outdoors nature camping trails mountains", FavoriteColor = "brown" },
                        new AntdCommunityGroup { Name = "Music Makers", Description = "Collaborate with musicians and share your creations.", Image = "", Category = "Art", Location = "Nashville", Size = 378, Leader = "Emily Davis", StartDate = DateTime.UtcNow.AddDays(-10), MeetingTime = "4:00 PM", MemberAgeRange = 26, MemberInterests = "music instruments singing production concerts bands", FavoriteColor = "pink" }
                    };
                    context.AntdCommunityGroups.AddRange(communityGroups);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd community groups data seeded successfully");
                }

                // Seed Antd Truck Deliveries (10 rows)
                if (!await context.AntdTruckDeliveries.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd truck deliveries data...");
                    var truckDeliveries = new List<AntdTruckDelivery>
                    {
                        new AntdTruckDelivery { ShipmentId = Guid.Parse("d79bd5bf-732b-4bb2-b1d0-ce11d81436a2"), TruckId = Guid.Parse("6cf8fbcd-fd5a-449a-ba4e-9bf580770071"), CustomerId = Guid.Parse("79192439-9814-41c2-81cc-a0752c715df2"), CustomerName = "Powlowski, Stanton and Goyette", DriverName = "Ada Coxall", OriginCity = "Mergayu", DestinationCity = "Longjiang", ShipmentDate = DateTime.UtcNow.AddDays(-365), DeliveryTime = 14, ShipmentWeight = 1752.78m, DeliveryStatus = "delivered", ShipmentCost = 7389.89m, FavoriteColor = "gold" },
                        new AntdTruckDelivery { ShipmentId = Guid.Parse("e409d5b5-a6c1-4a02-9ca1-90d3f99e75f5"), TruckId = Guid.Parse("39f994b0-bf2d-4a13-84b4-ea80a9cb683f"), CustomerId = Guid.Parse("c05c7cbc-e80e-45d6-a07a-0b027d45e448"), CustomerName = "Baumbach LLC", DriverName = "Cherry Jentin", OriginCity = "Asikkala", DestinationCity = "Bakıxanov", ShipmentDate = DateTime.UtcNow.AddDays(-240), DeliveryTime = 23, ShipmentWeight = 16025.23m, DeliveryStatus = "delayed", ShipmentCost = 5075.07m, FavoriteColor = "teal" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Smith & Associates", DriverName = "John Miller", OriginCity = "New York", DestinationCity = "Los Angeles", ShipmentDate = DateTime.UtcNow.AddDays(-30), DeliveryTime = 48, ShipmentWeight = 5000.00m, DeliveryStatus = "in transit", ShipmentCost = 3500.00m, FavoriteColor = "blue" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Johnson Industries", DriverName = "Sarah Williams", OriginCity = "Chicago", DestinationCity = "Houston", ShipmentDate = DateTime.UtcNow.AddDays(-15), DeliveryTime = 24, ShipmentWeight = 8500.50m, DeliveryStatus = "delivered", ShipmentCost = 4200.00m, FavoriteColor = "green" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Davis Corp", DriverName = "Michael Brown", OriginCity = "Seattle", DestinationCity = "Denver", ShipmentDate = DateTime.UtcNow.AddDays(-10), DeliveryTime = 18, ShipmentWeight = 3200.75m, DeliveryStatus = "pending", ShipmentCost = 2800.00m, FavoriteColor = "red" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Wilson Logistics", DriverName = "Emily Chen", OriginCity = "Miami", DestinationCity = "Atlanta", ShipmentDate = DateTime.UtcNow.AddDays(-5), DeliveryTime = 12, ShipmentWeight = 6750.25m, DeliveryStatus = "in transit", ShipmentCost = 3100.00m, FavoriteColor = "purple" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Anderson Trading", DriverName = "David Lee", OriginCity = "Phoenix", DestinationCity = "San Diego", ShipmentDate = DateTime.UtcNow.AddDays(-3), DeliveryTime = 8, ShipmentWeight = 2100.00m, DeliveryStatus = "delivered", ShipmentCost = 1500.00m, FavoriteColor = "orange" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Taylor Enterprises", DriverName = "Jennifer Kim", OriginCity = "Boston", DestinationCity = "Philadelphia", ShipmentDate = DateTime.UtcNow.AddDays(-2), DeliveryTime = 6, ShipmentWeight = 4500.00m, DeliveryStatus = "delayed", ShipmentCost = 2200.00m, FavoriteColor = "cyan" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Martinez Supply", DriverName = "Robert Garcia", OriginCity = "Dallas", DestinationCity = "Austin", ShipmentDate = DateTime.UtcNow.AddDays(-1), DeliveryTime = 4, ShipmentWeight = 1800.50m, DeliveryStatus = "in transit", ShipmentCost = 950.00m, FavoriteColor = "pink" },
                        new AntdTruckDelivery { ShipmentId = Guid.NewGuid(), TruckId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), CustomerName = "Garcia Manufacturing", DriverName = "Lisa Rodriguez", OriginCity = "Portland", DestinationCity = "Sacramento", ShipmentDate = DateTime.UtcNow, DeliveryTime = 10, ShipmentWeight = 7200.00m, DeliveryStatus = "pending", ShipmentCost = 3800.00m, FavoriteColor = "yellow" }
                    };
                    context.AntdTruckDeliveries.AddRange(truckDeliveries);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd truck deliveries data seeded successfully");
                }

                // Seed Antd Delivery Analytics (10 rows)
                if (!await context.AntdDeliveryAnalytics.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd delivery analytics data...");
                    var deliveryAnalytics = new List<AntdDeliveryAnalytic>
                    {
                        new AntdDeliveryAnalytic { Value = 82441, Month = "October", Status = "in transit" },
                        new AntdDeliveryAnalytic { Value = 71714, Month = "June", Status = "in transit" },
                        new AntdDeliveryAnalytic { Value = 51185, Month = "March", Status = "delivered" },
                        new AntdDeliveryAnalytic { Value = 45230, Month = "January", Status = "delivered" },
                        new AntdDeliveryAnalytic { Value = 63890, Month = "February", Status = "pending" },
                        new AntdDeliveryAnalytic { Value = 58720, Month = "April", Status = "delivered" },
                        new AntdDeliveryAnalytic { Value = 77340, Month = "May", Status = "in transit" },
                        new AntdDeliveryAnalytic { Value = 69450, Month = "July", Status = "delayed" },
                        new AntdDeliveryAnalytic { Value = 84120, Month = "August", Status = "delivered" },
                        new AntdDeliveryAnalytic { Value = 92560, Month = "September", Status = "in transit" }
                    };
                    context.AntdDeliveryAnalytics.AddRange(deliveryAnalytics);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd delivery analytics data seeded successfully");
                }

                // Seed Antd Trucks (10 rows)
                if (!await context.AntdTrucks.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd trucks data...");
                    var trucks = new List<AntdTruck>
                    {
                        new AntdTruck { TruckId = Guid.Parse("a81b31a8-dffa-421d-b500-ebd89eca3eb8"), Make = "Chevrolet", Model = "Ram", Year = 2012, Mileage = 305728, Price = 544573.35m, Color = "olive", Status = "in transit", Availability = false, Origin = "Soljani", Destination = "Sarbīsheh", Progress = 35 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Ford", Model = "F-150", Year = 2020, Mileage = 45000, Price = 42000.00m, Color = "white", Status = "available", Availability = true, Origin = "New York", Destination = "Chicago", Progress = 0 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Freightliner", Model = "Cascadia", Year = 2019, Mileage = 180000, Price = 85000.00m, Color = "blue", Status = "in transit", Availability = false, Origin = "Los Angeles", Destination = "Seattle", Progress = 65 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Kenworth", Model = "T680", Year = 2021, Mileage = 95000, Price = 125000.00m, Color = "red", Status = "maintenance", Availability = false, Origin = "Houston", Destination = "Dallas", Progress = 0 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Peterbilt", Model = "579", Year = 2018, Mileage = 250000, Price = 68000.00m, Color = "black", Status = "available", Availability = true, Origin = "Miami", Destination = "Atlanta", Progress = 0 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Volvo", Model = "VNL", Year = 2022, Mileage = 35000, Price = 145000.00m, Color = "silver", Status = "in transit", Availability = false, Origin = "Denver", Destination = "Phoenix", Progress = 80 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Mack", Model = "Anthem", Year = 2020, Mileage = 120000, Price = 95000.00m, Color = "green", Status = "available", Availability = true, Origin = "Boston", Destination = "Philadelphia", Progress = 0 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "International", Model = "LT", Year = 2019, Mileage = 200000, Price = 72000.00m, Color = "yellow", Status = "in transit", Availability = false, Origin = "San Francisco", Destination = "Portland", Progress = 45 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Western Star", Model = "5700XE", Year = 2021, Mileage = 80000, Price = 115000.00m, Color = "orange", Status = "maintenance", Availability = false, Origin = "Nashville", Destination = "Memphis", Progress = 0 },
                        new AntdTruck { TruckId = Guid.NewGuid(), Make = "Hino", Model = "XL8", Year = 2023, Mileage = 15000, Price = 98000.00m, Color = "gray", Status = "available", Availability = true, Origin = "Detroit", Destination = "Cleveland", Progress = 0 }
                    };
                    context.AntdTrucks.AddRange(trucks);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd trucks data seeded successfully");
                }

                // Seed Antd Truck Delivery Requests (10 rows)
                if (!await context.AntdTruckDeliveryRequests.AnyAsync())
                {
                    logger.LogInformation("Seeding Antd truck delivery requests data...");
                    var deliveryRequests = new List<AntdTruckDeliveryRequest>
                    {
                        new AntdTruckDeliveryRequest { Name = "sollicitudin mi sit", PickupLocation = "887 Anderson Trail", DeliveryLocation = "0041 Talisman Plaza", DeliveryDate = DateTime.UtcNow.AddDays(-180), DeliveryTime = 8, TruckType = "medium", CargoWeight = 8190.37m, DeliveryStatus = "pending", DriverName = "Lonnard Sheahan", ContactNumber = "685-807-2981" },
                        new AntdTruckDeliveryRequest { Name = "in consequat ut", PickupLocation = "758 Rusk Street", DeliveryLocation = "3300 Southridge Lane", DeliveryDate = DateTime.UtcNow.AddDays(-120), DeliveryTime = 2, TruckType = "medium", CargoWeight = 759.08m, DeliveryStatus = "pending", DriverName = "Borg Rothert", ContactNumber = "630-323-2763" },
                        new AntdTruckDeliveryRequest { Name = "Electronics Shipment", PickupLocation = "123 Tech Park Drive", DeliveryLocation = "456 Retail Center Blvd", DeliveryDate = DateTime.UtcNow.AddDays(7), DeliveryTime = 10, TruckType = "large", CargoWeight = 5500.00m, DeliveryStatus = "confirmed", DriverName = "Marcus Johnson", ContactNumber = "555-123-4567" },
                        new AntdTruckDeliveryRequest { Name = "Furniture Delivery", PickupLocation = "789 Warehouse Ave", DeliveryLocation = "321 Home Depot Rd", DeliveryDate = DateTime.UtcNow.AddDays(3), DeliveryTime = 14, TruckType = "large", CargoWeight = 3200.50m, DeliveryStatus = "in progress", DriverName = "Sarah Williams", ContactNumber = "555-234-5678" },
                        new AntdTruckDeliveryRequest { Name = "Food Supplies", PickupLocation = "456 Farm Road", DeliveryLocation = "789 Restaurant Row", DeliveryDate = DateTime.UtcNow.AddDays(1), DeliveryTime = 6, TruckType = "small", CargoWeight = 1200.00m, DeliveryStatus = "confirmed", DriverName = "Mike Chen", ContactNumber = "555-345-6789" },
                        new AntdTruckDeliveryRequest { Name = "Construction Materials", PickupLocation = "234 Industrial Blvd", DeliveryLocation = "567 Building Site", DeliveryDate = DateTime.UtcNow.AddDays(5), DeliveryTime = 7, TruckType = "large", CargoWeight = 12000.00m, DeliveryStatus = "pending", DriverName = "David Brown", ContactNumber = "555-456-7890" },
                        new AntdTruckDeliveryRequest { Name = "Medical Equipment", PickupLocation = "890 Medical Center Dr", DeliveryLocation = "123 Hospital Way", DeliveryDate = DateTime.UtcNow.AddDays(2), DeliveryTime = 9, TruckType = "medium", CargoWeight = 2500.75m, DeliveryStatus = "confirmed", DriverName = "Emily Davis", ContactNumber = "555-567-8901" },
                        new AntdTruckDeliveryRequest { Name = "Retail Stock", PickupLocation = "567 Distribution Center", DeliveryLocation = "890 Shopping Mall", DeliveryDate = DateTime.UtcNow.AddDays(4), DeliveryTime = 11, TruckType = "medium", CargoWeight = 4800.00m, DeliveryStatus = "in progress", DriverName = "Robert Garcia", ContactNumber = "555-678-9012" },
                        new AntdTruckDeliveryRequest { Name = "Office Supplies", PickupLocation = "345 Supply Store", DeliveryLocation = "678 Corporate Park", DeliveryDate = DateTime.UtcNow.AddDays(6), DeliveryTime = 13, TruckType = "small", CargoWeight = 850.25m, DeliveryStatus = "pending", DriverName = "Lisa Anderson", ContactNumber = "555-789-0123" },
                        new AntdTruckDeliveryRequest { Name = "Automotive Parts", PickupLocation = "901 Auto Parts Warehouse", DeliveryLocation = "234 Car Dealership", DeliveryDate = DateTime.UtcNow.AddDays(8), DeliveryTime = 15, TruckType = "medium", CargoWeight = 3500.00m, DeliveryStatus = "confirmed", DriverName = "James Wilson", ContactNumber = "555-890-1234" }
                    };
                    context.AntdTruckDeliveryRequests.AddRange(deliveryRequests);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Antd truck delivery requests data seeded successfully");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error seeding Antd data");
                throw;
            }
        }
    }
}
