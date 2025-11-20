using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdBiddingTransactionService : IAntdBiddingTransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdBiddingTransactionService> _logger;

        public AntdBiddingTransactionService(ApplicationDbContext context, ILogger<AntdBiddingTransactionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdBiddingTransactionListResponse> GetAllAsync(AntdBiddingTransactionQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdBiddingTransactions.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.TransactionType))
                    query = query.Where(t => t.TransactionType.ToLower() == queryParams.TransactionType.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Seller))
                    query = query.Where(t => t.Seller.Contains(queryParams.Seller));

                if (!string.IsNullOrEmpty(queryParams.Buyer))
                    query = query.Where(t => t.Buyer.Contains(queryParams.Buyer));

                if (!string.IsNullOrEmpty(queryParams.Country))
                    query = query.Where(t => t.Country.ToLower() == queryParams.Country.ToLower());

                if (queryParams.DateFrom.HasValue)
                    query = query.Where(t => t.TransactionDate >= queryParams.DateFrom.Value);

                if (queryParams.DateTo.HasValue)
                    query = query.Where(t => t.TransactionDate <= queryParams.DateTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "transactiondate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.TransactionDate) : query.OrderBy(t => t.TransactionDate),
                    "profit" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.Profit) : query.OrderBy(t => t.Profit),
                    "purchaseprice" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.PurchasePrice) : query.OrderBy(t => t.PurchasePrice),
                    "saleprice" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.SalePrice) : query.OrderBy(t => t.SalePrice),
                    "quantity" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.Quantity) : query.OrderBy(t => t.Quantity),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(t => t.TransactionDate) : query.OrderBy(t => t.TransactionDate)
                };

                var total = await query.CountAsync();
                var transactions = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdBiddingTransactionListResponse
                {
                    Success = true,
                    Data = transactions.Select(MapToDto).ToList(),
                    Message = "Bidding transactions retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd bidding transactions");
                throw;
            }
        }

        public async Task<AntdBiddingTransactionResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTransactionResponse { Success = false, Message = "Invalid transaction ID format" };

                var transaction = await _context.AntdBiddingTransactions.FindAsync(guidId);
                if (transaction == null)
                    return new AntdBiddingTransactionResponse { Success = false, Message = "Transaction not found" };

                return new AntdBiddingTransactionResponse { Success = true, Data = MapToDto(transaction), Message = "Transaction retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd bidding transaction {TransactionId}", id);
                throw;
            }
        }

        public async Task<AntdBiddingTransactionCreateResponse> CreateAsync(AntdBiddingTransactionDto dto)
        {
            try
            {
                var transaction = new AntdBiddingTransaction
                {
                    Id = Guid.NewGuid(),
                    Image = dto.Image,
                    ProductId = dto.ProductId,
                    TransactionDate = DateTime.Parse(dto.TransactionDate).ToUniversalTime(),
                    Seller = dto.Seller,
                    Buyer = dto.Buyer,
                    PurchasePrice = dto.PurchasePrice,
                    SalePrice = dto.SalePrice,
                    Profit = dto.Profit,
                    Quantity = dto.Quantity,
                    ShippingAddress = dto.ShippingAddress,
                    State = dto.State,
                    Country = dto.Country,
                    TransactionType = dto.TransactionType,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdBiddingTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                return new AntdBiddingTransactionCreateResponse { Success = true, Data = MapToDto(transaction), Message = "Transaction created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd bidding transaction");
                throw;
            }
        }

        public async Task<AntdBiddingTransactionUpdateResponse> UpdateAsync(string id, AntdBiddingTransactionDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTransactionUpdateResponse { Success = false, Message = "Invalid transaction ID format" };

                var transaction = await _context.AntdBiddingTransactions.FindAsync(guidId);
                if (transaction == null)
                    return new AntdBiddingTransactionUpdateResponse { Success = false, Message = "Transaction not found" };

                transaction.Image = dto.Image;
                transaction.ProductId = dto.ProductId;
                transaction.TransactionDate = DateTime.Parse(dto.TransactionDate).ToUniversalTime();
                transaction.Seller = dto.Seller;
                transaction.Buyer = dto.Buyer;
                transaction.PurchasePrice = dto.PurchasePrice;
                transaction.SalePrice = dto.SalePrice;
                transaction.Profit = dto.Profit;
                transaction.Quantity = dto.Quantity;
                transaction.ShippingAddress = dto.ShippingAddress;
                transaction.State = dto.State;
                transaction.Country = dto.Country;
                transaction.TransactionType = dto.TransactionType;
                transaction.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdBiddingTransactionUpdateResponse { Success = true, Data = MapToDto(transaction), Message = "Transaction updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd bidding transaction {TransactionId}", id);
                throw;
            }
        }

        public async Task<AntdBiddingTransactionDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdBiddingTransactionDeleteResponse { Success = false, Message = "Invalid transaction ID format" };

                var transaction = await _context.AntdBiddingTransactions.FindAsync(guidId);
                if (transaction == null)
                    return new AntdBiddingTransactionDeleteResponse { Success = false, Message = "Transaction not found" };

                _context.AntdBiddingTransactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return new AntdBiddingTransactionDeleteResponse { Success = true, Message = "Transaction deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd bidding transaction {TransactionId}", id);
                throw;
            }
        }

        private static AntdBiddingTransactionDto MapToDto(AntdBiddingTransaction transaction)
        {
            return new AntdBiddingTransactionDto
            {
                Id = transaction.Id.ToString(),
                Image = transaction.Image,
                ProductId = transaction.ProductId,
                TransactionDate = transaction.TransactionDate.ToString("M/d/yyyy"),
                Seller = transaction.Seller,
                Buyer = transaction.Buyer,
                PurchasePrice = transaction.PurchasePrice,
                SalePrice = transaction.SalePrice,
                Profit = transaction.Profit,
                Quantity = transaction.Quantity,
                ShippingAddress = transaction.ShippingAddress,
                State = transaction.State ?? "",
                Country = transaction.Country,
                TransactionType = transaction.TransactionType
            };
        }
    }
}
