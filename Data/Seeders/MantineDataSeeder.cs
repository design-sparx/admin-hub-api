using AdminHubApi.Data;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using Microsoft.EntityFrameworkCore;
using TaskStatus = AdminHubApi.Enums.Mantine.TaskStatus;

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
                // Seed Dashboard Stats (20 rows)
                if (!await context.DashboardStats.AnyAsync())
                {
                    logger.LogInformation("Seeding dashboard stats...");
                    var stats = new List<DashboardStats>();
                    var icons = new[] { "IconReceipt2", "IconCoin", "IconDiscount2", "IconUser", "IconShoppingCart", "IconTrendingUp", "IconTrendingDown", "IconChart", "IconCreditCard", "IconGift" };
                    var titles = new[] { "Revenue", "Profit", "Coupons", "Customers", "Orders", "Sales", "Returns", "Growth", "Payments", "Discounts", "Traffic", "Conversions", "Leads", "Support", "Reviews", "Referrals", "Subscriptions", "Cancellations", "Upgrades", "Renewals" };

                    for (int i = 0; i < 20; i++)
                    {
                        stats.Add(new DashboardStats
                        {
                            Title = titles[i],
                            Icon = icons[i % icons.Length],
                            Value = i % 2 == 0 ? $"${(i + 1) * 1000 + 500}" : $"{(i + 1) * 50 + 100}",
                            Diff = (i % 4 == 0) ? -(i + 5) : (i + 10)
                        });
                    }

                    context.DashboardStats.AddRange(stats);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Dashboard stats seeded successfully");
                }

                // Seed Sales Data (20 rows)
                if (!await context.Sales.AnyAsync())
                {
                    logger.LogInformation("Seeding sales data...");
                    var sales = new List<Sales>();
                    var sources = new[] { "Direct sales", "Affiliate program", "Sponsored", "Email campaign", "Social media", "Google Ads", "Facebook Ads", "SEO Organic", "Referrals", "Newsletter", "YouTube", "Instagram", "Twitter", "LinkedIn", "TikTok", "Pinterest", "Reddit", "Podcast", "Webinar", "Trade Show" };

                    for (int i = 0; i < 20; i++)
                    {
                        var amount = (i + 1) * 1000 + (i * 200);
                        sales.Add(new Sales
                        {
                            Source = sources[i],
                            RevenueAmount = amount,
                            RevenueFormatted = $"${amount:N0}",
                            Value = (decimal)(50 + (i * 3.5))
                        });
                    }

                    context.Sales.AddRange(sales);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Sales data seeded successfully");
                }

                // Seed Orders Data (20 rows)
                if (!await context.Orders.AnyAsync())
                {
                    logger.LogInformation("Seeding orders data...");
                    var orders = new List<Orders>();
                    var products = new[] { "Laptop Pro", "Wireless Mouse", "Gaming Keyboard", "Office Chair", "Monitor 27\"", "USB Cable", "Phone Case", "Tablet Stand", "Bluetooth Speaker", "Web Camera", "Desk Lamp", "Power Bank", "Hard Drive", "Router", "Printer", "Scanner", "Headphones", "Microphone", "Smart Watch", "Fitness Tracker" };
                    var statuses = Enum.GetValues<OrderStatus>();
                    var paymentMethods = Enum.GetValues<PaymentMethod>();

                    for (int i = 0; i < 20; i++)
                    {
                        orders.Add(new Orders
                        {
                            Id = Guid.NewGuid(),
                            Product = products[i],
                            Date = DateTime.UtcNow.AddDays(-(i + 1)),
                            Total = (decimal)(29.99 + (i * 15.50)),
                            Status = statuses[i % statuses.Length],
                            PaymentMethod = paymentMethods[i % paymentMethods.Length]
                        });
                    }

                    context.Orders.AddRange(orders);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Orders data seeded successfully");
                }

                // Seed Projects Data (20 rows)
                if (!await context.Projects.AnyAsync())
                {
                    logger.LogInformation("Seeding projects data...");
                    var projects = new List<Projects>();
                    var projectNames = new[] { "Website Redesign", "Mobile App", "API Integration", "Database Migration", "Security Audit", "Performance Optimization", "User Dashboard", "Payment Gateway", "Email System", "Chat Feature", "Analytics Platform", "Admin Panel", "File Upload", "Notification System", "Search Functionality", "User Authentication", "Data Backup", "Cloud Migration", "Testing Suite", "Documentation" };
                    var assignees = new[] { "John Doe", "Jane Smith", "Mike Johnson", "Sarah Wilson", "David Brown", "Emily Davis", "Chris Miller", "Lisa Garcia", "Tom Anderson", "Amy Taylor" };
                    var states = Enum.GetValues<ProjectState>();

                    for (int i = 0; i < 20; i++)
                    {
                        projects.Add(new Projects
                        {
                            Id = Guid.NewGuid(),
                            Name = projectNames[i],
                            StartDate = DateTime.UtcNow.AddDays(-(30 + i)),
                            EndDate = DateTime.UtcNow.AddDays((i % 3 == 0) ? -(i) : (15 + i)),
                            State = states[i % states.Length],
                            Assignee = assignees[i % assignees.Length]
                        });
                    }

                    context.Projects.AddRange(projects);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Projects data seeded successfully");
                }

                // Seed Invoices Data (20 rows) - Using actual entity properties
                if (!await context.Invoices.AnyAsync())
                {
                    logger.LogInformation("Seeding invoices data...");
                    var invoices = new List<Invoices>();
                    var fullNames = new[] { "John Doe", "Jane Smith", "Mike Johnson", "Sarah Wilson", "David Brown", "Emily Davis", "Chris Miller", "Lisa Garcia", "Tom Anderson", "Amy Taylor", "Robert Moore", "Maria Jackson", "Kevin Martin", "Jessica Lee", "Daniel Thompson", "Ashley White", "Ryan Harris", "Michelle Clark", "Steven Lewis", "Nicole Walker" };
                    var companies = new[] { "ABC Corp", "XYZ Ltd", "Tech Solutions", "Digital Agency", "Marketing Pro", "Design Studio", "Web Dev Co", "Software Inc", "Data Systems", "Cloud Services", "Mobile Apps", "E-commerce", "Consulting", "Analytics Co", "Security Firm", "Innovation Lab", "Startup Hub", "Enterprise", "SMB Solutions", "Global Tech" };
                    var countries = new[] { "US", "GB", "CA", "AU", "DE", "FR", "IT", "ES", "NL", "SE" };
                    var statuses = Enum.GetValues<InvoiceStatus>();

                    for (int i = 0; i < 20; i++)
                    {
                        invoices.Add(new Invoices
                        {
                            Id = Guid.NewGuid(),
                            FullName = fullNames[i],
                            Email = $"{fullNames[i].Replace(" ", ".").ToLower()}@example.com",
                            Address = $"{(i + 1) * 100} Business Street, Suite {i + 1}",
                            Country = countries[i % countries.Length],
                            Status = statuses[i % statuses.Length],
                            Amount = (decimal)(500 + (i * 250)),
                            IssueDate = DateTime.UtcNow.AddDays(-(i + 5)),
                            Description = $"Professional services for {companies[i]}",
                            ClientEmail = $"client{i + 1}@{companies[i].Replace(" ", "").ToLower()}.com",
                            ClientAddress = $"{(i + 1) * 200} Client Avenue, Floor {i % 5 + 1}",
                            ClientCountry = countries[i % countries.Length],
                            ClientName = companies[i],
                            ClientCompany = companies[i],
                            CreatedAt = DateTime.UtcNow.AddDays(-(i + 5)),
                            UpdatedAt = DateTime.UtcNow.AddDays(-(i % 3))
                        });
                    }

                    context.Invoices.AddRange(invoices);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Invoices data seeded successfully");
                }

                // Seed Kanban Tasks Data (20 rows) - Using actual entity properties
                if (!await context.KanbanTasks.AnyAsync())
                {
                    logger.LogInformation("Seeding kanban tasks data...");
                    var tasks = new List<KanbanTasks>();
                    var taskNames = new[] { "Fix login bug", "Update documentation", "Code review", "Deploy to staging", "Design mockup", "Database backup", "Security patch", "Performance test", "User feedback", "API endpoint", "UI component", "Data migration", "Error handling", "Integration test", "Feature flag", "Cache optimization", "Log analysis", "Monitoring setup", "Backup restore", "Version update" };
                    var statuses = Enum.GetValues<TaskStatus>();

                    for (int i = 0; i < 20; i++)
                    {
                        tasks.Add(new KanbanTasks
                        {
                            Id = Guid.NewGuid(),
                            Title = taskNames[i],
                            Status = statuses[i % statuses.Length],
                            Comments = i % 5,
                            Users = (i % 3) + 1,
                            CreatedAt = DateTime.UtcNow.AddDays(-(i + 1)),
                            UpdatedAt = DateTime.UtcNow.AddDays(-(i % 3))
                        });
                    }

                    context.KanbanTasks.AddRange(tasks);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Kanban tasks data seeded successfully");
                }

                // Seed Files Data (20 rows) - Using actual entity properties
                if (!await context.Files.AnyAsync())
                {
                    logger.LogInformation("Seeding files data...");
                    var files = new List<Files>();
                    var fileNames = new[] { "project_proposal.pdf", "design_mockup.png", "meeting_notes.docx", "budget_2024.xlsx", "presentation.pptx", "database_schema.sql", "api_documentation.md", "user_manual.pdf", "invoice_template.xlsx", "logo_design.ai", "wireframes.sketch", "test_results.csv", "requirements.docx", "timeline.pdf", "contract.pdf", "specifications.docx", "prototype.fig", "analysis.xlsx", "report.pdf", "backup.zip" };
                    var extensions = new[] { "pdf", "png", "docx", "xlsx", "pptx", "sql", "md", "pdf", "xlsx", "ai", "sketch", "csv", "docx", "pdf", "pdf", "docx", "fig", "xlsx", "pdf", "zip" };

                    for (int i = 0; i < 20; i++)
                    {
                        files.Add(new Files
                        {
                            Id = Guid.NewGuid(),
                            Name = fileNames[i],
                            Size = (i + 1) * 1024 * (i % 5 + 1),
                            Type = extensions[i],
                            Path = $"/uploads/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{fileNames[i]}",
                            OwnerId = Guid.NewGuid(), // Random owner ID
                            FolderId = i % 3 == 0 ? Guid.NewGuid() : null, // Some files in folders
                            CreatedAt = DateTime.UtcNow.AddDays(-(i + 1)),
                            UpdatedAt = DateTime.UtcNow.AddDays(-(i % 3))
                        });
                    }

                    context.Files.AddRange(files);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Files data seeded successfully");
                }

                // Seed Languages Data (20 rows)
                if (!await context.Languages.AnyAsync())
                {
                    logger.LogInformation("Seeding languages data...");
                    var languages = new List<Languages>
                    {
                        new() { Code = "en", Name = "English", NativeName = "English", IsActive = true },
                        new() { Code = "es", Name = "Spanish", NativeName = "Español", IsActive = true },
                        new() { Code = "fr", Name = "French", NativeName = "Français", IsActive = true },
                        new() { Code = "de", Name = "German", NativeName = "Deutsch", IsActive = true },
                        new() { Code = "it", Name = "Italian", NativeName = "Italiano", IsActive = true },
                        new() { Code = "pt", Name = "Portuguese", NativeName = "Português", IsActive = true },
                        new() { Code = "ru", Name = "Russian", NativeName = "Русский", IsActive = true },
                        new() { Code = "ja", Name = "Japanese", NativeName = "日本語", IsActive = true },
                        new() { Code = "ko", Name = "Korean", NativeName = "한국어", IsActive = true },
                        new() { Code = "zh", Name = "Chinese", NativeName = "中文", IsActive = true },
                        new() { Code = "ar", Name = "Arabic", NativeName = "العربية", IsActive = true },
                        new() { Code = "hi", Name = "Hindi", NativeName = "हिन्दी", IsActive = false },
                        new() { Code = "tr", Name = "Turkish", NativeName = "Türkçe", IsActive = false },
                        new() { Code = "pl", Name = "Polish", NativeName = "Polski", IsActive = false },
                        new() { Code = "nl", Name = "Dutch", NativeName = "Nederlands", IsActive = false },
                        new() { Code = "sv", Name = "Swedish", NativeName = "Svenska", IsActive = false },
                        new() { Code = "da", Name = "Danish", NativeName = "Dansk", IsActive = false },
                        new() { Code = "no", Name = "Norwegian", NativeName = "Norsk", IsActive = false },
                        new() { Code = "fi", Name = "Finnish", NativeName = "Suomi", IsActive = false },
                        new() { Code = "cs", Name = "Czech", NativeName = "Čeština", IsActive = false }
                    };

                    context.Languages.AddRange(languages);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Languages data seeded successfully");
                }

                // Seed Countries Data (All countries worldwide)
                if (!await context.Countries.AnyAsync())
                {
                    logger.LogInformation("Seeding countries data...");
                    var countries = new List<Countries>
                    {
                        // Major countries (active by default)
                        new() { Code = "US", Name = "United States", Continent = "North America", Population = 331000000, IsActive = true },
                        new() { Code = "GB", Name = "United Kingdom", Continent = "Europe", Population = 67000000, IsActive = true },
                        new() { Code = "CA", Name = "Canada", Continent = "North America", Population = 38000000, IsActive = true },
                        new() { Code = "AU", Name = "Australia", Continent = "Oceania", Population = 25000000, IsActive = true },
                        new() { Code = "DE", Name = "Germany", Continent = "Europe", Population = 83000000, IsActive = true },
                        new() { Code = "FR", Name = "France", Continent = "Europe", Population = 67000000, IsActive = true },
                        new() { Code = "IT", Name = "Italy", Continent = "Europe", Population = 60000000, IsActive = true },
                        new() { Code = "ES", Name = "Spain", Continent = "Europe", Population = 47000000, IsActive = true },
                        new() { Code = "NL", Name = "Netherlands", Continent = "Europe", Population = 17000000, IsActive = true },
                        new() { Code = "SE", Name = "Sweden", Continent = "Europe", Population = 10000000, IsActive = true },
                        new() { Code = "NO", Name = "Norway", Continent = "Europe", Population = 5000000, IsActive = true },
                        new() { Code = "DK", Name = "Denmark", Continent = "Europe", Population = 6000000, IsActive = true },
                        new() { Code = "FI", Name = "Finland", Continent = "Europe", Population = 5500000, IsActive = true },
                        new() { Code = "JP", Name = "Japan", Continent = "Asia", Population = 125000000, IsActive = true },
                        new() { Code = "KR", Name = "South Korea", Continent = "Asia", Population = 51000000, IsActive = true },
                        new() { Code = "CN", Name = "China", Continent = "Asia", Population = 1400000000, IsActive = true },
                        new() { Code = "IN", Name = "India", Continent = "Asia", Population = 1380000000, IsActive = true },
                        new() { Code = "BR", Name = "Brazil", Continent = "South America", Population = 212000000, IsActive = true },
                        new() { Code = "MX", Name = "Mexico", Continent = "North America", Population = 128000000, IsActive = true },
                        new() { Code = "RU", Name = "Russia", Continent = "Europe", Population = 146000000, IsActive = true },

                        // All other countries alphabetically (inactive by default)
                        new() { Code = "AD", Name = "Andorra", Continent = "Europe", Population = 77000, IsActive = false },
                        new() { Code = "AE", Name = "United Arab Emirates", Continent = "Asia", Population = 9890000, IsActive = false },
                        new() { Code = "AF", Name = "Afghanistan", Continent = "Asia", Population = 38928000, IsActive = false },
                        new() { Code = "AG", Name = "Antigua and Barbuda", Continent = "North America", Population = 97000, IsActive = false },
                        new() { Code = "AI", Name = "Anguilla", Continent = "North America", Population = 15000, IsActive = false },
                        new() { Code = "AL", Name = "Albania", Continent = "Europe", Population = 2877000, IsActive = false },
                        new() { Code = "AM", Name = "Armenia", Continent = "Asia", Population = 2963000, IsActive = false },
                        new() { Code = "AO", Name = "Angola", Continent = "Africa", Population = 32866000, IsActive = false },
                        new() { Code = "AQ", Name = "Antarctica", Continent = "Antarctica", Population = 0, IsActive = false },
                        new() { Code = "AR", Name = "Argentina", Continent = "South America", Population = 45195000, IsActive = false },
                        new() { Code = "AS", Name = "American Samoa", Continent = "Oceania", Population = 55000, IsActive = false },
                        new() { Code = "AT", Name = "Austria", Continent = "Europe", Population = 9006000, IsActive = false },
                        new() { Code = "AW", Name = "Aruba", Continent = "North America", Population = 107000, IsActive = false },
                        new() { Code = "AX", Name = "Åland Islands", Continent = "Europe", Population = 30000, IsActive = false },
                        new() { Code = "AZ", Name = "Azerbaijan", Continent = "Asia", Population = 10139000, IsActive = false },
                        new() { Code = "BA", Name = "Bosnia and Herzegovina", Continent = "Europe", Population = 3280000, IsActive = false },
                        new() { Code = "BB", Name = "Barbados", Continent = "North America", Population = 287000, IsActive = false },
                        new() { Code = "BD", Name = "Bangladesh", Continent = "Asia", Population = 164689000, IsActive = false },
                        new() { Code = "BE", Name = "Belgium", Continent = "Europe", Population = 11589000, IsActive = false },
                        new() { Code = "BF", Name = "Burkina Faso", Continent = "Africa", Population = 20903000, IsActive = false },
                        new() { Code = "BG", Name = "Bulgaria", Continent = "Europe", Population = 6948000, IsActive = false },
                        new() { Code = "BH", Name = "Bahrain", Continent = "Asia", Population = 1701000, IsActive = false },
                        new() { Code = "BI", Name = "Burundi", Continent = "Africa", Population = 11890000, IsActive = false },
                        new() { Code = "BJ", Name = "Benin", Continent = "Africa", Population = 12123000, IsActive = false },
                        new() { Code = "BL", Name = "Saint Barthélemy", Continent = "North America", Population = 10000, IsActive = false },
                        new() { Code = "BM", Name = "Bermuda", Continent = "North America", Population = 62000, IsActive = false },
                        new() { Code = "BN", Name = "Brunei", Continent = "Asia", Population = 437000, IsActive = false },
                        new() { Code = "BO", Name = "Bolivia", Continent = "South America", Population = 11673000, IsActive = false },
                        new() { Code = "BQ", Name = "Bonaire, Sint Eustatius and Saba", Continent = "North America", Population = 26000, IsActive = false },
                        new() { Code = "BS", Name = "Bahamas", Continent = "North America", Population = 393000, IsActive = false },
                        new() { Code = "BT", Name = "Bhutan", Continent = "Asia", Population = 772000, IsActive = false },
                        new() { Code = "BV", Name = "Bouvet Island", Continent = "Antarctica", Population = 0, IsActive = false },
                        new() { Code = "BW", Name = "Botswana", Continent = "Africa", Population = 2351000, IsActive = false },
                        new() { Code = "BY", Name = "Belarus", Continent = "Europe", Population = 9449000, IsActive = false },
                        new() { Code = "BZ", Name = "Belize", Continent = "North America", Population = 397000, IsActive = false },
                        new() { Code = "CC", Name = "Cocos (Keeling) Islands", Continent = "Asia", Population = 600, IsActive = false },
                        new() { Code = "CD", Name = "Democratic Republic of the Congo", Continent = "Africa", Population = 89561000, IsActive = false },
                        new() { Code = "CF", Name = "Central African Republic", Continent = "Africa", Population = 4829000, IsActive = false },
                        new() { Code = "CG", Name = "Republic of the Congo", Continent = "Africa", Population = 5518000, IsActive = false },
                        new() { Code = "CH", Name = "Switzerland", Continent = "Europe", Population = 8655000, IsActive = false },
                        new() { Code = "CI", Name = "Côte d'Ivoire", Continent = "Africa", Population = 26378000, IsActive = false },
                        new() { Code = "CK", Name = "Cook Islands", Continent = "Oceania", Population = 17000, IsActive = false },
                        new() { Code = "CL", Name = "Chile", Continent = "South America", Population = 19116000, IsActive = false },
                        new() { Code = "CM", Name = "Cameroon", Continent = "Africa", Population = 26545000, IsActive = false },
                        new() { Code = "CO", Name = "Colombia", Continent = "South America", Population = 50883000, IsActive = false },
                        new() { Code = "CR", Name = "Costa Rica", Continent = "North America", Population = 5094000, IsActive = false },
                        new() { Code = "CU", Name = "Cuba", Continent = "North America", Population = 11327000, IsActive = false },
                        new() { Code = "CV", Name = "Cape Verde", Continent = "Africa", Population = 556000, IsActive = false },
                        new() { Code = "CW", Name = "Curaçao", Continent = "North America", Population = 164000, IsActive = false },
                        new() { Code = "CX", Name = "Christmas Island", Continent = "Asia", Population = 2000, IsActive = false },
                        new() { Code = "CY", Name = "Cyprus", Continent = "Europe", Population = 1207000, IsActive = false },
                        new() { Code = "CZ", Name = "Czech Republic", Continent = "Europe", Population = 10709000, IsActive = false },
                        new() { Code = "DJ", Name = "Djibouti", Continent = "Africa", Population = 988000, IsActive = false },
                        new() { Code = "DM", Name = "Dominica", Continent = "North America", Population = 72000, IsActive = false },
                        new() { Code = "DO", Name = "Dominican Republic", Continent = "North America", Population = 10848000, IsActive = false },
                        new() { Code = "DZ", Name = "Algeria", Continent = "Africa", Population = 43851000, IsActive = false },
                        new() { Code = "EC", Name = "Ecuador", Continent = "South America", Population = 17643000, IsActive = false },
                        new() { Code = "EE", Name = "Estonia", Continent = "Europe", Population = 1327000, IsActive = false },
                        new() { Code = "EG", Name = "Egypt", Continent = "Africa", Population = 102334000, IsActive = false },
                        new() { Code = "EH", Name = "Western Sahara", Continent = "Africa", Population = 597000, IsActive = false },
                        new() { Code = "ER", Name = "Eritrea", Continent = "Africa", Population = 3546000, IsActive = false },
                        new() { Code = "ET", Name = "Ethiopia", Continent = "Africa", Population = 115000000, IsActive = false },
                        new() { Code = "FJ", Name = "Fiji", Continent = "Oceania", Population = 896000, IsActive = false },
                        new() { Code = "FK", Name = "Falkland Islands", Continent = "South America", Population = 3000, IsActive = false },
                        new() { Code = "FM", Name = "Micronesia", Continent = "Oceania", Population = 115000, IsActive = false },
                        new() { Code = "FO", Name = "Faroe Islands", Continent = "Europe", Population = 49000, IsActive = false },
                        new() { Code = "GA", Name = "Gabon", Continent = "Africa", Population = 2226000, IsActive = false },
                        new() { Code = "GD", Name = "Grenada", Continent = "North America", Population = 112000, IsActive = false },
                        new() { Code = "GE", Name = "Georgia", Continent = "Asia", Population = 3989000, IsActive = false },
                        new() { Code = "GF", Name = "French Guiana", Continent = "South America", Population = 299000, IsActive = false },
                        new() { Code = "GG", Name = "Guernsey", Continent = "Europe", Population = 63000, IsActive = false },
                        new() { Code = "GH", Name = "Ghana", Continent = "Africa", Population = 31073000, IsActive = false },
                        new() { Code = "GI", Name = "Gibraltar", Continent = "Europe", Population = 34000, IsActive = false },
                        new() { Code = "GL", Name = "Greenland", Continent = "North America", Population = 56000, IsActive = false },
                        new() { Code = "GM", Name = "Gambia", Continent = "Africa", Population = 2417000, IsActive = false },
                        new() { Code = "GN", Name = "Guinea", Continent = "Africa", Population = 13133000, IsActive = false },
                        new() { Code = "GP", Name = "Guadeloupe", Continent = "North America", Population = 400000, IsActive = false },
                        new() { Code = "GQ", Name = "Equatorial Guinea", Continent = "Africa", Population = 1403000, IsActive = false },
                        new() { Code = "GR", Name = "Greece", Continent = "Europe", Population = 10424000, IsActive = false },
                        new() { Code = "GS", Name = "South Georgia and the South Sandwich Islands", Continent = "Antarctica", Population = 0, IsActive = false },
                        new() { Code = "GT", Name = "Guatemala", Continent = "North America", Population = 16858000, IsActive = false },
                        new() { Code = "GU", Name = "Guam", Continent = "Oceania", Population = 169000, IsActive = false },
                        new() { Code = "GW", Name = "Guinea-Bissau", Continent = "Africa", Population = 1968000, IsActive = false },
                        new() { Code = "GY", Name = "Guyana", Continent = "South America", Population = 787000, IsActive = false },
                        new() { Code = "HK", Name = "Hong Kong", Continent = "Asia", Population = 7497000, IsActive = false },
                        new() { Code = "HM", Name = "Heard Island and McDonald Islands", Continent = "Antarctica", Population = 0, IsActive = false },
                        new() { Code = "HN", Name = "Honduras", Continent = "North America", Population = 9905000, IsActive = false },
                        new() { Code = "HR", Name = "Croatia", Continent = "Europe", Population = 4105000, IsActive = false },
                        new() { Code = "HT", Name = "Haiti", Continent = "North America", Population = 11403000, IsActive = false },
                        new() { Code = "HU", Name = "Hungary", Continent = "Europe", Population = 9660000, IsActive = false },
                        new() { Code = "ID", Name = "Indonesia", Continent = "Asia", Population = 273524000, IsActive = false },
                        new() { Code = "IE", Name = "Ireland", Continent = "Europe", Population = 4938000, IsActive = false },
                        new() { Code = "IL", Name = "Israel", Continent = "Asia", Population = 8656000, IsActive = false },
                        new() { Code = "IM", Name = "Isle of Man", Continent = "Europe", Population = 85000, IsActive = false },
                        new() { Code = "IO", Name = "British Indian Ocean Territory", Continent = "Asia", Population = 0, IsActive = false },
                        new() { Code = "IQ", Name = "Iraq", Continent = "Asia", Population = 40223000, IsActive = false },
                        new() { Code = "IR", Name = "Iran", Continent = "Asia", Population = 83993000, IsActive = false },
                        new() { Code = "IS", Name = "Iceland", Continent = "Europe", Population = 341000, IsActive = false },
                        new() { Code = "JE", Name = "Jersey", Continent = "Europe", Population = 101000, IsActive = false },
                        new() { Code = "JM", Name = "Jamaica", Continent = "North America", Population = 2961000, IsActive = false },
                        new() { Code = "JO", Name = "Jordan", Continent = "Asia", Population = 10203000, IsActive = false },
                        new() { Code = "KE", Name = "Kenya", Continent = "Africa", Population = 53771000, IsActive = false },
                        new() { Code = "KG", Name = "Kyrgyzstan", Continent = "Asia", Population = 6524000, IsActive = false },
                        new() { Code = "KH", Name = "Cambodia", Continent = "Asia", Population = 16719000, IsActive = false },
                        new() { Code = "KI", Name = "Kiribati", Continent = "Oceania", Population = 119000, IsActive = false },
                        new() { Code = "KM", Name = "Comoros", Continent = "Africa", Population = 870000, IsActive = false },
                        new() { Code = "KN", Name = "Saint Kitts and Nevis", Continent = "North America", Population = 53000, IsActive = false },
                        new() { Code = "KP", Name = "North Korea", Continent = "Asia", Population = 25778000, IsActive = false },
                        new() { Code = "KW", Name = "Kuwait", Continent = "Asia", Population = 4271000, IsActive = false },
                        new() { Code = "KY", Name = "Cayman Islands", Continent = "North America", Population = 65000, IsActive = false },
                        new() { Code = "KZ", Name = "Kazakhstan", Continent = "Asia", Population = 18777000, IsActive = false },
                        new() { Code = "LA", Name = "Laos", Continent = "Asia", Population = 7276000, IsActive = false },
                        new() { Code = "LB", Name = "Lebanon", Continent = "Asia", Population = 6825000, IsActive = false },
                        new() { Code = "LC", Name = "Saint Lucia", Continent = "North America", Population = 184000, IsActive = false },
                        new() { Code = "LI", Name = "Liechtenstein", Continent = "Europe", Population = 38000, IsActive = false },
                        new() { Code = "LK", Name = "Sri Lanka", Continent = "Asia", Population = 21413000, IsActive = false },
                        new() { Code = "LR", Name = "Liberia", Continent = "Africa", Population = 5058000, IsActive = false },
                        new() { Code = "LS", Name = "Lesotho", Continent = "Africa", Population = 2142000, IsActive = false },
                        new() { Code = "LT", Name = "Lithuania", Continent = "Europe", Population = 2722000, IsActive = false },
                        new() { Code = "LU", Name = "Luxembourg", Continent = "Europe", Population = 626000, IsActive = false },
                        new() { Code = "LV", Name = "Latvia", Continent = "Europe", Population = 1886000, IsActive = false },
                        new() { Code = "LY", Name = "Libya", Continent = "Africa", Population = 6871000, IsActive = false },
                        new() { Code = "MA", Name = "Morocco", Continent = "Africa", Population = 36910000, IsActive = false },
                        new() { Code = "MC", Name = "Monaco", Continent = "Europe", Population = 39000, IsActive = false },
                        new() { Code = "MD", Name = "Moldova", Continent = "Europe", Population = 4034000, IsActive = false },
                        new() { Code = "ME", Name = "Montenegro", Continent = "Europe", Population = 628000, IsActive = false },
                        new() { Code = "MF", Name = "Saint Martin", Continent = "North America", Population = 38000, IsActive = false },
                        new() { Code = "MG", Name = "Madagascar", Continent = "Africa", Population = 27691000, IsActive = false },
                        new() { Code = "MH", Name = "Marshall Islands", Continent = "Oceania", Population = 59000, IsActive = false },
                        new() { Code = "MK", Name = "North Macedonia", Continent = "Europe", Population = 2083000, IsActive = false },
                        new() { Code = "ML", Name = "Mali", Continent = "Africa", Population = 20251000, IsActive = false },
                        new() { Code = "MM", Name = "Myanmar", Continent = "Asia", Population = 54410000, IsActive = false },
                        new() { Code = "MN", Name = "Mongolia", Continent = "Asia", Population = 3278000, IsActive = false },
                        new() { Code = "MO", Name = "Macao", Continent = "Asia", Population = 649000, IsActive = false },
                        new() { Code = "MP", Name = "Northern Mariana Islands", Continent = "Oceania", Population = 57000, IsActive = false },
                        new() { Code = "MQ", Name = "Martinique", Continent = "North America", Population = 375000, IsActive = false },
                        new() { Code = "MR", Name = "Mauritania", Continent = "Africa", Population = 4650000, IsActive = false },
                        new() { Code = "MS", Name = "Montserrat", Continent = "North America", Population = 5000, IsActive = false },
                        new() { Code = "MT", Name = "Malta", Continent = "Europe", Population = 442000, IsActive = false },
                        new() { Code = "MU", Name = "Mauritius", Continent = "Africa", Population = 1271000, IsActive = false },
                        new() { Code = "MV", Name = "Maldives", Continent = "Asia", Population = 540000, IsActive = false },
                        new() { Code = "MW", Name = "Malawi", Continent = "Africa", Population = 19130000, IsActive = false },
                        new() { Code = "MY", Name = "Malaysia", Continent = "Asia", Population = 32366000, IsActive = false },
                        new() { Code = "MZ", Name = "Mozambique", Continent = "Africa", Population = 31255000, IsActive = false },
                        new() { Code = "NA", Name = "Namibia", Continent = "Africa", Population = 2541000, IsActive = false },
                        new() { Code = "NC", Name = "New Caledonia", Continent = "Oceania", Population = 285000, IsActive = false },
                        new() { Code = "NE", Name = "Niger", Continent = "Africa", Population = 24207000, IsActive = false },
                        new() { Code = "NF", Name = "Norfolk Island", Continent = "Oceania", Population = 2000, IsActive = false },
                        new() { Code = "NG", Name = "Nigeria", Continent = "Africa", Population = 206140000, IsActive = false },
                        new() { Code = "NI", Name = "Nicaragua", Continent = "North America", Population = 6625000, IsActive = false },
                        new() { Code = "NP", Name = "Nepal", Continent = "Asia", Population = 29137000, IsActive = false },
                        new() { Code = "NR", Name = "Nauru", Continent = "Oceania", Population = 11000, IsActive = false },
                        new() { Code = "NU", Name = "Niue", Continent = "Oceania", Population = 2000, IsActive = false },
                        new() { Code = "NZ", Name = "New Zealand", Continent = "Oceania", Population = 4822000, IsActive = false },
                        new() { Code = "OM", Name = "Oman", Continent = "Asia", Population = 5107000, IsActive = false },
                        new() { Code = "PA", Name = "Panama", Continent = "North America", Population = 4315000, IsActive = false },
                        new() { Code = "PE", Name = "Peru", Continent = "South America", Population = 32972000, IsActive = false },
                        new() { Code = "PF", Name = "French Polynesia", Continent = "Oceania", Population = 280000, IsActive = false },
                        new() { Code = "PG", Name = "Papua New Guinea", Continent = "Oceania", Population = 8947000, IsActive = false },
                        new() { Code = "PH", Name = "Philippines", Continent = "Asia", Population = 109581000, IsActive = false },
                        new() { Code = "PK", Name = "Pakistan", Continent = "Asia", Population = 220892000, IsActive = false },
                        new() { Code = "PL", Name = "Poland", Continent = "Europe", Population = 37847000, IsActive = false },
                        new() { Code = "PM", Name = "Saint Pierre and Miquelon", Continent = "North America", Population = 6000, IsActive = false },
                        new() { Code = "PN", Name = "Pitcairn", Continent = "Oceania", Population = 50, IsActive = false },
                        new() { Code = "PR", Name = "Puerto Rico", Continent = "North America", Population = 2861000, IsActive = false },
                        new() { Code = "PS", Name = "Palestine", Continent = "Asia", Population = 5101000, IsActive = false },
                        new() { Code = "PT", Name = "Portugal", Continent = "Europe", Population = 10196000, IsActive = false },
                        new() { Code = "PW", Name = "Palau", Continent = "Oceania", Population = 18000, IsActive = false },
                        new() { Code = "PY", Name = "Paraguay", Continent = "South America", Population = 7133000, IsActive = false },
                        new() { Code = "QA", Name = "Qatar", Continent = "Asia", Population = 2881000, IsActive = false },
                        new() { Code = "RE", Name = "Réunion", Continent = "Africa", Population = 895000, IsActive = false },
                        new() { Code = "RO", Name = "Romania", Continent = "Europe", Population = 19238000, IsActive = false },
                        new() { Code = "RS", Name = "Serbia", Continent = "Europe", Population = 8738000, IsActive = false },
                        new() { Code = "RW", Name = "Rwanda", Continent = "Africa", Population = 12952000, IsActive = false },
                        new() { Code = "SA", Name = "Saudi Arabia", Continent = "Asia", Population = 34814000, IsActive = false },
                        new() { Code = "SB", Name = "Solomon Islands", Continent = "Oceania", Population = 687000, IsActive = false },
                        new() { Code = "SC", Name = "Seychelles", Continent = "Africa", Population = 98000, IsActive = false },
                        new() { Code = "SD", Name = "Sudan", Continent = "Africa", Population = 43849000, IsActive = false },
                        new() { Code = "SG", Name = "Singapore", Continent = "Asia", Population = 5850000, IsActive = false },
                        new() { Code = "SH", Name = "Saint Helena, Ascension and Tristan da Cunha", Continent = "Africa", Population = 6000, IsActive = false },
                        new() { Code = "SI", Name = "Slovenia", Continent = "Europe", Population = 2079000, IsActive = false },
                        new() { Code = "SJ", Name = "Svalbard and Jan Mayen", Continent = "Europe", Population = 3000, IsActive = false },
                        new() { Code = "SK", Name = "Slovakia", Continent = "Europe", Population = 5460000, IsActive = false },
                        new() { Code = "SL", Name = "Sierra Leone", Continent = "Africa", Population = 7976000, IsActive = false },
                        new() { Code = "SM", Name = "San Marino", Continent = "Europe", Population = 34000, IsActive = false },
                        new() { Code = "SN", Name = "Senegal", Continent = "Africa", Population = 16744000, IsActive = false },
                        new() { Code = "SO", Name = "Somalia", Continent = "Africa", Population = 15893000, IsActive = false },
                        new() { Code = "SR", Name = "Suriname", Continent = "South America", Population = 587000, IsActive = false },
                        new() { Code = "SS", Name = "South Sudan", Continent = "Africa", Population = 11194000, IsActive = false },
                        new() { Code = "ST", Name = "São Tomé and Príncipe", Continent = "Africa", Population = 219000, IsActive = false },
                        new() { Code = "SV", Name = "El Salvador", Continent = "North America", Population = 6486000, IsActive = false },
                        new() { Code = "SX", Name = "Sint Maarten", Continent = "North America", Population = 42000, IsActive = false },
                        new() { Code = "SY", Name = "Syria", Continent = "Asia", Population = 17501000, IsActive = false },
                        new() { Code = "SZ", Name = "Eswatini", Continent = "Africa", Population = 1160000, IsActive = false },
                        new() { Code = "TC", Name = "Turks and Caicos Islands", Continent = "North America", Population = 38000, IsActive = false },
                        new() { Code = "TD", Name = "Chad", Continent = "Africa", Population = 16425000, IsActive = false },
                        new() { Code = "TF", Name = "French Southern Territories", Continent = "Antarctica", Population = 0, IsActive = false },
                        new() { Code = "TG", Name = "Togo", Continent = "Africa", Population = 8279000, IsActive = false },
                        new() { Code = "TH", Name = "Thailand", Continent = "Asia", Population = 69800000, IsActive = false },
                        new() { Code = "TJ", Name = "Tajikistan", Continent = "Asia", Population = 9538000, IsActive = false },
                        new() { Code = "TK", Name = "Tokelau", Continent = "Oceania", Population = 1000, IsActive = false },
                        new() { Code = "TL", Name = "Timor-Leste", Continent = "Asia", Population = 1318000, IsActive = false },
                        new() { Code = "TM", Name = "Turkmenistan", Continent = "Asia", Population = 6032000, IsActive = false },
                        new() { Code = "TN", Name = "Tunisia", Continent = "Africa", Population = 11819000, IsActive = false },
                        new() { Code = "TO", Name = "Tonga", Continent = "Oceania", Population = 105000, IsActive = false },
                        new() { Code = "TR", Name = "Turkey", Continent = "Asia", Population = 84339000, IsActive = false },
                        new() { Code = "TT", Name = "Trinidad and Tobago", Continent = "North America", Population = 1399000, IsActive = false },
                        new() { Code = "TV", Name = "Tuvalu", Continent = "Oceania", Population = 12000, IsActive = false },
                        new() { Code = "TW", Name = "Taiwan", Continent = "Asia", Population = 23817000, IsActive = false },
                        new() { Code = "TZ", Name = "Tanzania", Continent = "Africa", Population = 59734000, IsActive = false },
                        new() { Code = "UA", Name = "Ukraine", Continent = "Europe", Population = 43734000, IsActive = false },
                        new() { Code = "UG", Name = "Uganda", Continent = "Africa", Population = 45741000, IsActive = false },
                        new() { Code = "UM", Name = "United States Minor Outlying Islands", Continent = "Oceania", Population = 0, IsActive = false },
                        new() { Code = "UY", Name = "Uruguay", Continent = "South America", Population = 3474000, IsActive = false },
                        new() { Code = "UZ", Name = "Uzbekistan", Continent = "Asia", Population = 33469000, IsActive = false },
                        new() { Code = "VA", Name = "Vatican City", Continent = "Europe", Population = 800, IsActive = false },
                        new() { Code = "VC", Name = "Saint Vincent and the Grenadines", Continent = "North America", Population = 111000, IsActive = false },
                        new() { Code = "VE", Name = "Venezuela", Continent = "South America", Population = 28436000, IsActive = false },
                        new() { Code = "VG", Name = "British Virgin Islands", Continent = "North America", Population = 30000, IsActive = false },
                        new() { Code = "VI", Name = "U.S. Virgin Islands", Continent = "North America", Population = 107000, IsActive = false },
                        new() { Code = "VN", Name = "Vietnam", Continent = "Asia", Population = 97339000, IsActive = false },
                        new() { Code = "VU", Name = "Vanuatu", Continent = "Oceania", Population = 307000, IsActive = false },
                        new() { Code = "WF", Name = "Wallis and Futuna", Continent = "Oceania", Population = 11000, IsActive = false },
                        new() { Code = "WS", Name = "Samoa", Continent = "Oceania", Population = 198000, IsActive = false },
                        new() { Code = "YE", Name = "Yemen", Continent = "Asia", Population = 29826000, IsActive = false },
                        new() { Code = "YT", Name = "Mayotte", Continent = "Africa", Population = 273000, IsActive = false },
                        new() { Code = "ZA", Name = "South Africa", Continent = "Africa", Population = 59309000, IsActive = false },
                        new() { Code = "ZM", Name = "Zambia", Continent = "Africa", Population = 18384000, IsActive = false },
                        new() { Code = "ZW", Name = "Zimbabwe", Continent = "Africa", Population = 14863000, IsActive = false }
                    };

                    context.Countries.AddRange(countries);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Countries data seeded successfully - {countries.Count} countries added");
                }

                logger.LogInformation("Mantine data seeding completed successfully - All entities seeded with realistic data");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while seeding Mantine data");
                throw;
            }
        }
    }
}