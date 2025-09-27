using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IFileManagementService
    {
        Task<ApiResponse<List<FileDto>>> GetFilesAsync(FileQueryParams queryParams);
        Task<ApiResponse<List<FolderDto>>> GetFoldersAsync(FolderQueryParams queryParams);
        Task<ApiResponse<List<FileActivityDto>>> GetFileActivitiesAsync(int page = 1, int limit = 20);
        Task<FileDto?> GetFileByIdAsync(string id);
        Task<FolderDto?> GetFolderByIdAsync(string id);
        Task<FileDto> CreateFileAsync(FileDto fileDto);
        Task<FolderDto> CreateFolderAsync(FolderDto folderDto);
        Task<FileDto?> UpdateFileAsync(string id, FileDto fileDto);
        Task<FolderDto?> UpdateFolderAsync(string id, FolderDto folderDto);
        Task<bool> DeleteFileAsync(string id);
        Task<bool> DeleteFolderAsync(string id);
    }
}