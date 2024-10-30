using MantineAdmin.Data;
using MantineAdmin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MantineAdmin.Repository;

public class ProjectCommentRepository : IProjectCommentRepository
{
    private readonly ApplicationDBContext _context;

    public ProjectCommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProjectComment>> GetAllAsync()
    {
        return await _context.ProjectComments.ToListAsync();
    }

    public Task<ProjectComment?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}