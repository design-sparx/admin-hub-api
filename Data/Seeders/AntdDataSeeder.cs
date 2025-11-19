using AdminHubApi.Entities.Antd;
using AdminHubApi.Enums.Antd;
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
                await SeedTasksAsync(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while seeding Antd data");
                throw;
            }
        }

        private static async Task SeedTasksAsync(ApplicationDbContext context, ILogger logger)
        {
            if (await context.AntdTasks.AnyAsync())
            {
                logger.LogInformation("Antd tasks data already exists, skipping seeding");
                return;
            }

            logger.LogInformation("Seeding Antd tasks data...");

            var priorities = Enum.GetValues<AntdTaskPriority>();
            var statuses = Enum.GetValues<AntdTaskStatus>();
            var categories = Enum.GetValues<AntdTaskCategory>();
            var colors = Enum.GetValues<AntdTaskColor>();

            var assignees = new[]
            {
                "Florina Kirtlan", "Randi Asbrey", "Flory Nollet", "Chaunce Corcoran",
                "Vite Dumberell", "Con Duckering", "Robby Paffot", "David Gapper",
                "Annie Oxbie", "Corey Wrathall", "Aloin Binge", "Kev Lamberti",
                "Susana Dartnall", "Kara-lynn Whitten", "Lorna Salmoni", "Trefor Bentham",
                "Marjie Eccersley", "Gianna Howman", "Quillan Baiyle", "Kordula McAline"
            };

            var taskNames = new[]
            {
                "Review quarterly sales report",
                "Update customer database records",
                "Prepare marketing presentation",
                "Conduct team performance reviews",
                "Implement new security protocols",
                "Optimize database queries",
                "Design new landing page",
                "Create social media content calendar",
                "Analyze competitor pricing",
                "Document API endpoints",
                "Test mobile app functionality",
                "Review vendor contracts",
                "Update employee handbook",
                "Plan Q4 budget allocation",
                "Organize team building event",
                "Audit inventory levels",
                "Implement feedback system",
                "Create onboarding materials",
                "Review insurance policies",
                "Setup automated testing"
            };

            var random = new Random(42);
            var tasks = new List<AntdTask>();

            for (int i = 0; i < 100; i++)
            {
                var dueDate = DateTime.UtcNow.AddDays(random.Next(-60, 90));
                var status = statuses[random.Next(statuses.Length)];
                var completedDate = status == AntdTaskStatus.Completed
                    ? dueDate.AddDays(random.Next(-5, 5))
                    : (DateTime?)null;

                tasks.Add(new AntdTask
                {
                    Id = Guid.NewGuid(),
                    Name = taskNames[random.Next(taskNames.Length)] + $" - Task {i + 1}",
                    Description = $"This is a detailed description for task {i + 1}. It contains important information about the task requirements and expected outcomes.",
                    Priority = priorities[random.Next(priorities.Length)],
                    DueDate = dueDate,
                    AssignedTo = assignees[random.Next(assignees.Length)],
                    Status = status,
                    Notes = $"Additional notes for task {i + 1}. Please review carefully before proceeding.",
                    Category = categories[random.Next(categories.Length)],
                    Duration = Math.Round((decimal)(random.NextDouble() * 24), 2),
                    CompletedDate = completedDate,
                    Color = colors[random.Next(colors.Length)],
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 90)),
                    UpdatedAt = DateTime.UtcNow
                });
            }

            context.AntdTasks.AddRange(tasks);
            await context.SaveChangesAsync();
            logger.LogInformation("Antd tasks data seeded successfully with {Count} records", tasks.Count);
        }
    }
}
