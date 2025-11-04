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