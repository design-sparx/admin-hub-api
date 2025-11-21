using System.Reflection;

namespace AdminHubApi.Constants;

public static class Permissions
{
    // Admin-only permissions (User Management)
    public static class Admin
    {
        public const string UserManagement = "Permissions.Admin.UserManagement";
        public const string SystemSettings = "Permissions.Admin.SystemSettings";
    }

    // Shared team resources (all users can CRUD)
    public static class Team
    {
        public const string Projects = "Permissions.Team.Projects";
        public const string Orders = "Permissions.Team.Orders";
        public const string KanbanTasks = "Permissions.Team.KanbanTasks";
        public const string Analytics = "Permissions.Team.Analytics";
    }

    // User directory (basic user info visibility)
    public static class Users
    {
        public const string ViewDirectory = "Permissions.Users.ViewDirectory";
    }

    // Personal resources (users access only their own)
    public static class Personal
    {
        public const string Profile = "Permissions.Personal.Profile";
        public const string Invoices = "Permissions.Personal.Invoices";
        public const string Files = "Permissions.Personal.Files";
        public const string Chats = "Permissions.Personal.Chats";
    }

    // Antd Dashboard permissions
    public static class Antd
    {
        public const string Tasks = "Permissions.Antd.Tasks";
        public const string Projects = "Permissions.Antd.Projects";
        public const string Clients = "Permissions.Antd.Clients";
        public const string Products = "Permissions.Antd.Products";
        public const string Sellers = "Permissions.Antd.Sellers";
        public const string Orders = "Permissions.Antd.Orders";
        public const string CampaignAds = "Permissions.Antd.CampaignAds";
        public const string SocialMediaStats = "Permissions.Antd.SocialMediaStats";
        public const string SocialMediaActivities = "Permissions.Antd.SocialMediaActivities";
        public const string ScheduledPosts = "Permissions.Antd.ScheduledPosts";
        public const string LiveAuctions = "Permissions.Antd.LiveAuctions";
        public const string AuctionCreators = "Permissions.Antd.AuctionCreators";
        public const string BiddingTopSellers = "Permissions.Antd.BiddingTopSellers";
        public const string BiddingTransactions = "Permissions.Antd.BiddingTransactions";
        public const string Courses = "Permissions.Antd.Courses";
        public const string StudyStatistics = "Permissions.Antd.StudyStatistics";
        public const string RecommendedCourses = "Permissions.Antd.RecommendedCourses";
        public const string Exams = "Permissions.Antd.Exams";
        public const string CommunityGroups = "Permissions.Antd.CommunityGroups";
        public const string TruckDeliveries = "Permissions.Antd.TruckDeliveries";
        public const string DeliveryAnalytics = "Permissions.Antd.DeliveryAnalytics";
        public const string Trucks = "Permissions.Antd.Trucks";
        public const string TruckDeliveryRequests = "Permissions.Antd.TruckDeliveryRequests";
        public const string Employees = "Permissions.Antd.Employees";
        public const string Faqs = "Permissions.Antd.Faqs";
        public const string Pricings = "Permissions.Antd.Pricings";
    }


    // Helper method to get all permissions
    public static IEnumerable<string> GetAllPermissions()
    {
        return typeof(Permissions)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(fi => (string)fi.GetValue(null))
            .ToList();
    }
}