using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine")]
    [Tags("Mantine - File Management")]
    public class FileManagementController : MantineBaseController
    {
        private readonly IFileManagementService _fileManagementService;

        public FileManagementController(IFileManagementService fileManagementService, ILogger<FileManagementController> logger)
            : base(logger)
        {
            _fileManagementService = fileManagementService;
        }

        /// <summary>
        /// Get all files with pagination and filtering
        /// </summary>
        [HttpGet("files")]
        public async Task<IActionResult> GetAllFiles([FromQuery] FileQueryParams queryParams)
        {
            try
            {
                var files = await _fileManagementService.GetFilesAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = files.Data,
                    message = "Files retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = files.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving files");
                return ErrorResponse("Failed to retrieve files", 500);
            }
        }

        /// <summary>
        /// Get all folders with pagination and filtering
        /// </summary>
        [HttpGet("folders")]
        public async Task<IActionResult> GetAllFolders([FromQuery] FolderQueryParams queryParams)
        {
            try
            {
                var folders = await _fileManagementService.GetFoldersAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = folders.Data,
                    message = "Folders retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = folders.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folders");
                return ErrorResponse("Failed to retrieve folders", 500);
            }
        }

        /// <summary>
        /// Get file activities
        /// </summary>
        [HttpGet("file-activities")]
        public async Task<IActionResult> GetFileActivities([FromQuery] int page = 1, [FromQuery] int limit = 20)
        {
            try
            {
                var activities = await _fileManagementService.GetFileActivitiesAsync(page, limit);
                return Ok(new
                {
                    success = true,
                    data = activities.Data,
                    message = "File activities retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = activities.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file activities");
                return ErrorResponse("Failed to retrieve file activities", 500);
            }
        }

        /// <summary>
        /// Get file by ID
        /// </summary>
        [HttpGet("files/{id}")]
        public async Task<IActionResult> GetFileById(string id)
        {
            try
            {
                var file = await _fileManagementService.GetFileByIdAsync(id);
                if (file == null)
                    return NotFound(new { success = false, message = "File not found" });

                return SuccessResponse(file, "File retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file {FileId}", id);
                return ErrorResponse("Failed to retrieve file", 500);
            }
        }

        /// <summary>
        /// Get folder by ID
        /// </summary>
        [HttpGet("folders/{id}")]
        public async Task<IActionResult> GetFolderById(string id)
        {
            try
            {
                var folder = await _fileManagementService.GetFolderByIdAsync(id);
                if (folder == null)
                    return NotFound(new { success = false, message = "Folder not found" });

                return SuccessResponse(folder, "Folder retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder {FolderId}", id);
                return ErrorResponse("Failed to retrieve folder", 500);
            }
        }

        /// <summary>
        /// Create new file
        /// </summary>
        [HttpPost("files")]
        public async Task<IActionResult> CreateFile([FromBody] FileDto fileDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var file = await _fileManagementService.CreateFileAsync(fileDto);
                return SuccessResponse(file, "File created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating file");
                return ErrorResponse("Failed to create file", 500);
            }
        }

        /// <summary>
        /// Create new folder
        /// </summary>
        [HttpPost("folders")]
        public async Task<IActionResult> CreateFolder([FromBody] FolderDto folderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var folder = await _fileManagementService.CreateFolderAsync(folderDto);
                return SuccessResponse(folder, "Folder created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating folder");
                return ErrorResponse("Failed to create folder", 500);
            }
        }

        /// <summary>
        /// Update existing file
        /// </summary>
        [HttpPut("files/{id}")]
        public async Task<IActionResult> UpdateFile(string id, [FromBody] FileDto fileDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var file = await _fileManagementService.UpdateFileAsync(id, fileDto);
                if (file == null)
                    return NotFound(new { success = false, message = "File not found" });

                return SuccessResponse(file, "File updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating file {FileId}", id);
                return ErrorResponse("Failed to update file", 500);
            }
        }

        /// <summary>
        /// Update existing folder
        /// </summary>
        [HttpPut("folders/{id}")]
        public async Task<IActionResult> UpdateFolder(string id, [FromBody] FolderDto folderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var folder = await _fileManagementService.UpdateFolderAsync(id, folderDto);
                if (folder == null)
                    return NotFound(new { success = false, message = "Folder not found" });

                return SuccessResponse(folder, "Folder updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating folder {FolderId}", id);
                return ErrorResponse("Failed to update folder", 500);
            }
        }

        /// <summary>
        /// Delete file
        /// </summary>
        [HttpDelete("files/{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            try
            {
                var deleted = await _fileManagementService.DeleteFileAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "File not found" });

                return SuccessResponse(new { }, "File deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file {FileId}", id);
                return ErrorResponse("Failed to delete file", 500);
            }
        }

        /// <summary>
        /// Delete folder
        /// </summary>
        [HttpDelete("folders/{id}")]
        public async Task<IActionResult> DeleteFolder(string id)
        {
            try
            {
                var deleted = await _fileManagementService.DeleteFolderAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Folder not found" });

                return SuccessResponse(new { }, "Folder deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting folder {FolderId}", id);
                return ErrorResponse("Failed to delete folder", 500);
            }
        }
    }
}