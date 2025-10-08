using AdminHubApi.Data;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data.Seeders
{
    public static class MantineChatSeeder
    {
        public static async Task SeedChatsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                if (!await context.Chats.AnyAsync())
                {
                    logger.LogInformation("Seeding Mantine chats...");

                    // Create some predefined chat IDs and sender IDs
                    var chat1Id = Guid.NewGuid();
                    var chat2Id = Guid.NewGuid();
                    var chat3Id = Guid.NewGuid();
                    var chat4Id = Guid.NewGuid();
                    var chat5Id = Guid.NewGuid();

                    var user1Id = Guid.NewGuid();
                    var user2Id = Guid.NewGuid();
                    var user3Id = Guid.NewGuid();
                    var user4Id = Guid.NewGuid();
                    var user5Id = Guid.NewGuid();

                    // Create Chats
                    var chats = new List<Chats>
                    {
                        new Chats
                        {
                            Id = chat1Id,
                            Name = "Project Team",
                            Type = ChatType.Group,
                            ParticipantCount = 5,
                            LastMessage = "Let's schedule a meeting for tomorrow",
                            LastMessageAt = DateTime.UtcNow.AddMinutes(-15),
                            CreatedAt = DateTime.UtcNow.AddDays(-10)
                        },
                        new Chats
                        {
                            Id = chat2Id,
                            Name = "Sarah Johnson",
                            Type = ChatType.Direct,
                            ParticipantCount = 2,
                            LastMessage = "Thanks for the update!",
                            LastMessageAt = DateTime.UtcNow.AddHours(-2),
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        },
                        new Chats
                        {
                            Id = chat3Id,
                            Name = "Development Team",
                            Type = ChatType.Group,
                            ParticipantCount = 8,
                            LastMessage = "Code review completed",
                            LastMessageAt = DateTime.UtcNow.AddHours(-4),
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new Chats
                        {
                            Id = chat4Id,
                            Name = "Announcements",
                            Type = ChatType.Channel,
                            ParticipantCount = 25,
                            LastMessage = "New feature release scheduled for next week",
                            LastMessageAt = DateTime.UtcNow.AddHours(-6),
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new Chats
                        {
                            Id = chat5Id,
                            Name = "Michael Chen",
                            Type = ChatType.Direct,
                            ParticipantCount = 2,
                            LastMessage = "See you at the conference",
                            LastMessageAt = DateTime.UtcNow.AddDays(-1),
                            CreatedAt = DateTime.UtcNow.AddDays(-60)
                        },
                        new Chats
                        {
                            Name = "Design Review",
                            Type = ChatType.Group,
                            ParticipantCount = 4,
                            LastMessage = "New mockups look great!",
                            LastMessageAt = DateTime.UtcNow.AddMinutes(-30),
                            CreatedAt = DateTime.UtcNow.AddDays(-5)
                        },
                        new Chats
                        {
                            Name = "Customer Support",
                            Type = ChatType.Channel,
                            ParticipantCount = 12,
                            LastMessage = "Ticket #4521 has been resolved",
                            LastMessageAt = DateTime.UtcNow.AddHours(-1),
                            CreatedAt = DateTime.UtcNow.AddDays(-90)
                        },
                        new Chats
                        {
                            Name = "Emily Rodriguez",
                            Type = ChatType.Direct,
                            ParticipantCount = 2,
                            LastMessage = "I'll send the files by end of day",
                            LastMessageAt = DateTime.UtcNow.AddMinutes(-45),
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        },
                        new Chats
                        {
                            Name = "Marketing Campaign",
                            Type = ChatType.Group,
                            ParticipantCount = 6,
                            LastMessage = "Campaign metrics are looking good",
                            LastMessageAt = DateTime.UtcNow.AddHours(-8),
                            CreatedAt = DateTime.UtcNow.AddDays(-12)
                        },
                        new Chats
                        {
                            Name = "General Discussion",
                            Type = ChatType.Channel,
                            ParticipantCount = 50,
                            LastMessage = "Welcome to the team!",
                            LastMessageAt = DateTime.UtcNow.AddMinutes(-90),
                            CreatedAt = DateTime.UtcNow.AddDays(-120)
                        }
                    };

                    context.Chats.AddRange(chats);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully seeded {chats.Count} chats");

                    // Create Chat Messages for the first few chats
                    var messages = new List<ChatMessages>
                    {
                        // Chat 1 - Project Team
                        new ChatMessages
                        {
                            ChatId = chat1Id,
                            SenderId = user1Id,
                            Content = "Hi everyone! How's the progress on the new feature?",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-3)
                        },
                        new ChatMessages
                        {
                            ChatId = chat1Id,
                            SenderId = user2Id,
                            Content = "Going well! We're about 70% complete.",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(-45)
                        },
                        new ChatMessages
                        {
                            ChatId = chat1Id,
                            SenderId = user3Id,
                            Content = "I've uploaded the design mockups",
                            MessageType = MessageType.File,
                            CreatedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(-30)
                        },
                        new ChatMessages
                        {
                            ChatId = chat1Id,
                            SenderId = user1Id,
                            Content = "Let's schedule a meeting for tomorrow",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddMinutes(-15)
                        },

                        // Chat 2 - Sarah Johnson
                        new ChatMessages
                        {
                            ChatId = chat2Id,
                            SenderId = user1Id,
                            Content = "Hey Sarah, can you review the latest document?",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-4)
                        },
                        new ChatMessages
                        {
                            ChatId = chat2Id,
                            SenderId = user2Id,
                            Content = "Sure! I'll take a look right now.",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-3).AddMinutes(-30)
                        },
                        new ChatMessages
                        {
                            ChatId = chat2Id,
                            SenderId = user2Id,
                            Content = "document-review.pdf",
                            MessageType = MessageType.File,
                            CreatedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(-15)
                        },
                        new ChatMessages
                        {
                            ChatId = chat2Id,
                            SenderId = user2Id,
                            Content = "Thanks for the update!",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-2)
                        },

                        // Chat 3 - Development Team
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user3Id,
                            Content = "Who can help me with the API integration?",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-6)
                        },
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user4Id,
                            Content = "I can help! What's the issue?",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-5).AddMinutes(-45)
                        },
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user3Id,
                            Content = "Getting a 401 error on authentication",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-5).AddMinutes(-30)
                        },
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user4Id,
                            Content = "Check if you're including the Bearer token",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-5).AddMinutes(-15)
                        },
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user3Id,
                            Content = "That fixed it! Thanks!",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-5)
                        },
                        new ChatMessages
                        {
                            ChatId = chat3Id,
                            SenderId = user5Id,
                            Content = "Code review completed",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-4)
                        },

                        // Chat 4 - Announcements
                        new ChatMessages
                        {
                            ChatId = chat4Id,
                            SenderId = user1Id,
                            Content = "Welcome to our announcements channel!",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-2)
                        },
                        new ChatMessages
                        {
                            ChatId = chat4Id,
                            SenderId = user1Id,
                            Content = "Please keep discussions professional",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-1).AddHours(-12)
                        },
                        new ChatMessages
                        {
                            ChatId = chat4Id,
                            SenderId = user2Id,
                            Content = "New feature release scheduled for next week",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddHours(-6)
                        },

                        // Chat 5 - Michael Chen
                        new ChatMessages
                        {
                            ChatId = chat5Id,
                            SenderId = user1Id,
                            Content = "Are you attending the tech conference?",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-2)
                        },
                        new ChatMessages
                        {
                            ChatId = chat5Id,
                            SenderId = user5Id,
                            Content = "Yes! I'll be presenting on day 2",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-1).AddHours(-18)
                        },
                        new ChatMessages
                        {
                            ChatId = chat5Id,
                            SenderId = user1Id,
                            Content = "Awesome! Looking forward to it",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-1).AddHours(-12)
                        },
                        new ChatMessages
                        {
                            ChatId = chat5Id,
                            SenderId = user5Id,
                            Content = "See you at the conference",
                            MessageType = MessageType.Text,
                            CreatedAt = DateTime.UtcNow.AddDays(-1)
                        }
                    };

                    context.ChatMessages.AddRange(messages);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully seeded {messages.Count} chat messages");
                }
                else
                {
                    logger.LogInformation("Mantine chats already seeded, skipping...");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error seeding Mantine chats");
                throw;
            }
        }
    }
}
