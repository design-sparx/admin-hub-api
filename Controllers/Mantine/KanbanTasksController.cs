using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/kanban-tasks")]
    [Tags("Mantine - Kanban")]
    public class KanbanTasksController : MantineBaseController
    {
        private readonly IKanbanTaskService _kanbanTaskService;

        public KanbanTasksController(IKanbanTaskService kanbanTaskService, ILogger<KanbanTasksController> logger)
            : base(logger)
        {
            _kanbanTaskService = kanbanTaskService;
        }

        /// <summary>
        /// Get all kanban tasks with pagination and filtering
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllKanbanTasks([FromQuery] KanbanTaskQueryParams queryParams)
        {
            try
            {
                var tasks = await _kanbanTaskService.GetAllAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = tasks.Data,
                    message = "Kanban tasks retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = tasks.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanban tasks");
                return ErrorResponse("Failed to retrieve kanban tasks", 500);
            }
        }

        /// <summary>
        /// Get kanban task by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKanbanTaskById(string id)
        {
            try
            {
                var task = await _kanbanTaskService.GetByIdAsync(id);
                if (task == null)
                    return NotFound(new { success = false, message = "Kanban task not found" });

                return SuccessResponse(task, "Kanban task retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanban task {TaskId}", id);
                return ErrorResponse("Failed to retrieve kanban task", 500);
            }
        }

        /// <summary>
        /// Create new kanban task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateKanbanTask([FromBody] KanbanTaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var task = await _kanbanTaskService.CreateAsync(taskDto);
                return SuccessResponse(task, "Kanban task created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanban task");
                return ErrorResponse("Failed to create kanban task", 500);
            }
        }

        /// <summary>
        /// Update existing kanban task
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKanbanTask(string id, [FromBody] KanbanTaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var task = await _kanbanTaskService.UpdateAsync(id, taskDto);
                if (task == null)
                    return NotFound(new { success = false, message = "Kanban task not found" });

                return SuccessResponse(task, "Kanban task updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanban task {TaskId}", id);
                return ErrorResponse("Failed to update kanban task", 500);
            }
        }

        /// <summary>
        /// Delete kanban task
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanbanTask(string id)
        {
            try
            {
                var deleted = await _kanbanTaskService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Kanban task not found" });

                return SuccessResponse(new { }, "Kanban task deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanban task {TaskId}", id);
                return ErrorResponse("Failed to delete kanban task", 500);
            }
        }
    }
}