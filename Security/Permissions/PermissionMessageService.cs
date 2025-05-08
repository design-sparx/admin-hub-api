namespace AdminHubApi.Security.Permissions;

public interface IPermissionMessageService
{
    string GetPermissionMessage(string path, string httpMethod);
}

public class PermissionMessageService : IPermissionMessageService
{
    private readonly List<ResourcePermissionMessages> _resourcePermissions;

    public PermissionMessageService()
    {
        _resourcePermissions = new List<ResourcePermissionMessages>
        {
            new()
            {
                Resource = "projects",
                Prefix = "/api/projects",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view projects.",
                        Post = "You do not have sufficient permissions to create a project.",
                        Put = "You do not have sufficient permissions to edit a project.",
                        Delete = "You do not have sufficient permissions to delete a project.",
                        Default = "You do not have sufficient permissions to perform this action on projects."
                    }
                }
            },
            new()
            {
                Resource = "products",
                Prefix = "/api/products",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view products.",
                        Post = "You do not have sufficient permissions to create a product.",
                        Put = "You do not have sufficient permissions to edit a product.",
                        Delete = "You do not have sufficient permissions to delete a product.",
                        Default = "You do not have sufficient permissions to perform this action on products."
                    }
                }
            },
            new()
            {
                Resource = "product-categories",
                Prefix = "/api/product-categories",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view product categories.",
                        Post = "You do not have sufficient permissions to create a product category.",
                        Put = "You do not have sufficient permissions to edit a product category.",
                        Delete = "You do not have sufficient permissions to delete a product category.",
                        Default = "You do not have sufficient permissions to perform this action on product categories."
                    }
                }
            },
            new()
            {
                Resource = "users",
                Prefix = "/api/users",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view user information.",
                        Post = "You do not have sufficient permissions to create a user.",
                        Put = "You do not have sufficient permissions to edit user information.",
                        Delete = "You do not have sufficient permissions to delete a user.",
                        Default = "You do not have sufficient permissions to perform this action on users."
                    }
                }
            },
            new()
            {
                Resource = "orders",
                Prefix = "/api/orders",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view orders.",
                        Post = "You do not have sufficient permissions to create an order.",
                        Put = "You do not have sufficient permissions to modify an order.",
                        Delete = "You do not have sufficient permissions to cancel an order.",
                        Default = "You do not have sufficient permissions to perform this action on orders."
                    }
                }
            }
            // Add more resources as needed
        };
    }

    public string GetPermissionMessage(string path, string httpMethod)
    {
        // Find the matching resource configuration
        var resourceConfig =
            _resourcePermissions.FirstOrDefault(r => path.StartsWith(r.Prefix, StringComparison.OrdinalIgnoreCase));

        if (resourceConfig == null)
        {
            return "You do not have sufficient permissions to perform this action.";
        }

        // Get the specific action configuration (default to "*" if not found)
        var actionKey = "*"; // We can later extend this to match specific actions like "/api/projects/{id}"

        if (!resourceConfig.Permissions.TryGetValue(actionKey, out var permissions))
        {
            return "You do not have sufficient permissions to perform this action.";
        }

        // Return the appropriate message based on HTTP method
        return httpMethod.ToUpper() switch
        {
            "GET" => permissions.Get ?? permissions.Default,
            "POST" => permissions.Post ?? permissions.Default,
            "PUT" => permissions.Put ?? permissions.Default,
            "DELETE" => permissions.Delete ?? permissions.Default,
            _ => permissions.Default ?? "You do not have sufficient permissions to perform this action."
        };
    }
}