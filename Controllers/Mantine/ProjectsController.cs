using AdminHubApi.Constants;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/projects")]
    [Tags("Mantine - Projects")]
    [PermissionAuthorize(Permissions.Team.Projects)]
    public class ProjectsController : MantineBaseController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
            : base(logger)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get all projects with pagination and filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ProjectListResponse), 200)]
        public async Task<IActionResult> GetAllProjects([FromQuery] ProjectQueryParams queryParams)
        {
            try
            {
                var response = await _projectService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects");
                return ErrorResponse("Failed to retrieve projects", 500);
            }
        }

        /// <summary>
        /// Get project by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
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
                _logger.LogError(ex, "Error retrieving project {ProjectId}", id);
                return ErrorResponse("Failed to retrieve project", 500);
            }
        }

        /// <summary>
        /// Create new project
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectCreateResponse), 201)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
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
                _logger.LogError(ex, "Error creating project");
                return ErrorResponse("Failed to create project", 500);
            }
        }

        /// <summary>
        /// Update existing project
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProjectUpdateResponse), 200)]
        public async Task<IActionResult> UpdateProject(string id, [FromBody] ProjectDto projectDto)
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
                _logger.LogError(ex, "Error updating project {ProjectId}", id);
                return ErrorResponse("Failed to update project", 500);
            }
        }

        /// <summary>
        /// Delete project
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProjectDeleteResponse), 200)]
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
                _logger.LogError(ex, "Error deleting project {ProjectId}", id);
                return ErrorResponse("Failed to delete project", 500);
            }
        }
    }
}