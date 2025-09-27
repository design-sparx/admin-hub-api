using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/sales")]
    [Tags("Mantine - Sales")]
    public class SalesController : MantineBaseController
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
            : base(logger)
        {
            _salesService = salesService;
        }

        /// <summary>
        /// Get all sales with pagination and filtering
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSales([FromQuery] SalesQueryParams queryParams)
        {
            try
            {
                var sales = await _salesService.GetAllAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = sales.Data,
                    message = "Sales retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = sales.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales");
                return ErrorResponse("Failed to retrieve sales", 500);
            }
        }

        /// <summary>
        /// Get sale by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                var sale = await _salesService.GetByIdAsync(id);
                if (sale == null)
                    return NotFound(new { success = false, message = "Sale not found" });

                return SuccessResponse(sale, "Sale retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sale {SaleId}", id);
                return ErrorResponse("Failed to retrieve sale", 500);
            }
        }

        /// <summary>
        /// Create new sale
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SalesDto salesDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var sale = await _salesService.CreateAsync(salesDto);
                return SuccessResponse(sale, "Sale created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                return ErrorResponse("Failed to create sale", 500);
            }
        }

        /// <summary>
        /// Update existing sale
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] SalesDto salesDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var sale = await _salesService.UpdateAsync(id, salesDto);
                if (sale == null)
                    return NotFound(new { success = false, message = "Sale not found" });

                return SuccessResponse(sale, "Sale updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale {SaleId}", id);
                return ErrorResponse("Failed to update sale", 500);
            }
        }

        /// <summary>
        /// Delete sale
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                var deleted = await _salesService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Sale not found" });

                return SuccessResponse(new { }, "Sale deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale {SaleId}", id);
                return ErrorResponse("Failed to delete sale", 500);
            }
        }
    }
}