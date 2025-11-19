using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdLiveAuctionDto
    {
        [JsonPropertyName("auction_id")]
        public string AuctionId { get; set; } = string.Empty;

        [JsonPropertyName("nft_name")]
        public string NftName { get; set; } = string.Empty;

        [JsonPropertyName("nft_image")]
        public string NftImage { get; set; } = string.Empty;

        [JsonPropertyName("seller_username")]
        public string SellerUsername { get; set; } = string.Empty;

        [JsonPropertyName("buyer_username")]
        public string BuyerUsername { get; set; } = string.Empty;

        [JsonPropertyName("start_price")]
        public decimal StartPrice { get; set; }

        [JsonPropertyName("end_price")]
        public decimal EndPrice { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("is_highest_bid_mine")]
        public bool IsHighestBidMine { get; set; }

        [JsonPropertyName("winning_bid")]
        public decimal WinningBid { get; set; }

        [JsonPropertyName("time_left")]
        public string TimeLeft { get; set; } = string.Empty;
    }

    public class AntdLiveAuctionQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Status { get; set; }
        public string? SellerUsername { get; set; }
        public string? BuyerUsername { get; set; }
        public decimal? MinStartPrice { get; set; }
        public decimal? MaxStartPrice { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public string SortBy { get; set; } = "enddate";
        public string SortOrder { get; set; } = "asc";
    }

    public class AntdLiveAuctionResponse : ApiResponse<AntdLiveAuctionDto> { }
    public class AntdLiveAuctionListResponse : ApiResponse<List<AntdLiveAuctionDto>> { }
    public class AntdLiveAuctionCreateResponse : ApiResponse<AntdLiveAuctionDto> { }
    public class AntdLiveAuctionUpdateResponse : ApiResponse<AntdLiveAuctionDto> { }
    public class AntdLiveAuctionDeleteResponse : ApiResponse<object> { }
}
