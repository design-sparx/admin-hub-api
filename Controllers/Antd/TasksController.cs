using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/tasks")]
    [Tags("Antd - Tasks")]
    [Authorize]
    public class TasksController : AntdBaseController
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
            : base(logger)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get all tasks with filtering and pagination
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskQueryParams queryParams)
        {
            try
            {
                var tasks = await _taskService.GetAllAsync(queryParams);
                return SuccessResponse(tasks.Data, "Tasks retrieved successfully", tasks.Meta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks");
                return ErrorResponse("Failed to retrieve tasks", 500);
            }
        }

        /// <summary>
        /// Get a task by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                    return ErrorResponse("Task not found", 404);

                return SuccessResponse(task, "Task retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task {TaskId}", id);
                return ErrorResponse("Failed to retrieve task", 500);
            }
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var task = await _taskService.CreateAsync(taskDto);
                return SuccessResponse(task, "Task created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return ErrorResponse("Failed to create task", 500);
            }
        }

        /// <summary>
        /// Update an existing task
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(string id, [FromBody] TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var task = await _taskService.UpdateAsync(id, taskDto);
                if (task == null)
                    return ErrorResponse("Task not found", 404);

                return SuccessResponse(task, "Task updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {TaskId}", id);
                return ErrorResponse("Failed to update task", 500);
            }
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                var deleted = await _taskService.DeleteAsync(id);
                if (!deleted)
                    return ErrorResponse("Task not found", 404);

                return SuccessResponse(new { }, "Task deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {TaskId}", id);
                return ErrorResponse("Failed to delete task", 500);
            }
        }
    }
}
