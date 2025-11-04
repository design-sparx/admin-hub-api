namespace AdminHubApi.Dtos.Mantine
{
    public class StatsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public int Diff { get; set; }
        public string? Period { get; set; }
    }

    public class StatsGridResponse
    {
        public List<StatsDto> Data { get; set; } = new();
    }
}