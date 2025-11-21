using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdPricingService : IAntdPricingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdPricingService> _logger;

        public AntdPricingService(ApplicationDbContext context, ILogger<AntdPricingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdPricingListResponse> GetAllAsync(AntdPricingQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdPricings.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Plan))
                    query = query.Where(p => p.Plan.ToLower() == queryParams.Plan.ToLower());

                if (queryParams.Preferred.HasValue)
                    query = query.Where(p => p.Preferred == queryParams.Preferred.Value);

                if (queryParams.MaxMonthly.HasValue)
                    query = query.Where(p => p.Monthly <= queryParams.MaxMonthly.Value);

                if (queryParams.MaxAnnually.HasValue)
                    query = query.Where(p => p.Annually <= queryParams.MaxAnnually.Value);

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "plan" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Plan)
                        : query.OrderBy(p => p.Plan),
                    "annually" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Annually)
                        : query.OrderBy(p => p.Annually),
                    "preferred" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Preferred)
                        : query.OrderBy(p => p.Preferred),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Monthly)
                        : query.OrderBy(p => p.Monthly)
                };

                var total = await query.CountAsync();
                var pricings = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                return new AntdPricingListResponse
                {
                    Succeeded = true,
                    Data = pricings.Select(MapToDto).ToList(),
                    Message = "Pricing plans retrieved successfully",
                    Meta = new PaginationMeta
                    {
                        Page = queryParams.Page,
                        Limit = queryParams.Limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plans");
                throw;
            }
        }

        public async Task<AntdPricingResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdPricingResponse { Succeeded = false, Message = "Invalid pricing ID format" };

                var pricing = await _context.AntdPricings.FindAsync(guidId);
                if (pricing == null)
                    return new AntdPricingResponse { Succeeded = false, Message = "Pricing plan not found" };

                return new AntdPricingResponse
                {
                    Succeeded = true,
                    Data = MapToDto(pricing),
                    Message = "Pricing plan retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plan {PricingId}", id);
                throw;
            }
        }

        public async Task<AntdPricingResponse> GetByPlanAsync(string plan)
        {
            try
            {
                var pricing = await _context.AntdPricings
                    .FirstOrDefaultAsync(p => p.Plan.ToLower() == plan.ToLower());

                if (pricing == null)
                    return new AntdPricingResponse { Succeeded = false, Message = "Pricing plan not found" };

                return new AntdPricingResponse
                {
                    Succeeded = true,
                    Data = MapToDto(pricing),
                    Message = "Pricing plan retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plan {Plan}", plan);
                throw;
            }
        }

        public async Task<AntdPricingCreateResponse> CreateAsync(AntdPricingDto pricingDto)
        {
            try
            {
                var pricing = new AntdPricing
                {
                    Id = Guid.NewGuid(),
                    Plan = pricingDto.Plan,
                    Monthly = pricingDto.Monthly,
                    Annually = pricingDto.Annually,
                    SavingsCaption = pricingDto.SavingsCaption,
                    Features = string.Join("\n", pricingDto.Features),
                    Color = pricingDto.Color,
                    Preferred = pricingDto.Preferred,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdPricings.Add(pricing);
                await _context.SaveChangesAsync();

                return new AntdPricingCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(pricing),
                    Message = "Pricing plan created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd pricing plan");
                throw;
            }
        }

        public async Task<AntdPricingUpdateResponse> UpdateAsync(string id, AntdPricingDto pricingDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdPricingUpdateResponse { Succeeded = false, Message = "Invalid pricing ID format" };

                var pricing = await _context.AntdPricings.FindAsync(guidId);
                if (pricing == null)
                    return new AntdPricingUpdateResponse { Succeeded = false, Message = "Pricing plan not found" };

                pricing.Plan = pricingDto.Plan;
                pricing.Monthly = pricingDto.Monthly;
                pricing.Annually = pricingDto.Annually;
                pricing.SavingsCaption = pricingDto.SavingsCaption;
                pricing.Features = string.Join("\n", pricingDto.Features);
                pricing.Color = pricingDto.Color;
                pricing.Preferred = pricingDto.Preferred;
                pricing.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdPricingUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(pricing),
                    Message = "Pricing plan updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd pricing plan {PricingId}", id);
                throw;
            }
        }

        public async Task<AntdPricingDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdPricingDeleteResponse { Succeeded = false, Message = "Invalid pricing ID format" };

                var pricing = await _context.AntdPricings.FindAsync(guidId);
                if (pricing == null)
                    return new AntdPricingDeleteResponse { Succeeded = false, Message = "Pricing plan not found" };

                _context.AntdPricings.Remove(pricing);
                await _context.SaveChangesAsync();

                return new AntdPricingDeleteResponse
                {
                    Succeeded = true,
                    Message = "Pricing plan deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd pricing plan {PricingId}", id);
                throw;
            }
        }

        private static AntdPricingDto MapToDto(AntdPricing pricing)
        {
            return new AntdPricingDto
            {
                Id = pricing.Id.ToString(),
                Plan = pricing.Plan,
                Monthly = pricing.Monthly,
                Annually = pricing.Annually,
                SavingsCaption = pricing.SavingsCaption,
                Features = string.IsNullOrEmpty(pricing.Features)
                    ? new List<string>()
                    : pricing.Features.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList(),
                Color = pricing.Color,
                Preferred = pricing.Preferred
            };
        }
    }
}
