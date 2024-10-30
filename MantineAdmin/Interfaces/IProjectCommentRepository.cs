namespace MantineAdmin.Interfaces;

public interface IProjectCommentRepository
{
    Task<List<ProjectComment>> GetAllAsync();
    Task<ProjectComment?> GetByIdAsync(int id);
}