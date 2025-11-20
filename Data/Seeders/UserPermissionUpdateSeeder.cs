using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class UserPermissionUpdateSeeder
    {
        public static async Task UpdateUserPermissionsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();

            logger.LogInformation("Starting user permissions update process with new RBAC system...");

            // Update admin permissions
            var adminUser = await userManager.FindByEmailAsync("admin@adminhub.com");
            if (adminUser != null)
            {
                var adminPermissions = new List<Claim>
                {
                    // Admin permissions
                    new Claim(CustomClaimTypes.Permission, Permissions.Admin.UserManagement),
                    new Claim(CustomClaimTypes.Permission, Permissions.Admin.SystemSettings),

                    // Team permissions
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Projects),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Orders),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.KanbanTasks),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Analytics),

                    // User directory
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewDirectory),

                    // Personal permissions (admin can access all)
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Profile),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Invoices),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Files),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Chats),

                    // Antd Dashboard permissions
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Tasks),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Projects),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Clients),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Products),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Sellers),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Orders),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.CampaignAds),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.SocialMediaStats),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.SocialMediaActivities),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.ScheduledPosts),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.LiveAuctions),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.AuctionCreators),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.BiddingTopSellers),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.BiddingTransactions),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Courses),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.StudyStatistics),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.RecommendedCourses),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Exams),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.CommunityGroups),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.TruckDeliveries),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.DeliveryAnalytics),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Trucks),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.TruckDeliveryRequests),
                };

                await UpdateUserPermissions(userManager, adminUser, adminPermissions, logger);
            }

            // Update user permissions for other users
            var users = userManager.Users.Where(u => u.Email != "admin@adminhub.com").ToList();
            foreach (var user in users)
            {
                var userPermissions = new List<Claim>
                {
                    // Team permissions (collaborative access)
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Projects),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Orders),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.KanbanTasks),
                    new Claim(CustomClaimTypes.Permission, Permissions.Team.Analytics),

                    // User directory
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewDirectory),

                    // Personal permissions
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Profile),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Invoices),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Files),
                    new Claim(CustomClaimTypes.Permission, Permissions.Personal.Chats),

                    // Antd Dashboard permissions
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Projects),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Clients),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Products),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Sellers),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Orders),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.CampaignAds),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.SocialMediaStats),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.SocialMediaActivities),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.ScheduledPosts),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.LiveAuctions),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.AuctionCreators),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.BiddingTopSellers),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.BiddingTransactions),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Courses),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.StudyStatistics),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.RecommendedCourses),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Exams),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.CommunityGroups),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.TruckDeliveries),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.DeliveryAnalytics),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.Trucks),
                    new Claim(CustomClaimTypes.Permission, Permissions.Antd.TruckDeliveryRequests),
                };

                await UpdateUserPermissions(userManager, user, userPermissions, logger);
            }

            logger.LogInformation("User permissions update process completed successfully");
        }

        private static async Task UpdateUserPermissions(UserManager<ApplicationUser> userManager, ApplicationUser user, List<Claim> newPermissions, ILogger logger)
        {
            try
            {
                // Remove all existing permission claims
                var existingClaims = await userManager.GetClaimsAsync(user);
                var permissionClaims = existingClaims.Where(c => c.Type == CustomClaimTypes.Permission).ToList();

                if (permissionClaims.Any())
                {
                    await userManager.RemoveClaimsAsync(user, permissionClaims);
                }

                // Add new permissions
                await userManager.AddClaimsAsync(user, newPermissions);

                logger.LogInformation($"Updated permissions for user: {user.Email}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating permissions for user: {user.Email}");
            }
        }
    }
}