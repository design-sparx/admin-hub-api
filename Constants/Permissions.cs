using System.Reflection;

namespace AdminHubApi.Constants;

public class Permissions
{
    // User management permissions
    public const string ViewUsers = "Permissions.Users.View";
    public const string CreateUsers = "Permissions.Users.Create";
    public const string EditUsers = "Permissions.Users.Edit";
    public const string DeleteUsers = "Permissions.Users.Delete";

    // Example: Content management permissions
    public const string ViewContent = "Permissions.Content.View";
    public const string CreateContent = "Permissions.Content.Create";
    public const string EditContent = "Permissions.Content.Edit";
    public const string DeleteContent = "Permissions.Content.Delete";

    // Add more permissions as needed for your application

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