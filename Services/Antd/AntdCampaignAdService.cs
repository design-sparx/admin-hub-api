using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdCampaignAdService : IAntdCampaignAdService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdCampaignAdService> _logger;

        public AntdCampaignAdService(ApplicationDbContext context, ILogger<AntdCampaignAdService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdCampaignAdListResponse> GetAllAsync(AntdCampaignAdQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdCampaignAds.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.AdSource))
                    query = query.Where(c => c.AdSource.ToLower() == queryParams.AdSource.ToLower());

                if (!string.IsNullOrEmpty(queryParams.AdCampaign))
                    query = query.Where(c => c.AdCampaign.Contains(queryParams.AdCampaign));

                if (!string.IsNullOrEmpty(queryParams.AdType))
                    query = query.Where(c => c.AdType.ToLower() == queryParams.AdType.ToLower());

                if (queryParams.StartDateFrom.HasValue)
                    query = query.Where(c => c.StartDate >= queryParams.StartDateFrom.Value);

                if (queryParams.StartDateTo.HasValue)
                    query = query.Where(c => c.StartDate <= queryParams.StartDateTo.Value);

                if (queryParams.MinRoi.HasValue)
                    query = query.Where(c => c.Roi >= queryParams.MinRoi.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "impressions" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Impressions) : query.OrderBy(c => c.Impressions),
                    "clicks" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Clicks) : query.OrderBy(c => c.Clicks),
                    "conversions" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Conversions) : query.OrderBy(c => c.Conversions),
                    "cost" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Cost) : query.OrderBy(c => c.Cost),
                    "revenue" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Revenue) : query.OrderBy(c => c.Revenue),
                    "roi" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Roi) : query.OrderBy(c => c.Roi),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.StartDate) : query.OrderBy(c => c.StartDate)
                };

                var total = await query.CountAsync();
                var ads = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdCampaignAdListResponse
                {
                    Succeeded = true,
                    Data = ads.Select(MapToDto).ToList(),
                    Message = "Campaign ads retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd campaign ads");
                throw;
            }
        }

        public async Task<AntdCampaignAdResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCampaignAdResponse { Succeeded = false, Message = "Invalid campaign ad ID format" };

                var ad = await _context.AntdCampaignAds.FindAsync(guidId);
                if (ad == null)
                    return new AntdCampaignAdResponse { Succeeded = false, Message = "Campaign ad not found" };

                return new AntdCampaignAdResponse { Succeeded = true, Data = MapToDto(ad), Message = "Campaign ad retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd campaign ad {AdId}", id);
                throw;
            }
        }

        public async Task<AntdCampaignAdCreateResponse> CreateAsync(AntdCampaignAdDto dto)
        {
            try
            {
                var ad = new AntdCampaignAd
                {
                    Id = Guid.NewGuid(),
                    AdSource = dto.AdSource,
                    AdCampaign = dto.AdCampaign,
                    AdGroup = dto.AdGroup,
                    AdType = dto.AdType,
                    Impressions = dto.Impressions,
                    Clicks = dto.Clicks,
                    Conversions = dto.Conversions,
                    Cost = dto.Cost,
                    ConversionRate = dto.ConversionRate,
                    Revenue = dto.Revenue,
                    Roi = dto.Roi,
                    StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdCampaignAds.Add(ad);
                await _context.SaveChangesAsync();

                return new AntdCampaignAdCreateResponse { Succeeded = true, Data = MapToDto(ad), Message = "Campaign ad created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd campaign ad");
                throw;
            }
        }

        public async Task<AntdCampaignAdUpdateResponse> UpdateAsync(string id, AntdCampaignAdDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCampaignAdUpdateResponse { Succeeded = false, Message = "Invalid campaign ad ID format" };

                var ad = await _context.AntdCampaignAds.FindAsync(guidId);
                if (ad == null)
                    return new AntdCampaignAdUpdateResponse { Succeeded = false, Message = "Campaign ad not found" };

                ad.AdSource = dto.AdSource;
                ad.AdCampaign = dto.AdCampaign;
                ad.AdGroup = dto.AdGroup;
                ad.AdType = dto.AdType;
                ad.Impressions = dto.Impressions;
                ad.Clicks = dto.Clicks;
                ad.Conversions = dto.Conversions;
                ad.Cost = dto.Cost;
                ad.ConversionRate = dto.ConversionRate;
                ad.Revenue = dto.Revenue;
                ad.Roi = dto.Roi;
                ad.StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime();
                ad.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdCampaignAdUpdateResponse { Succeeded = true, Data = MapToDto(ad), Message = "Campaign ad updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd campaign ad {AdId}", id);
                throw;
            }
        }

        public async Task<AntdCampaignAdDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCampaignAdDeleteResponse { Succeeded = false, Message = "Invalid campaign ad ID format" };

                var ad = await _context.AntdCampaignAds.FindAsync(guidId);
                if (ad == null)
                    return new AntdCampaignAdDeleteResponse { Succeeded = false, Message = "Campaign ad not found" };

                _context.AntdCampaignAds.Remove(ad);
                await _context.SaveChangesAsync();

                return new AntdCampaignAdDeleteResponse { Succeeded = true, Message = "Campaign ad deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd campaign ad {AdId}", id);
                throw;
            }
        }

        private static AntdCampaignAdDto MapToDto(AntdCampaignAd ad)
        {
            return new AntdCampaignAdDto
            {
                Id = ad.Id.ToString(),
                AdSource = ad.AdSource,
                AdCampaign = ad.AdCampaign,
                AdGroup = ad.AdGroup,
                AdType = ad.AdType,
                Impressions = ad.Impressions,
                Clicks = ad.Clicks,
                Conversions = ad.Conversions,
                Cost = ad.Cost,
                ConversionRate = ad.ConversionRate,
                Revenue = ad.Revenue,
                Roi = ad.Roi,
                StartDate = ad.StartDate.ToString("yyyy-MM-dd")
            };
        }
    }
}
