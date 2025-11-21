using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdEmployeeService : IAntdEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdEmployeeService> _logger;

        public AntdEmployeeService(ApplicationDbContext context, ILogger<AntdEmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdEmployeeListResponse> GetAllAsync(AntdEmployeeQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdEmployees.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Role))
                    query = query.Where(e => e.Role.ToLower() == queryParams.Role.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Country))
                    query = query.Where(e => e.Country.ToLower() == queryParams.Country.ToLower());

                if (queryParams.MinAge.HasValue)
                    query = query.Where(e => e.Age >= queryParams.MinAge.Value);

                if (queryParams.MaxAge.HasValue)
                    query = query.Where(e => e.Age <= queryParams.MaxAge.Value);

                if (queryParams.MinSalary.HasValue)
                    query = query.Where(e => e.Salary >= queryParams.MinSalary.Value);

                if (queryParams.MaxSalary.HasValue)
                    query = query.Where(e => e.Salary <= queryParams.MaxSalary.Value);

                if (!string.IsNullOrEmpty(queryParams.SearchTerm))
                {
                    var searchLower = queryParams.SearchTerm.ToLower();
                    query = query.Where(e =>
                        e.FirstName.ToLower().Contains(searchLower) ||
                        e.LastName.ToLower().Contains(searchLower) ||
                        e.Email.ToLower().Contains(searchLower) ||
                        e.Role.ToLower().Contains(searchLower));
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "firstname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.FirstName)
                        : query.OrderBy(e => e.FirstName),
                    "lastname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.LastName)
                        : query.OrderBy(e => e.LastName),
                    "age" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Age)
                        : query.OrderBy(e => e.Age),
                    "salary" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Salary)
                        : query.OrderBy(e => e.Salary),
                    "hiredate" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.HireDate)
                        : query.OrderBy(e => e.HireDate),
                    "role" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Role)
                        : query.OrderBy(e => e.Role),
                    "country" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Country)
                        : query.OrderBy(e => e.Country),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.LastName)
                        : query.OrderBy(e => e.LastName)
                };

                var total = await query.CountAsync();
                var employees = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                return new AntdEmployeeListResponse
                {
                    Succeeded = true,
                    Data = employees.Select(MapToDto).ToList(),
                    Message = "Employees retrieved successfully",
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
                _logger.LogError(ex, "Error retrieving Antd employees");
                throw;
            }
        }

        public async Task<AntdEmployeeResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdEmployeeResponse { Succeeded = false, Message = "Invalid employee ID format" };

                var employee = await _context.AntdEmployees.FindAsync(guidId);
                if (employee == null)
                    return new AntdEmployeeResponse { Succeeded = false, Message = "Employee not found" };

                return new AntdEmployeeResponse
                {
                    Succeeded = true,
                    Data = MapToDto(employee),
                    Message = "Employee retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd employee {EmployeeId}", id);
                throw;
            }
        }

        public async Task<AntdEmployeeStatisticsResponse> GetStatisticsAsync()
        {
            try
            {
                var employees = await _context.AntdEmployees.ToListAsync();

                if (employees.Count == 0)
                {
                    return new AntdEmployeeStatisticsResponse
                    {
                        Succeeded = true,
                        Data = new AntdEmployeeStatisticsDto
                        {
                            TotalEmployees = 0,
                            AverageSalary = 0,
                            AverageAge = 0,
                            EmployeesByCountry = new Dictionary<string, int>(),
                            EmployeesByRole = new Dictionary<string, int>()
                        },
                        Message = "Statistics retrieved successfully"
                    };
                }

                var statistics = new AntdEmployeeStatisticsDto
                {
                    TotalEmployees = employees.Count,
                    AverageSalary = employees.Average(e => e.Salary),
                    AverageAge = employees.Average(e => e.Age),
                    EmployeesByCountry = employees
                        .GroupBy(e => e.Country)
                        .ToDictionary(g => g.Key, g => g.Count()),
                    EmployeesByRole = employees
                        .GroupBy(e => e.Role)
                        .ToDictionary(g => g.Key, g => g.Count())
                };

                return new AntdEmployeeStatisticsResponse
                {
                    Succeeded = true,
                    Data = statistics,
                    Message = "Statistics retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd employee statistics");
                throw;
            }
        }

        public async Task<AntdEmployeeCreateResponse> CreateAsync(AntdEmployeeDto employeeDto)
        {
            try
            {
                var employee = new AntdEmployee
                {
                    Id = Guid.NewGuid(),
                    Title = employeeDto.Title,
                    FirstName = employeeDto.FirstName,
                    MiddleName = employeeDto.MiddleName,
                    LastName = employeeDto.LastName,
                    Avatar = employeeDto.Avatar,
                    Role = employeeDto.Role,
                    Age = employeeDto.Age,
                    Email = employeeDto.Email,
                    Country = employeeDto.Country,
                    FavoriteColor = employeeDto.FavoriteColor,
                    HireDate = string.IsNullOrEmpty(employeeDto.HireDate)
                        ? null
                        : DateTime.Parse(employeeDto.HireDate).ToUniversalTime(),
                    Salary = employeeDto.Salary,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdEmployees.Add(employee);
                await _context.SaveChangesAsync();

                return new AntdEmployeeCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(employee),
                    Message = "Employee created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd employee");
                throw;
            }
        }

        public async Task<AntdEmployeeUpdateResponse> UpdateAsync(string id, AntdEmployeeDto employeeDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdEmployeeUpdateResponse { Succeeded = false, Message = "Invalid employee ID format" };

                var employee = await _context.AntdEmployees.FindAsync(guidId);
                if (employee == null)
                    return new AntdEmployeeUpdateResponse { Succeeded = false, Message = "Employee not found" };

                employee.Title = employeeDto.Title;
                employee.FirstName = employeeDto.FirstName;
                employee.MiddleName = employeeDto.MiddleName;
                employee.LastName = employeeDto.LastName;
                employee.Avatar = employeeDto.Avatar;
                employee.Role = employeeDto.Role;
                employee.Age = employeeDto.Age;
                employee.Email = employeeDto.Email;
                employee.Country = employeeDto.Country;
                employee.FavoriteColor = employeeDto.FavoriteColor;
                employee.HireDate = string.IsNullOrEmpty(employeeDto.HireDate)
                    ? null
                    : DateTime.Parse(employeeDto.HireDate).ToUniversalTime();
                employee.Salary = employeeDto.Salary;
                employee.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdEmployeeUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(employee),
                    Message = "Employee updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd employee {EmployeeId}", id);
                throw;
            }
        }

        public async Task<AntdEmployeeDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdEmployeeDeleteResponse { Succeeded = false, Message = "Invalid employee ID format" };

                var employee = await _context.AntdEmployees.FindAsync(guidId);
                if (employee == null)
                    return new AntdEmployeeDeleteResponse { Succeeded = false, Message = "Employee not found" };

                _context.AntdEmployees.Remove(employee);
                await _context.SaveChangesAsync();

                return new AntdEmployeeDeleteResponse
                {
                    Succeeded = true,
                    Message = "Employee deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd employee {EmployeeId}", id);
                throw;
            }
        }

        private static AntdEmployeeDto MapToDto(AntdEmployee employee)
        {
            return new AntdEmployeeDto
            {
                EmployeeId = employee.Id.ToString(),
                Title = employee.Title,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Avatar = employee.Avatar,
                Role = employee.Role,
                Age = employee.Age,
                Email = employee.Email,
                Country = employee.Country,
                FavoriteColor = employee.FavoriteColor,
                HireDate = employee.HireDate?.ToString("M/d/yyyy") ?? string.Empty,
                Salary = employee.Salary
            };
        }
    }
}
