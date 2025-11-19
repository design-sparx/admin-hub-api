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
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error seeding Antd data");
                throw;
            }
        }
    }
}
