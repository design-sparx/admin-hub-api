using AdminHubApi.Data;
using AdminHubApi.Entities.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data.Seeders
{
    public static class MantineDataSeeder
    {
        public static async Task SeedMantineDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                // Seed Dashboard Stats
                if (!await context.DashboardStats.AnyAsync())
                {
                    logger.LogInformation("Seeding dashboard stats...");
                    var stats = new List<DashboardStats>
                    {
                        new() { Title = "Revenue", Icon = "IconReceipt2", Value = "$13,456", Diff = 34 },
                        new() { Title = "Profit", Icon = "IconCoin", Value = "$4,145", Diff = -13 },
                        new() { Title = "Coupons usage", Icon = "IconDiscount2", Value = "745", Diff = 18 },
                        new() { Title = "New customers", Icon = "IconUser", Value = "188", Diff = -30 }
                    };

                    context.DashboardStats.AddRange(stats);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Dashboard stats seeded successfully");
                }

                // Seed Sales Data
                if (!await context.Sales.AnyAsync())
                {
                    logger.LogInformation("Seeding sales data...");
                    var sales = new List<Sales>
                    {
                        new() { Source = "Direct sales", RevenueAmount = 5856, RevenueFormatted = "$5,856", Value = 79.2m },
                        new() { Source = "Affiliate program", RevenueAmount = 3842, RevenueFormatted = "$3,842", Value = 60.8m },
                        new() { Source = "Sponsored", RevenueAmount = 982, RevenueFormatted = "$982", Value = 15.4m },
                        new() { Source = "Email campaign", RevenueAmount = 2174, RevenueFormatted = "$2,174", Value = 34.6m },
                        new() { Source = "Social media", RevenueAmount = 1569, RevenueFormatted = "$1,569", Value = 25.2m }
                    };

                    context.Sales.AddRange(sales);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Sales data seeded successfully");
                }

                // Seed Orders Data
                if (!await context.Orders.AnyAsync())
                {
                    logger.LogInformation("Seeding orders data...");
                    var orders = new List<Order>
                    {
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product = "Awesome Concrete Shoes",
                            Date = DateTime.UtcNow.AddDays(-2),
                            Total = 64.99m,
                            Status = OrderStatus.Delivered,
                            PaymentMethod = PaymentMethod.CreditCard
                        },
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product = "Practical Granite Mouse",
                            Date = DateTime.UtcNow.AddDays(-1),
                            Total = 32.50m,
                            Status = OrderStatus.Processing,
                            PaymentMethod = PaymentMethod.PayPal
                        },
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product = "Incredible Steel Shirt",
                            Date = DateTime.UtcNow.AddHours(-6),
                            Total = 89.99m,
                            Status = OrderStatus.Shipped,
                            PaymentMethod = PaymentMethod.ApplePay
                        }
                    };

                    context.Orders.AddRange(orders);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Orders data seeded successfully");
                }

                // Seed Projects Data
                if (!await context.Projects.AnyAsync())
                {
                    logger.LogInformation("Seeding projects data...");
                    var projects = new List<Project>
                    {
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Migration to React 18",
                            StartDate = DateTime.UtcNow.AddDays(-30),
                            EndDate = DateTime.UtcNow.AddDays(15),
                            State = ProjectState.InProgress,
                            Assignee = "John Doe"
                        },
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Analytics Dashboard",
                            StartDate = DateTime.UtcNow.AddDays(-60),
                            EndDate = DateTime.UtcNow.AddDays(-10),
                            State = ProjectState.Completed,
                            Assignee = "Jane Smith"
                        },
                        new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "API Documentation",
                            StartDate = DateTime.UtcNow.AddDays(5),
                            EndDate = DateTime.UtcNow.AddDays(35),
                            State = ProjectState.Pending,
                            Assignee = "Mike Johnson"
                        }
                    };

                    context.Projects.AddRange(projects);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Projects data seeded successfully");
                }

                logger.LogInformation("Mantine data seeding completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while seeding Mantine data");
                throw;
            }
        }
    }
}