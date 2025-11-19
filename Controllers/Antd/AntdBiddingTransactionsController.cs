using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/bidding-transactions")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.BiddingTransactions)]
    public class AntdBiddingTransactionsController : ControllerBase
    {
        private readonly IAntdBiddingTransactionService _transactionService;

        public AntdBiddingTransactionsController(IAntdBiddingTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdBiddingTransactionQueryParams queryParams)
        {
            var result = await _transactionService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _transactionService.GetByIdAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdBiddingTransactionDto dto)
        {
            var result = await _transactionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdBiddingTransactionDto dto)
        {
            var result = await _transactionService.UpdateAsync(id, dto);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _transactionService.DeleteAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}
