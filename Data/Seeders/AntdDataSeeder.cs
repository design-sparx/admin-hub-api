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
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error seeding Antd data");
                throw;
            }
        }
    }
}
