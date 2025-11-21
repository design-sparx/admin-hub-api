using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/faqs")]
    [Tags("Antd - FAQs")]
    [PermissionAuthorize(Permissions.Antd.Faqs)]
    public class AntdFaqsController : AntdBaseController
    {
        private readonly IAntdFaqService _faqService;

        public AntdFaqsController(IAntdFaqService faqService, ILogger<AntdFaqsController> logger)
            : base(logger)
        {
            _faqService = faqService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdFaqListResponse), 200)]
        public async Task<IActionResult> GetAllFaqs([FromQuery] AntdFaqQueryParams queryParams)
        {
            try
            {
                var response = await _faqService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd FAQs");
                return ErrorResponse("Failed to retrieve FAQs", 500);
            }
        }

        [HttpGet("featured")]
        [ProducesResponseType(typeof(AntdFaqListResponse), 200)]
        public async Task<IActionResult> GetFeaturedFaqs([FromQuery] int limit = 10)
        {
            try
            {
                var response = await _faqService.GetFeaturedAsync(limit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving featured Antd FAQs");
                return ErrorResponse("Failed to retrieve featured FAQs", 500);
            }
        }

        [HttpGet("statistics")]
        [ProducesResponseType(typeof(AntdFaqStatisticsResponse), 200)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var response = await _faqService.GetStatisticsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd FAQ statistics");
                return ErrorResponse("Failed to retrieve statistics", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdFaqResponse), 200)]
        public async Task<IActionResult> GetFaqById(string id)
        {
            try
            {
                var response = await _faqService.GetByIdAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd FAQ {FaqId}", id);
                return ErrorResponse("Failed to retrieve FAQ", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdFaqCreateResponse), 201)]
        public async Task<IActionResult> CreateFaq([FromBody] AntdFaqDto faqDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _faqService.CreateAsync(faqDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd FAQ");
                return ErrorResponse("Failed to create FAQ", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdFaqUpdateResponse), 200)]
        public async Task<IActionResult> UpdateFaq(string id, [FromBody] AntdFaqDto faqDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _faqService.UpdateAsync(id, faqDto);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd FAQ {FaqId}", id);
                return ErrorResponse("Failed to update FAQ", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdFaqDeleteResponse), 200)]
        public async Task<IActionResult> DeleteFaq(string id)
        {
            try
            {
                var response = await _faqService.DeleteAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd FAQ {FaqId}", id);
                return ErrorResponse("Failed to delete FAQ", 500);
            }
        }
    }
}
