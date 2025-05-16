using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Invoice;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities.Invoice;
using AdminHubApi.Interfaces;

namespace AdminHubApi.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ApiResponse<IEnumerable<InvoiceDto>>> GetAllAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        return new ApiResponse<IEnumerable<InvoiceDto>>
        {
            Succeeded = true,
            Data = invoices.Select(MapToResponseDto),
            Message = "Invoices retrieved",
            Errors = []
        };
    }

    public async Task<ApiResponse<InvoiceDto>> GetByIdAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);

        if (invoice == null) throw new KeyNotFoundException($"Invoice with id: {id} was not found");

        return new ApiResponse<InvoiceDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(invoice),
            Message = "Invoice retrieved by id: " + id + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<IEnumerable<InvoiceDto>>> GetByUserIdAsync(string userId)
    {
        var invoices = await _invoiceRepository.GetByUserIdAsync(userId);

        return new ApiResponse<IEnumerable<InvoiceDto>>
        {
            Succeeded = true,
            Data = invoices.Select(MapToResponseDto),
            Message = "Invoice retrieved by user id: " + userId + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<IEnumerable<InvoiceDto>>> GetByOrderIdAsync(string orderId)
    {
        var invoices = await _invoiceRepository.GetByUserIdAsync(orderId);

        return new ApiResponse<IEnumerable<InvoiceDto>>
        {
            Succeeded = true,
            Data = invoices.Select(MapToResponseDto),
            Message = "Invoice retrieved by order id: " + orderId + "",
            Errors = []
        };
    }

    public async Task<ApiResponse<InvoiceDto>> CreateAsync(Invoice invoice)
    {
        await _invoiceRepository.CreateAsync(invoice);

        return new ApiResponse<InvoiceDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(invoice),
            Message = "Invoice created",
            Errors = []
        };
    }

    public async Task<ApiResponse<InvoiceDto>> UpdateAsync(InvoiceDto invoice)
    {
        var existingInvoice = await _invoiceRepository.GetByIdAsync(invoice.Id);

        if (existingInvoice == null) 
            throw new KeyNotFoundException($"Invoice with id: {invoice.Id} was not found");
            
        existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
        existingInvoice.IssueDate = invoice.IssueDate;
        existingInvoice.OrderId = invoice.OrderId;
        existingInvoice.Notes = invoice.Notes;
        existingInvoice.DueDate = invoice.DueDate;
        existingInvoice.PaidAmount = invoice.PaidAmount;
        existingInvoice.Status = invoice.Status;
        existingInvoice.ModifiedById = invoice.ModifiedById;

        await _invoiceRepository.UpdateAsync(existingInvoice);
        
        return new ApiResponse<InvoiceDto>
        {
            Succeeded = true,
            Data = invoice,
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

    private static InvoiceDto MapToResponseDto(Invoice invoice)
    {
        return new InvoiceDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.DueDate,
            OrderId = invoice.OrderId,
            PaidAmount = invoice.PaidAmount,
            Notes = invoice.Notes,
            Status = invoice.Status,
            Created = invoice.Created,
            Modified = invoice.Modified,
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
                : null
        };
    }
}