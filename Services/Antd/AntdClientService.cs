using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdClientService : IAntdClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdClientService> _logger;

        public AntdClientService(ApplicationDbContext context, ILogger<AntdClientService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdClientListResponse> GetAllAsync(AntdClientQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdClients.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Email))
                {
                    query = query.Where(c => c.Email.Contains(queryParams.Email));
                }

                if (!string.IsNullOrEmpty(queryParams.ProductName))
                {
                    query = query.Where(c => c.ProductName.Contains(queryParams.ProductName));
                }

                if (!string.IsNullOrEmpty(queryParams.Country))
                {
                    query = query.Where(c => c.Country.ToLower() == queryParams.Country.ToLower());
                }

                if (queryParams.PurchaseDateFrom.HasValue)
                {
                    query = query.Where(c => c.PurchaseDate >= queryParams.PurchaseDateFrom.Value);
                }

                if (queryParams.PurchaseDateTo.HasValue)
                {
                    query = query.Where(c => c.PurchaseDate <= queryParams.PurchaseDateTo.Value);
                }

                if (queryParams.MinTotalPrice.HasValue)
                {
                    query = query.Where(c => c.TotalPrice >= queryParams.MinTotalPrice.Value);
                }

                if (queryParams.MaxTotalPrice.HasValue)
                {
                    query = query.Where(c => c.TotalPrice <= queryParams.MaxTotalPrice.Value);
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "firstname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.FirstName)
                        : query.OrderBy(c => c.FirstName),
                    "lastname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.LastName)
                        : query.OrderBy(c => c.LastName),
                    "email" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.Email)
                        : query.OrderBy(c => c.Email),
                    "productname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.ProductName)
                        : query.OrderBy(c => c.ProductName),
                    "totalprice" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.TotalPrice)
                        : query.OrderBy(c => c.TotalPrice),
                    "country" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.Country)
                        : query.OrderBy(c => c.Country),
                    "quantity" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.Quantity)
                        : query.OrderBy(c => c.Quantity),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.PurchaseDate)
                        : query.OrderBy(c => c.PurchaseDate)
                };

                var total = await query.CountAsync();
                var clients = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var clientsDto = clients.Select(MapToDto).ToList();

                return new AntdClientListResponse
                {
                    Succeeded = true,
                    Data = clientsDto,
                    Message = "Clients retrieved successfully",
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
                _logger.LogError(ex, "Error retrieving Antd clients data");
                throw;
            }
        }

        public async Task<AntdClientResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdClientResponse
                    {
                        Succeeded = false,
                        Message = "Invalid client ID format"
                    };
                }

                var client = await _context.AntdClients.FindAsync(guidId);
                if (client == null)
                {
                    return new AntdClientResponse
                    {
                        Succeeded = false,
                        Message = "Client not found"
                    };
                }

                return new AntdClientResponse
                {
                    Succeeded = true,
                    Data = MapToDto(client),
                    Message = "Client retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd client with ID {ClientId}", id);
                throw;
            }
        }

        public async Task<AntdClientCreateResponse> CreateAsync(AntdClientDto clientDto)
        {
            try
            {
                var client = new AntdClient
                {
                    Id = Guid.NewGuid(),
                    FirstName = clientDto.FirstName,
                    LastName = clientDto.LastName,
                    Email = clientDto.Email,
                    PhoneNumber = clientDto.PhoneNumber,
                    PurchaseDate = DateTime.Parse(clientDto.PurchaseDate).ToUniversalTime(),
                    ProductName = clientDto.ProductName,
                    Quantity = clientDto.Quantity,
                    UnitPrice = clientDto.UnitPrice,
                    TotalPrice = clientDto.TotalPrice,
                    Country = clientDto.Country,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdClients.Add(client);
                await _context.SaveChangesAsync();

                return new AntdClientCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(client),
                    Message = "Client created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new Antd client");
                throw;
            }
        }

        public async Task<AntdClientUpdateResponse> UpdateAsync(string id, AntdClientDto clientDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdClientUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Invalid client ID format"
                    };
                }

                var client = await _context.AntdClients.FindAsync(guidId);
                if (client == null)
                {
                    return new AntdClientUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Client not found"
                    };
                }

                client.FirstName = clientDto.FirstName;
                client.LastName = clientDto.LastName;
                client.Email = clientDto.Email;
                client.PhoneNumber = clientDto.PhoneNumber;
                client.PurchaseDate = DateTime.Parse(clientDto.PurchaseDate).ToUniversalTime();
                client.ProductName = clientDto.ProductName;
                client.Quantity = clientDto.Quantity;
                client.UnitPrice = clientDto.UnitPrice;
                client.TotalPrice = clientDto.TotalPrice;
                client.Country = clientDto.Country;
                client.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdClientUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(client),
                    Message = "Client updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd client with ID {ClientId}", id);
                throw;
            }
        }

        public async Task<AntdClientDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdClientDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Invalid client ID format"
                    };
                }

                var client = await _context.AntdClients.FindAsync(guidId);
                if (client == null)
                {
                    return new AntdClientDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Client not found"
                    };
                }

                _context.AntdClients.Remove(client);
                await _context.SaveChangesAsync();

                return new AntdClientDeleteResponse
                {
                    Succeeded = true,
                    Message = "Client deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd client with ID {ClientId}", id);
                throw;
            }
        }

        private static AntdClientDto MapToDto(AntdClient client)
        {
            return new AntdClientDto
            {
                ClientId = client.Id.ToString(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                PurchaseDate = client.PurchaseDate.ToString("yyyy-MM-dd"),
                ProductName = client.ProductName,
                Quantity = client.Quantity,
                UnitPrice = client.UnitPrice,
                TotalPrice = client.TotalPrice,
                Country = client.Country
            };
        }
    }
}
