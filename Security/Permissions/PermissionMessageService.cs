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
                Resource = "users",
                Prefix = "/api/v1/users",
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
                Resource = "auth",
                Prefix = "/api/v1/auth",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to access authentication.",
                        Post = "You do not have sufficient permissions to perform authentication.",
                        Put = "You do not have sufficient permissions to modify authentication.",
                        Delete = "You do not have sufficient permissions to perform this authentication action.",
                        Default = "You do not have sufficient permissions to perform this authentication action."
                    }
                }
            },
            new()
            {
                Resource = "profile",
                Prefix = "/api/v1/profile",
                Permissions = new Dictionary<string, HttpMethodPermissions>
                {
                    ["*"] = new HttpMethodPermissions
                    {
                        Get = "You do not have sufficient permissions to view profile information.",
                        Post = "You do not have sufficient permissions to modify profile.",
                        Put = "You do not have sufficient permissions to update profile.",
                        Delete = "You do not have sufficient permissions to delete profile data.",
                        Default = "You do not have sufficient permissions to perform this profile action."
                    }
                }
            }
            // Add more resources as needed for new endpoints
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