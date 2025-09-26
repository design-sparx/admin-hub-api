namespace AdminHubApi.Dtos.ApiResponse;

public class ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public PaginationMeta Meta { get; set; }
}