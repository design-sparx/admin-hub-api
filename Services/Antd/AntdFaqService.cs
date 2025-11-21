using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdFaqService : IAntdFaqService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdFaqService> _logger;

        public AntdFaqService(ApplicationDbContext context, ILogger<AntdFaqService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdFaqListResponse> GetAllAsync(AntdFaqQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdFaqs.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Category))
                    query = query.Where(f => f.Category.ToLower() == queryParams.Category.ToLower());

                if (queryParams.IsFeatured.HasValue)
                    query = query.Where(f => f.IsFeatured == queryParams.IsFeatured.Value);

                if (queryParams.MinRating.HasValue)
                    query = query.Where(f => f.Rating >= queryParams.MinRating.Value);

                if (!string.IsNullOrEmpty(queryParams.SearchTerm))
                {
                    var searchLower = queryParams.SearchTerm.ToLower();
                    query = query.Where(f =>
                        f.Question.ToLower().Contains(searchLower) ||
                        f.Answer.ToLower().Contains(searchLower) ||
                        f.Tags.ToLower().Contains(searchLower) ||
                        f.Author.ToLower().Contains(searchLower));
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "question" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.Question)
                        : query.OrderBy(f => f.Question),
                    "category" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.Category)
                        : query.OrderBy(f => f.Category),
                    "rating" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.Rating)
                        : query.OrderBy(f => f.Rating),
                    "datecreated" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.DateCreated)
                        : query.OrderBy(f => f.DateCreated),
                    "views" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.Views)
                        : query.OrderBy(f => f.Views),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(f => f.Views)
                        : query.OrderBy(f => f.Views)
                };

                var total = await query.CountAsync();
                var faqs = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                return new AntdFaqListResponse
                {
                    Succeeded = true,
                    Data = faqs.Select(MapToDto).ToList(),
                    Message = "FAQs retrieved successfully",
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
                _logger.LogError(ex, "Error retrieving Antd FAQs");
                throw;
            }
        }

        public async Task<AntdFaqResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdFaqResponse { Succeeded = false, Message = "Invalid FAQ ID format" };

                var faq = await _context.AntdFaqs.FindAsync(guidId);
                if (faq == null)
                    return new AntdFaqResponse { Succeeded = false, Message = "FAQ not found" };

                // Increment view count
                faq.Views++;
                await _context.SaveChangesAsync();

                return new AntdFaqResponse
                {
                    Succeeded = true,
                    Data = MapToDto(faq),
                    Message = "FAQ retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd FAQ {FaqId}", id);
                throw;
            }
        }

        public async Task<AntdFaqStatisticsResponse> GetStatisticsAsync()
        {
            try
            {
                var faqs = await _context.AntdFaqs.ToListAsync();

                if (faqs.Count == 0)
                {
                    return new AntdFaqStatisticsResponse
                    {
                        Succeeded = true,
                        Data = new AntdFaqStatisticsDto
                        {
                            TotalFaqs = 0,
                            TotalViews = 0,
                            AverageRating = 0,
                            FeaturedCount = 0,
                            FaqsByCategory = new Dictionary<string, int>()
                        },
                        Message = "Statistics retrieved successfully"
                    };
                }

                var statistics = new AntdFaqStatisticsDto
                {
                    TotalFaqs = faqs.Count,
                    TotalViews = faqs.Sum(f => f.Views),
                    AverageRating = faqs.Average(f => f.Rating),
                    FeaturedCount = faqs.Count(f => f.IsFeatured),
                    FaqsByCategory = faqs
                        .GroupBy(f => f.Category)
                        .ToDictionary(g => g.Key, g => g.Count())
                };

                return new AntdFaqStatisticsResponse
                {
                    Succeeded = true,
                    Data = statistics,
                    Message = "Statistics retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd FAQ statistics");
                throw;
            }
        }

        public async Task<AntdFaqListResponse> GetFeaturedAsync(int limit = 10)
        {
            try
            {
                var faqs = await _context.AntdFaqs
                    .Where(f => f.IsFeatured)
                    .OrderByDescending(f => f.Views)
                    .Take(limit)
                    .ToListAsync();

                return new AntdFaqListResponse
                {
                    Succeeded = true,
                    Data = faqs.Select(MapToDto).ToList(),
                    Message = "Featured FAQs retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving featured Antd FAQs");
                throw;
            }
        }

        public async Task<AntdFaqCreateResponse> CreateAsync(AntdFaqDto faqDto)
        {
            try
            {
                var faq = new AntdFaq
                {
                    Id = Guid.NewGuid(),
                    Question = faqDto.Question,
                    Answer = faqDto.Answer,
                    Category = faqDto.Category,
                    DateCreated = string.IsNullOrEmpty(faqDto.DateCreated)
                        ? null
                        : DateTime.Parse(faqDto.DateCreated).ToUniversalTime(),
                    IsFeatured = faqDto.IsFeatured,
                    Views = faqDto.Views,
                    Tags = faqDto.Tags,
                    Rating = faqDto.Rating,
                    Author = faqDto.Author,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdFaqs.Add(faq);
                await _context.SaveChangesAsync();

                return new AntdFaqCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(faq),
                    Message = "FAQ created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd FAQ");
                throw;
            }
        }

        public async Task<AntdFaqUpdateResponse> UpdateAsync(string id, AntdFaqDto faqDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdFaqUpdateResponse { Succeeded = false, Message = "Invalid FAQ ID format" };

                var faq = await _context.AntdFaqs.FindAsync(guidId);
                if (faq == null)
                    return new AntdFaqUpdateResponse { Succeeded = false, Message = "FAQ not found" };

                faq.Question = faqDto.Question;
                faq.Answer = faqDto.Answer;
                faq.Category = faqDto.Category;
                faq.DateCreated = string.IsNullOrEmpty(faqDto.DateCreated)
                    ? null
                    : DateTime.Parse(faqDto.DateCreated).ToUniversalTime();
                faq.IsFeatured = faqDto.IsFeatured;
                faq.Tags = faqDto.Tags;
                faq.Rating = faqDto.Rating;
                faq.Author = faqDto.Author;
                faq.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdFaqUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(faq),
                    Message = "FAQ updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd FAQ {FaqId}", id);
                throw;
            }
        }

        public async Task<AntdFaqDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdFaqDeleteResponse { Succeeded = false, Message = "Invalid FAQ ID format" };

                var faq = await _context.AntdFaqs.FindAsync(guidId);
                if (faq == null)
                    return new AntdFaqDeleteResponse { Succeeded = false, Message = "FAQ not found" };

                _context.AntdFaqs.Remove(faq);
                await _context.SaveChangesAsync();

                return new AntdFaqDeleteResponse
                {
                    Succeeded = true,
                    Message = "FAQ deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd FAQ {FaqId}", id);
                throw;
            }
        }

        private static AntdFaqDto MapToDto(AntdFaq faq)
        {
            return new AntdFaqDto
            {
                Id = faq.Id.ToString(),
                Question = faq.Question,
                Answer = faq.Answer,
                Category = faq.Category,
                DateCreated = faq.DateCreated?.ToString("M/d/yyyy") ?? string.Empty,
                IsFeatured = faq.IsFeatured,
                Views = faq.Views,
                Tags = faq.Tags,
                Rating = faq.Rating,
                Author = faq.Author
            };
        }
    }
}
