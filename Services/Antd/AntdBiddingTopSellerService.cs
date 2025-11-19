using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdBiddingTopSellerService : IAntdBiddingTopSellerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdBiddingTopSellerService> _logger;

        public AntdBiddingTopSellerService(ApplicationDbContext context, ILogger<AntdBiddingTopSellerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdBiddingTopSellerListResponse> GetAllAsync(AntdBiddingTopSellerQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdBiddingTopSellers.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Collection))
                    query = query.Where(s => s.Collection.ToLower() == queryParams.Collection.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Artist))
                    query = query.Where(s => s.Artist.Contains(queryParams.Artist));

                if (queryParams.Verified.HasValue)
                    query = query.Where(s => s.Verified == queryParams.Verified.Value);

                if (queryParams.MinPrice.HasValue)
                    query = query.Where(s => s.Price >= queryParams.MinPrice.Value);

                if (queryParams.MaxPrice.HasValue)
                    query = query.Where(s => s.Price <= queryParams.MaxPrice.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "volume" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Volume) : query.OrderBy(s => s.Volume),
                    "price" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
                    "ownerscount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.OwnersCount) : query.OrderBy(s => s.OwnersCount),
                    "creationdate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.CreationDate) : query.OrderBy(s => s.CreationDate),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Volume) : query.OrderBy(s => s.Volume)
                };

                var total = await query.CountAsync();
                var sellers = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdBiddingTopSellerListResponse
                {
                    Succeeded = true,
                    Data = sellers.Select(MapToDto).ToList(),
                    Message = "Bidding top sellers retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd bidding top sellers");
                throw;
            }
        }

        public async Task<AntdBiddingTopSellerResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTopSellerResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdBiddingTopSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdBiddingTopSellerResponse { Succeeded = false, Message = "Seller not found" };

                return new AntdBiddingTopSellerResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd bidding top seller {SellerId}", id);
                throw;
            }
        }

        public async Task<AntdBiddingTopSellerCreateResponse> CreateAsync(AntdBiddingTopSellerDto dto)
        {
            try
            {
                var seller = new AntdBiddingTopSeller
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Artist = dto.Artist,
                    Volume = dto.Volume,
                    Status = dto.Status,
                    OwnersCount = dto.OwnersCount,
                    Description = dto.Description,
                    ImageUrl = dto.ImageUrl,
                    CreationDate = DateTime.Parse(dto.CreationDate).ToUniversalTime(),
                    Edition = dto.Edition,
                    Price = dto.Price,
                    Owner = dto.Owner,
                    Collection = dto.Collection,
                    Verified = dto.Verified,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdBiddingTopSellers.Add(seller);
                await _context.SaveChangesAsync();

                return new AntdBiddingTopSellerCreateResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd bidding top seller");
                throw;
            }
        }

        public async Task<AntdBiddingTopSellerUpdateResponse> UpdateAsync(string id, AntdBiddingTopSellerDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTopSellerUpdateResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdBiddingTopSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdBiddingTopSellerUpdateResponse { Succeeded = false, Message = "Seller not found" };

                seller.Title = dto.Title;
                seller.Artist = dto.Artist;
                seller.Volume = dto.Volume;
                seller.Status = dto.Status;
                seller.OwnersCount = dto.OwnersCount;
                seller.Description = dto.Description;
                seller.ImageUrl = dto.ImageUrl;
                seller.CreationDate = DateTime.Parse(dto.CreationDate).ToUniversalTime();
                seller.Edition = dto.Edition;
                seller.Price = dto.Price;
                seller.Owner = dto.Owner;
                seller.Collection = dto.Collection;
                seller.Verified = dto.Verified;
                seller.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdBiddingTopSellerUpdateResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd bidding top seller {SellerId}", id);
                throw;
            }
        }

        public async Task<AntdBiddingTopSellerDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTopSellerDeleteResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdBiddingTopSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdBiddingTopSellerDeleteResponse { Succeeded = false, Message = "Seller not found" };

                _context.AntdBiddingTopSellers.Remove(seller);
                await _context.SaveChangesAsync();

                return new AntdBiddingTopSellerDeleteResponse { Succeeded = true, Message = "Seller deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd bidding top seller {SellerId}", id);
                throw;
            }
        }

        private static AntdBiddingTopSellerDto MapToDto(AntdBiddingTopSeller seller)
        {
            return new AntdBiddingTopSellerDto
            {
                Id = seller.Id.ToString(),
                Title = seller.Title,
                Artist = seller.Artist,
                Volume = seller.Volume,
                Status = seller.Status,
                OwnersCount = seller.OwnersCount,
                Description = seller.Description,
                ImageUrl = seller.ImageUrl,
                CreationDate = seller.CreationDate.ToString("M/d/yyyy"),
                Edition = seller.Edition,
                Price = seller.Price,
                Owner = seller.Owner,
                Collection = seller.Collection,
                Verified = seller.Verified
            };
        }
    }
}
