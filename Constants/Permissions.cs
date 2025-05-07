using System.Reflection;

namespace AdminHubApi.Constants;

public static class Permissions
{
    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }

    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
    }
    
    public static class Projects
    {
        public const string View = "Permissions.Projects.View";
        public const string Create = "Permissions.Projects.Create";
        public const string Edit = "Permissions.Projects.Edit";
        public const string Delete = "Permissions.Projects.Delete";
    }
    
    public static class Products
    {
        public const string View = "Permissions.Products.View";
        public const string Create = "Permissions.Products.Create";
        public const string Edit = "Permissions.Products.Edit";
        public const string Delete = "Permissions.Products.Delete";
    }
    
    public static class ProductCategories
    {
        public const string View = "Permissions.ProductCategories.View";
        public const string Create = "Permissions.ProductCategories.Create";
        public const string Edit = "Permissions.ProductCategories.Edit";
        public const string Delete = "Permissions.ProductCategories.Delete";
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