using MantineAdmin.Data;
using MantineAdmin.Dtos.ProjectComment;
using MantineAdmin.Helpers;
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

    public async Task<List<ProjectComment>> GetAllAsync(QueryObject query)
    {
        var comments = _context.ProjectComments.Include(c => c.AppUser).AsQueryable();

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await comments.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<ProjectComment?> GetByIdAsync(int id)
    {
        return await _context.ProjectComments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<ProjectComment?> UpdateAsync(int id, ProjectComment commentDto)
    {
        var existingComment = await _context.ProjectComments.FindAsync(id);

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