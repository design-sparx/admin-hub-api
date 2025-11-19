using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/projects")]
    [Tags("Antd - Projects")]
    [PermissionAuthorize(Permissions.Antd.Projects)]
    public class AntdProjectsController : AntdBaseController
    {
        private readonly IAntdProjectService _projectService;

        public AntdProjectsController(IAntdProjectService projectService, ILogger<AntdProjectsController> logger)
            : base(logger)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get all projects with pagination and filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(AntdProjectListResponse), 200)]
        public async Task<IActionResult> GetAllProjects([FromQuery] AntdProjectQueryParams queryParams)
        {
            try
            {
                var response = await _projectService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd projects");
                return ErrorResponse("Failed to retrieve projects", 500);
            }
        }

        /// <summary>
        /// Get project by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdProjectResponse), 200)]
        public async Task<IActionResult> GetProjectById(string id)
        {
            try
            {
                var response = await _projectService.GetByIdAsync(id);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd project {ProjectId}", id);
                return ErrorResponse("Failed to retrieve project", 500);
            }
        }

        /// <summary>
        /// Create new project
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AntdProjectCreateResponse), 201)]
        public async Task<IActionResult> CreateProject([FromBody] AntdProjectDto projectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _projectService.CreateAsync(projectDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd project");
                return ErrorResponse("Failed to create project", 500);
            }
        }

        /// <summary>
        /// Update existing project
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdProjectUpdateResponse), 200)]
        public async Task<IActionResult> UpdateProject(string id, [FromBody] AntdProjectDto projectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _projectService.UpdateAsync(id, projectDto);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd project {ProjectId}", id);
                return ErrorResponse("Failed to update project", 500);
            }
        }

        /// <summary>
        /// Delete project
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdProjectDeleteResponse), 200)]
        public async Task<IActionResult> DeleteProject(string id)
        {
            try
            {
                var response = await _projectService.DeleteAsync(id);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd project {ProjectId}", id);
                return ErrorResponse("Failed to delete project", 500);
            }
        }
    }
}
