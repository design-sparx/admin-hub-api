namespace AdminHubApi.Security.Permissions;

public class ResourcePermissionMessages
{
    public string Resource { get; set; }
    public string Prefix { get; set; }
    public Dictionary<string, HttpMethodPermissions> Permissions { get; set; }
}

public class HttpMethodPermissions
{
    public string Get { get; set; }
    public string Post { get; set; }
    public string Put { get; set; }
    public string Delete { get; set; }
    public string Default { get; set; }
}