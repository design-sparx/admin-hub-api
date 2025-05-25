using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Invoice;
using AdminHubApi.Dtos.InvoiceItem;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities.Invoice;
using AdminHubApi.Interfaces;

namespace AdminHubApi.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger)
    {
        _invoiceRepository = invoiceRepository;
        _logger = logger;
    }

    public async Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetAllAsync()
    {
        try
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<InvoiceResponseDto>>
            {
                Succeeded = true,
                Data = invoices.Select(MapToResponseDto),
                Message = "Invoices retrieved",
                Errors = []
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all orders");

            return new ApiResponse<IEnumerable<InvoiceResponseDto>>
            {
                Succeeded = false,
                Message = "Error getting all invoices",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<InvoiceResponseDto>> GetByIdAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);

        if (invoice == null) throw new KeyNotFoundException($"Invoice with id: {id} was not found");

        return new ApiResponse<InvoiceResponseDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(invoice),
            Message = "Invoice retrieved by id: " + id + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetByUserIdAsync(string userId)
    {
        var invoices = await _invoiceRepository.GetByUserIdAsync(userId);

        return new ApiResponse<IEnumerable<InvoiceResponseDto>>
        {
            Succeeded = true,
            Data = invoices.Select(MapToResponseDto),
            Message = "Invoice retrieved by user id: " + userId + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetByOrderIdAsync(string orderId)
    {
        var invoices = await _invoiceRepository.GetByUserIdAsync(orderId);

        return new ApiResponse<IEnumerable<InvoiceResponseDto>>
        {
            Succeeded = true,
            Data = invoices.Select(MapToResponseDto),
            Message = "Invoice retrieved by order id: " + orderId + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<InvoiceResponseDto>> CreateAsync(Invoice invoice)
    {
        await _invoiceRepository.CreateAsync(invoice);

        return new ApiResponse<InvoiceResponseDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(invoice),
            Message = "Invoice created",
            Errors = []
        };
    }

    public async Task<ApiResponse<InvoiceResponseDto>> UpdateAsync(InvoiceResponseDto invoiceResponse)
    {
        var existingInvoice = await _invoiceRepository.GetByIdAsync(invoiceResponse.Id);

        if (existingInvoice == null)
            throw new KeyNotFoundException($"Invoice with id: {invoiceResponse.Id} was not found");

        existingInvoice.InvoiceNumber = invoiceResponse.InvoiceNumber;
        existingInvoice.IssueDate = invoiceResponse.IssueDate;
        existingInvoice.OrderId = invoiceResponse.OrderId;
        existingInvoice.Notes = invoiceResponse.Notes;
        existingInvoice.DueDate = invoiceResponse.DueDate;
        existingInvoice.PaidAmount = invoiceResponse.PaidAmount;
        existingInvoice.Status = invoiceResponse.Status;
        existingInvoice.ModifiedById = invoiceResponse.ModifiedById;

        await _invoiceRepository.UpdateAsync(existingInvoice);

        return new ApiResponse<InvoiceResponseDto>
        {
            Succeeded = true,
            Data = invoiceResponse,
            Message = "Invoice updated",
            Errors = []
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var product = await _invoiceRepository.GetByIdAsync(id);

        if (product == null)
            throw new KeyNotFoundException($"Invoice with id: {id} was not found");

        await _invoiceRepository.DeleteAsync(id);

        return new ApiResponse<bool>
        {
            Succeeded = true,
            Data = true,
            Message = "Invoice deleted",
            Errors = []
        };
    }

    private static InvoiceResponseDto MapToResponseDto(Invoice invoice)
    {
        return new InvoiceResponseDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.DueDate,
            OrderId = invoice.OrderId, // Now both are nullable
            PaidAmount = invoice.PaidAmount,
            TotalAmount = invoice.TotalAmount,
            Notes = invoice.Notes,
            Status = invoice.Status,

            // Customer fields
            CustomerName = invoice.CustomerName,
            CustomerEmail = invoice.CustomerEmail,
            CustomerPhone = invoice.CustomerPhone,
            CustomerAddress = invoice.CustomerAddress,
            BillingAddress = invoice.BillingAddress,

            // Pricing fields
            Subtotal = invoice.Subtotal,
            TaxRate = invoice.TaxRate,
            TaxAmount = invoice.TaxAmount,
            DiscountAmount = invoice.DiscountAmount,
            PaymentTerms = invoice.PaymentTerms,

            Created = invoice.Created,
            CreatedById = invoice.CreatedById,
            CreatedBy = invoice.CreatedBy != null
                ? new UserDto
                {
                    Id = invoice.CreatedBy.Id,
                    UserName = invoice.CreatedBy.UserName,
                    Email = invoice.CreatedBy.Email,
                    PhoneNumber = invoice.CreatedBy.PhoneNumber,
                    EmailConfirmed = invoice.CreatedBy.EmailConfirmed,
                    PhoneNumberConfirmed = invoice.CreatedBy.PhoneNumberConfirmed,
                    TwoFactorEnabled = invoice.CreatedBy.TwoFactorEnabled,
                    LockoutEnabled = invoice.CreatedBy.LockoutEnabled,
                    LockoutEnd = invoice.CreatedBy.LockoutEnd
                }
                : null,
            Modified = invoice.Modified,
            ModifiedById = invoice.ModifiedById,
            ModifiedBy = invoice.ModifiedBy != null
                ? new UserDto
                {
                    Id = invoice.ModifiedBy.Id,
                    UserName = invoice.ModifiedBy.UserName,
                    Email = invoice.ModifiedBy.Email,
                    PhoneNumber = invoice.ModifiedBy.PhoneNumber,
                    EmailConfirmed = invoice.ModifiedBy.EmailConfirmed,
                    PhoneNumberConfirmed = invoice.ModifiedBy.PhoneNumberConfirmed,
                    TwoFactorEnabled = invoice.ModifiedBy.TwoFactorEnabled,
                    LockoutEnabled = invoice.ModifiedBy.LockoutEnabled,
                    LockoutEnd = invoice.ModifiedBy.LockoutEnd
                }
                : null,

            // Map invoice items
            Items = invoice.Items?.Select(item => new InvoiceItemResponseDto
            {
                Id = item.Id,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                ProductId = item.ProductId,
                Created = item.Created,
                Modified = item.Modified
            }).ToList() ?? new List<InvoiceItemResponseDto>()
        };
    }
}