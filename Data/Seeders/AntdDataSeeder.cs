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
              ProjectDescription =
                "Complete redesign of the e-commerce platform with modern UI/UX, improved performance, and mobile-first approach.",
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
              ProjectDescription =
                "Development of a secure mobile banking application with biometric authentication and real-time transactions.",
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
              ProjectDescription =
                "Migration of legacy healthcare portal to cloud infrastructure with enhanced security compliance.",
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
              ProjectDescription =
                "Real-time marketing analytics dashboard with campaign tracking, ROI calculation, and automated reporting.",
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
              ProjectDescription =
                "Secure document management system for government agencies with audit trails and compliance reporting.",
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
              ProjectDescription =
                "Automated inventory tracking with barcode scanning, stock alerts, and supplier integration.",
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
              ProjectDescription =
                "Interactive e-learning platform with video courses, quizzes, certifications, and progress tracking.",
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
              ProjectDescription =
                "Advanced analytics platform for supply chain optimization with predictive modeling and demand forecasting.",
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
              ProjectDescription =
                "AI-powered chatbot for customer support with natural language processing and ticket escalation.",
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
              ProjectDescription =
                "Comprehensive real estate listing platform with virtual tours, mortgage calculator, and agent management.",
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
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error seeding Antd data");

        throw;
      }
    }
  }
}