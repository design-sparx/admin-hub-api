using MantineAdmin.Data;
using MantineAdmin.Dtos.ProjectComment;
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

    public async Task<ProjectComment?> GetByIdAsync(int id)
    {
        return await _context.ProjectComments.FindAsync(id);
    }

    public async Task<ProjectComment> CreateAsync(ProjectComment commentModel)
    {
        await _context.ProjectComments.AddAsync(commentModel);
        await _context.SaveChangesAsync();

        return commentModel;
    }

    public async Task<ProjectComment?> DeleteAsync(int id)
    {
        var commentModel = await _context.ProjectComments.FirstOrDefaultAsync(x => x.Id == id);

        if (commentModel == null)
        {
            return null;
        }

        _context.ProjectComments.Remove(commentModel);

        await _context.SaveChangesAsync();

        return commentModel;
    }

    public async Task<ProjectComment?> UpdateAsync(int id, UpdateProjectCommentDto commentDto)
    {
        var existingComment = await _context.ProjectComments.FirstOrDefaultAsync(x => x.Id == id);

        if (existingComment == null)
        {
            return null;
        }

        existingComment.Title = commentDto.Title;
        existingComment.Content = commentDto.Content;

        await _context.SaveChangesAsync();

        return existingComment;
    }
}