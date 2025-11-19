using System.Text.Json.Serialization;
using AdminHubApi.Dtos.Common;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdCommunityGroupDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("leader")]
        public string Leader { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("meeting_time")]
        public string MeetingTime { get; set; } = string.Empty;

        [JsonPropertyName("member_age_range")]
        public int MemberAgeRange { get; set; }

        [JsonPropertyName("member_interests")]
        public string MemberInterests { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;
    }

    public class AntdCommunityGroupResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("leader")]
        public string Leader { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("meeting_time")]
        public string MeetingTime { get; set; } = string.Empty;

        [JsonPropertyName("member_age_range")]
        public int MemberAgeRange { get; set; }

        [JsonPropertyName("member_interests")]
        public string MemberInterests { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class AntdCommunityGroupQueryParams
    {
        public string? Category { get; set; }
        public string? Location { get; set; }
        public string? Leader { get; set; }
        public int? MinSize { get; set; }
        public int? MaxSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdCommunityGroupListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdCommunityGroupResponseDto> Data { get; set; } = new();

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; } = new();
    }

    public class AntdCommunityGroupResponse
    {
        [JsonPropertyName("data")]
        public AntdCommunityGroupResponseDto Data { get; set; } = new();
    }

    public class AntdCommunityGroupCreateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdCommunityGroupResponseDto Data { get; set; } = new();
    }

    public class AntdCommunityGroupUpdateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdCommunityGroupResponseDto Data { get; set; } = new();
    }

    public class AntdCommunityGroupDeleteResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
