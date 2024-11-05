namespace MantineAdmin.Helpers;

public class QueryObject
{
    public string? Name { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
}