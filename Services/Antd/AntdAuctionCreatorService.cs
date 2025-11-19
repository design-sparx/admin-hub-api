using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdAuctionCreatorService : IAntdAuctionCreatorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdAuctionCreatorService> _logger;

        public AntdAuctionCreatorService(ApplicationDbContext context, ILogger<AntdAuctionCreatorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdAuctionCreatorListResponse> GetAllAsync(AntdAuctionCreatorQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdAuctionCreators.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Country))
                    query = query.Where(c => c.Country.ToLower() == queryParams.Country.ToLower());

                if (!string.IsNullOrEmpty(queryParams.FavoriteColor))
                    query = query.Where(c => c.FavoriteColor.ToLower() == queryParams.FavoriteColor.ToLower());

                if (queryParams.MinAge.HasValue)
                    query = query.Where(c => c.Age >= queryParams.MinAge.Value);

                if (queryParams.MaxAge.HasValue)
                    query = query.Where(c => c.Age <= queryParams.MaxAge.Value);

                if (queryParams.MinSalesCount.HasValue)
                    query = query.Where(c => c.SalesCount >= queryParams.MinSalesCount.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "salescount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.SalesCount) : query.OrderBy(c => c.SalesCount),
                    "age" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Age) : query.OrderBy(c => c.Age),
                    "firstname" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.FirstName) : query.OrderBy(c => c.FirstName),
                    "lastname" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.LastName) : query.OrderBy(c => c.LastName),
                    "country" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Country) : query.OrderBy(c => c.Country),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.SalesCount) : query.OrderBy(c => c.SalesCount)
                };

                var total = await query.CountAsync();
                var creators = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdAuctionCreatorListResponse
                {
                    Succeeded = true,
                    Data = creators.Select(MapToDto).ToList(),
                    Message = "Auction creators retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd auction creators");
                throw;
            }
        }

        public async Task<AntdAuctionCreatorResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdAuctionCreatorResponse { Succeeded = false, Message = "Invalid creator ID format" };

                var creator = await _context.AntdAuctionCreators.FindAsync(guidId);
                if (creator == null)
                    return new AntdAuctionCreatorResponse { Succeeded = false, Message = "Creator not found" };

                return new AntdAuctionCreatorResponse { Succeeded = true, Data = MapToDto(creator), Message = "Creator retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd auction creator {CreatorId}", id);
                throw;
            }
        }

        public async Task<AntdAuctionCreatorCreateResponse> CreateAsync(AntdAuctionCreatorDto dto)
        {
            try
            {
                var creator = new AntdAuctionCreator
                {
                    Id = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Email = dto.Email,
                    Country = dto.Country,
                    PostalCode = dto.PostalCode,
                    FavoriteColor = dto.FavoriteColor,
                    SalesCount = dto.SalesCount,
                    TotalSales = dto.TotalSales,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdAuctionCreators.Add(creator);
                await _context.SaveChangesAsync();

                return new AntdAuctionCreatorCreateResponse { Succeeded = true, Data = MapToDto(creator), Message = "Creator created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd auction creator");
                throw;
            }
        }

        public async Task<AntdAuctionCreatorUpdateResponse> UpdateAsync(string id, AntdAuctionCreatorDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdAuctionCreatorUpdateResponse { Succeeded = false, Message = "Invalid creator ID format" };

                var creator = await _context.AntdAuctionCreators.FindAsync(guidId);
                if (creator == null)
                    return new AntdAuctionCreatorUpdateResponse { Succeeded = false, Message = "Creator not found" };

                creator.FirstName = dto.FirstName;
                creator.LastName = dto.LastName;
                creator.Age = dto.Age;
                creator.Email = dto.Email;
                creator.Country = dto.Country;
                creator.PostalCode = dto.PostalCode;
                creator.FavoriteColor = dto.FavoriteColor;
                creator.SalesCount = dto.SalesCount;
                creator.TotalSales = dto.TotalSales;
                creator.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdAuctionCreatorUpdateResponse { Succeeded = true, Data = MapToDto(creator), Message = "Creator updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd auction creator {CreatorId}", id);
                throw;
            }
        }

        public async Task<AntdAuctionCreatorDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdAuctionCreatorDeleteResponse { Succeeded = false, Message = "Invalid creator ID format" };

                var creator = await _context.AntdAuctionCreators.FindAsync(guidId);
                if (creator == null)
                    return new AntdAuctionCreatorDeleteResponse { Succeeded = false, Message = "Creator not found" };

                _context.AntdAuctionCreators.Remove(creator);
                await _context.SaveChangesAsync();

                return new AntdAuctionCreatorDeleteResponse { Succeeded = true, Message = "Creator deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd auction creator {CreatorId}", id);
                throw;
            }
        }

        private static AntdAuctionCreatorDto MapToDto(AntdAuctionCreator creator)
        {
            return new AntdAuctionCreatorDto
            {
                CreatorId = creator.Id.ToString(),
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Age = creator.Age,
                Email = creator.Email,
                Country = creator.Country,
                PostalCode = creator.PostalCode ?? "",
                FavoriteColor = creator.FavoriteColor,
                SalesCount = creator.SalesCount,
                TotalSales = creator.TotalSales
            };
        }
    }
}
