using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdLiveAuctionService : IAntdLiveAuctionService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdLiveAuctionService> _logger;

        public AntdLiveAuctionService(ApplicationDbContext context, ILogger<AntdLiveAuctionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdLiveAuctionListResponse> GetAllAsync(AntdLiveAuctionQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdLiveAuctions.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Status))
                    query = query.Where(a => a.Status.ToLower() == queryParams.Status.ToLower());

                if (!string.IsNullOrEmpty(queryParams.SellerUsername))
                    query = query.Where(a => a.SellerUsername.Contains(queryParams.SellerUsername));

                if (!string.IsNullOrEmpty(queryParams.BuyerUsername))
                    query = query.Where(a => a.BuyerUsername.Contains(queryParams.BuyerUsername));

                if (queryParams.MinStartPrice.HasValue)
                    query = query.Where(a => a.StartPrice >= queryParams.MinStartPrice.Value);

                if (queryParams.MaxStartPrice.HasValue)
                    query = query.Where(a => a.StartPrice <= queryParams.MaxStartPrice.Value);

                if (queryParams.StartDateFrom.HasValue)
                    query = query.Where(a => a.StartDate >= queryParams.StartDateFrom.Value);

                if (queryParams.StartDateTo.HasValue)
                    query = query.Where(a => a.StartDate <= queryParams.StartDateTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "enddate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.EndDate) : query.OrderBy(a => a.EndDate),
                    "startdate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.StartDate) : query.OrderBy(a => a.StartDate),
                    "startprice" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.StartPrice) : query.OrderBy(a => a.StartPrice),
                    "endprice" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.EndPrice) : query.OrderBy(a => a.EndPrice),
                    "winningbid" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.WinningBid) : query.OrderBy(a => a.WinningBid),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.EndDate) : query.OrderBy(a => a.EndDate)
                };

                var total = await query.CountAsync();
                var auctions = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdLiveAuctionListResponse
                {
                    Succeeded = true,
                    Data = auctions.Select(MapToDto).ToList(),
                    Message = "Live auctions retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd live auctions");
                throw;
            }
        }

        public async Task<AntdLiveAuctionResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLiveAuctionResponse { Succeeded = false, Message = "Invalid auction ID format" };

                var auction = await _context.AntdLiveAuctions.FindAsync(guidId);
                if (auction == null)
                    return new AntdLiveAuctionResponse { Succeeded = false, Message = "Auction not found" };

                return new AntdLiveAuctionResponse { Succeeded = true, Data = MapToDto(auction), Message = "Auction retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd live auction {AuctionId}", id);
                throw;
            }
        }

        public async Task<AntdLiveAuctionCreateResponse> CreateAsync(AntdLiveAuctionDto dto)
        {
            try
            {
                var auction = new AntdLiveAuction
                {
                    Id = Guid.NewGuid(),
                    NftName = dto.NftName,
                    NftImage = dto.NftImage,
                    SellerUsername = dto.SellerUsername,
                    BuyerUsername = dto.BuyerUsername,
                    StartPrice = dto.StartPrice,
                    EndPrice = dto.EndPrice,
                    StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime(),
                    EndDate = DateTime.Parse(dto.EndDate).ToUniversalTime(),
                    Status = dto.Status,
                    IsHighestBidMine = dto.IsHighestBidMine,
                    WinningBid = dto.WinningBid,
                    TimeLeft = dto.TimeLeft,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdLiveAuctions.Add(auction);
                await _context.SaveChangesAsync();

                return new AntdLiveAuctionCreateResponse { Succeeded = true, Data = MapToDto(auction), Message = "Auction created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd live auction");
                throw;
            }
        }

        public async Task<AntdLiveAuctionUpdateResponse> UpdateAsync(string id, AntdLiveAuctionDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLiveAuctionUpdateResponse { Succeeded = false, Message = "Invalid auction ID format" };

                var auction = await _context.AntdLiveAuctions.FindAsync(guidId);
                if (auction == null)
                    return new AntdLiveAuctionUpdateResponse { Succeeded = false, Message = "Auction not found" };

                auction.NftName = dto.NftName;
                auction.NftImage = dto.NftImage;
                auction.SellerUsername = dto.SellerUsername;
                auction.BuyerUsername = dto.BuyerUsername;
                auction.StartPrice = dto.StartPrice;
                auction.EndPrice = dto.EndPrice;
                auction.StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime();
                auction.EndDate = DateTime.Parse(dto.EndDate).ToUniversalTime();
                auction.Status = dto.Status;
                auction.IsHighestBidMine = dto.IsHighestBidMine;
                auction.WinningBid = dto.WinningBid;
                auction.TimeLeft = dto.TimeLeft;
                auction.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdLiveAuctionUpdateResponse { Succeeded = true, Data = MapToDto(auction), Message = "Auction updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd live auction {AuctionId}", id);
                throw;
            }
        }

        public async Task<AntdLiveAuctionDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLiveAuctionDeleteResponse { Succeeded = false, Message = "Invalid auction ID format" };

                var auction = await _context.AntdLiveAuctions.FindAsync(guidId);
                if (auction == null)
                    return new AntdLiveAuctionDeleteResponse { Succeeded = false, Message = "Auction not found" };

                _context.AntdLiveAuctions.Remove(auction);
                await _context.SaveChangesAsync();

                return new AntdLiveAuctionDeleteResponse { Succeeded = true, Message = "Auction deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd live auction {AuctionId}", id);
                throw;
            }
        }

        private static AntdLiveAuctionDto MapToDto(AntdLiveAuction auction)
        {
            return new AntdLiveAuctionDto
            {
                AuctionId = auction.Id.ToString(),
                NftName = auction.NftName,
                NftImage = auction.NftImage,
                SellerUsername = auction.SellerUsername,
                BuyerUsername = auction.BuyerUsername,
                StartPrice = auction.StartPrice,
                EndPrice = auction.EndPrice,
                StartDate = auction.StartDate.ToString("M/d/yyyy"),
                EndDate = auction.EndDate.ToString("M/d/yyyy"),
                Status = auction.Status,
                IsHighestBidMine = auction.IsHighestBidMine,
                WinningBid = auction.WinningBid,
                TimeLeft = auction.TimeLeft
            };
        }
    }
}
