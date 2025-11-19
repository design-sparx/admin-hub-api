using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdSellerService : IAntdSellerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdSellerService> _logger;

        public AntdSellerService(ApplicationDbContext context, ILogger<AntdSellerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdSellerListResponse> GetAllAsync(AntdSellerQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdSellers.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.SalesRegion))
                    query = query.Where(s => s.SalesRegion.ToLower() == queryParams.SalesRegion.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Country))
                    query = query.Where(s => s.Country.ToLower() == queryParams.Country.ToLower());

                if (queryParams.MinSalesVolume.HasValue)
                    query = query.Where(s => s.SalesVolume >= queryParams.MinSalesVolume.Value);

                if (queryParams.MinSatisfaction.HasValue)
                    query = query.Where(s => s.CustomerSatisfaction >= queryParams.MinSatisfaction.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "salesvolume" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.SalesVolume) : query.OrderBy(s => s.SalesVolume),
                    "customersatisfaction" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.CustomerSatisfaction) : query.OrderBy(s => s.CustomerSatisfaction),
                    "firstname" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.FirstName) : query.OrderBy(s => s.FirstName),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.TotalSales) : query.OrderBy(s => s.TotalSales)
                };

                var total = await query.CountAsync();
                var sellers = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdSellerListResponse
                {
                    Succeeded = true,
                    Data = sellers.Select(MapToDto).ToList(),
                    Message = "Sellers retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd sellers");
                throw;
            }
        }

        public async Task<AntdSellerResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSellerResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdSellerResponse { Succeeded = false, Message = "Seller not found" };

                return new AntdSellerResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd seller {SellerId}", id);
                throw;
            }
        }

        public async Task<AntdSellerListResponse> GetTopSellersAsync(int limit)
        {
            try
            {
                var sellers = await _context.AntdSellers.OrderByDescending(s => s.TotalSales).Take(limit).ToListAsync();
                return new AntdSellerListResponse { Succeeded = true, Data = sellers.Select(MapToDto).ToList(), Message = "Top sellers retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top Antd sellers");
                throw;
            }
        }

        public async Task<AntdSellerCreateResponse> CreateAsync(AntdSellerDto sellerDto)
        {
            try
            {
                var seller = new AntdSeller
                {
                    Id = Guid.NewGuid(),
                    FirstName = sellerDto.FirstName,
                    LastName = sellerDto.LastName,
                    Age = sellerDto.Age,
                    Email = sellerDto.Email,
                    Country = sellerDto.Country,
                    PostalCode = sellerDto.PostalCode,
                    FavoriteColor = sellerDto.FavoriteColor,
                    SalesVolume = sellerDto.SalesVolume,
                    TotalSales = sellerDto.TotalSales,
                    CustomerSatisfaction = sellerDto.CustomerSatisfaction,
                    SalesRegion = sellerDto.SalesRegion,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdSellers.Add(seller);
                await _context.SaveChangesAsync();

                return new AntdSellerCreateResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd seller");
                throw;
            }
        }

        public async Task<AntdSellerUpdateResponse> UpdateAsync(string id, AntdSellerDto sellerDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSellerUpdateResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdSellerUpdateResponse { Succeeded = false, Message = "Seller not found" };

                seller.FirstName = sellerDto.FirstName;
                seller.LastName = sellerDto.LastName;
                seller.Age = sellerDto.Age;
                seller.Email = sellerDto.Email;
                seller.Country = sellerDto.Country;
                seller.PostalCode = sellerDto.PostalCode;
                seller.FavoriteColor = sellerDto.FavoriteColor;
                seller.SalesVolume = sellerDto.SalesVolume;
                seller.TotalSales = sellerDto.TotalSales;
                seller.CustomerSatisfaction = sellerDto.CustomerSatisfaction;
                seller.SalesRegion = sellerDto.SalesRegion;
                seller.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdSellerUpdateResponse { Succeeded = true, Data = MapToDto(seller), Message = "Seller updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd seller {SellerId}", id);
                throw;
            }
        }

        public async Task<AntdSellerDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSellerDeleteResponse { Succeeded = false, Message = "Invalid seller ID format" };

                var seller = await _context.AntdSellers.FindAsync(guidId);
                if (seller == null)
                    return new AntdSellerDeleteResponse { Succeeded = false, Message = "Seller not found" };

                _context.AntdSellers.Remove(seller);
                await _context.SaveChangesAsync();

                return new AntdSellerDeleteResponse { Succeeded = true, Message = "Seller deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd seller {SellerId}", id);
                throw;
            }
        }

        private static AntdSellerDto MapToDto(AntdSeller seller)
        {
            return new AntdSellerDto
            {
                Id = seller.Id.ToString(),
                FirstName = seller.FirstName,
                LastName = seller.LastName,
                Age = seller.Age,
                Email = seller.Email,
                Country = seller.Country,
                PostalCode = seller.PostalCode,
                FavoriteColor = seller.FavoriteColor,
                SalesVolume = seller.SalesVolume,
                TotalSales = seller.TotalSales,
                CustomerSatisfaction = seller.CustomerSatisfaction,
                SalesRegion = seller.SalesRegion
            };
        }
    }
}
