using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.Common;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdCommunityGroupService : IAntdCommunityGroupService
    {
        private readonly ApplicationDbContext _context;

        public AntdCommunityGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AntdCommunityGroupListResponse> GetAllAsync(AntdCommunityGroupQueryParams queryParams)
        {
            var query = _context.AntdCommunityGroups.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Category))
                query = query.Where(g => g.Category.ToLower().Contains(queryParams.Category.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Location))
                query = query.Where(g => g.Location.ToLower().Contains(queryParams.Location.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Leader))
                query = query.Where(g => g.Leader.ToLower().Contains(queryParams.Leader.ToLower()));

            if (queryParams.MinSize.HasValue)
                query = query.Where(g => g.Size >= queryParams.MinSize.Value);

            if (queryParams.MaxSize.HasValue)
                query = query.Where(g => g.Size <= queryParams.MaxSize.Value);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "name" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(g => g.Name) : query.OrderBy(g => g.Name),
                "size" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(g => g.Size) : query.OrderBy(g => g.Size),
                "category" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(g => g.Category) : query.OrderBy(g => g.Category),
                "startdate" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(g => g.StartDate) : query.OrderBy(g => g.StartDate),
                _ => query.OrderByDescending(g => g.CreatedAt)
            };

            // Apply pagination
            var groups = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new AntdCommunityGroupListResponse
            {
                Data = groups.Select(MapToResponseDto).ToList(),
                Meta = new PaginationMeta
                {
                    CurrentPage = queryParams.Page,
                    PageSize = queryParams.PageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
                }
            };
        }

        public async Task<AntdCommunityGroupResponse> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid group ID format");

            var group = await _context.AntdCommunityGroups.FindAsync(guid);
            if (group == null)
                throw new KeyNotFoundException($"Community group with ID {id} not found");

            return new AntdCommunityGroupResponse { Data = MapToResponseDto(group) };
        }

        public async Task<AntdCommunityGroupCreateResponse> CreateAsync(AntdCommunityGroupDto dto)
        {
            var group = new AntdCommunityGroup
            {
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                Category = dto.Category,
                Location = dto.Location,
                Size = dto.Size,
                Leader = dto.Leader,
                StartDate = dto.StartDate,
                MeetingTime = dto.MeetingTime,
                MemberAgeRange = dto.MemberAgeRange,
                MemberInterests = dto.MemberInterests,
                FavoriteColor = dto.FavoriteColor
            };

            _context.AntdCommunityGroups.Add(group);
            await _context.SaveChangesAsync();

            return new AntdCommunityGroupCreateResponse
            {
                Message = "Community group created successfully",
                Data = MapToResponseDto(group)
            };
        }

        public async Task<AntdCommunityGroupUpdateResponse> UpdateAsync(string id, AntdCommunityGroupDto dto)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid group ID format");

            var group = await _context.AntdCommunityGroups.FindAsync(guid);
            if (group == null)
                throw new KeyNotFoundException($"Community group with ID {id} not found");

            group.Name = dto.Name;
            group.Description = dto.Description;
            group.Image = dto.Image;
            group.Category = dto.Category;
            group.Location = dto.Location;
            group.Size = dto.Size;
            group.Leader = dto.Leader;
            group.StartDate = dto.StartDate;
            group.MeetingTime = dto.MeetingTime;
            group.MemberAgeRange = dto.MemberAgeRange;
            group.MemberInterests = dto.MemberInterests;
            group.FavoriteColor = dto.FavoriteColor;
            group.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new AntdCommunityGroupUpdateResponse
            {
                Message = "Community group updated successfully",
                Data = MapToResponseDto(group)
            };
        }

        public async Task<AntdCommunityGroupDeleteResponse> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid group ID format");

            var group = await _context.AntdCommunityGroups.FindAsync(guid);
            if (group == null)
                throw new KeyNotFoundException($"Community group with ID {id} not found");

            _context.AntdCommunityGroups.Remove(group);
            await _context.SaveChangesAsync();

            return new AntdCommunityGroupDeleteResponse { Message = "Community group deleted successfully" };
        }

        private static AntdCommunityGroupResponseDto MapToResponseDto(AntdCommunityGroup group)
        {
            return new AntdCommunityGroupResponseDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Image = group.Image,
                Category = group.Category,
                Location = group.Location,
                Size = group.Size,
                Leader = group.Leader,
                StartDate = group.StartDate,
                MeetingTime = group.MeetingTime,
                MemberAgeRange = group.MemberAgeRange,
                MemberInterests = group.MemberInterests,
                FavoriteColor = group.FavoriteColor,
                CreatedAt = group.CreatedAt,
                UpdatedAt = group.UpdatedAt
            };
        }
    }
}
