using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine")]
    [Tags("Mantine - Communication")]
    public class CommunicationController : MantineBaseController
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationController(ICommunicationService communicationService, ILogger<CommunicationController> logger)
            : base(logger)
        {
            _communicationService = communicationService;
        }

        /// <summary>
        /// Get all chats with pagination and filtering
        /// </summary>
        [HttpGet("chats")]
        public async Task<IActionResult> GetAllChats([FromQuery] ChatQueryParams queryParams)
        {
            try
            {
                var chats = await _communicationService.GetChatsAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = chats.Data,
                    message = "Chats retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = chats.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chats");
                return ErrorResponse("Failed to retrieve chats", 500);
            }
        }

        /// <summary>
        /// Get chat messages with pagination and filtering
        /// </summary>
        [HttpGet("chat-items")]
        public async Task<IActionResult> GetChatMessages([FromQuery] ChatMessageQueryParams queryParams)
        {
            try
            {
                var messages = await _communicationService.GetChatMessagesAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = messages.Data,
                    message = "Chat messages retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = messages.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat messages");
                return ErrorResponse("Failed to retrieve chat messages", 500);
            }
        }

        /// <summary>
        /// Get chat by ID
        /// </summary>
        [HttpGet("chats/{id}")]
        public async Task<IActionResult> GetChatById(string id)
        {
            try
            {
                var chat = await _communicationService.GetChatByIdAsync(id);
                if (chat == null)
                    return NotFound(new { success = false, message = "Chat not found" });

                return SuccessResponse(chat, "Chat retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat {ChatId}", id);
                return ErrorResponse("Failed to retrieve chat", 500);
            }
        }

        /// <summary>
        /// Get message by ID
        /// </summary>
        [HttpGet("chat-items/{id}")]
        public async Task<IActionResult> GetMessageById(string id)
        {
            try
            {
                var message = await _communicationService.GetMessageByIdAsync(id);
                if (message == null)
                    return NotFound(new { success = false, message = "Message not found" });

                return SuccessResponse(message, "Message retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving message {MessageId}", id);
                return ErrorResponse("Failed to retrieve message", 500);
            }
        }

        /// <summary>
        /// Create new chat
        /// </summary>
        [HttpPost("chats")]
        public async Task<IActionResult> CreateChat([FromBody] ChatDto chatDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var chat = await _communicationService.CreateChatAsync(chatDto);
                return SuccessResponse(chat, "Chat created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating chat");
                return ErrorResponse("Failed to create chat", 500);
            }
        }

        /// <summary>
        /// Create new message
        /// </summary>
        [HttpPost("chat-items")]
        public async Task<IActionResult> CreateMessage([FromBody] ChatMessageDto messageDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var message = await _communicationService.CreateMessageAsync(messageDto);
                return SuccessResponse(message, "Message created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message");
                return ErrorResponse("Failed to create message", 500);
            }
        }

        /// <summary>
        /// Update existing chat
        /// </summary>
        [HttpPut("chats/{id}")]
        public async Task<IActionResult> UpdateChat(string id, [FromBody] ChatDto chatDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var chat = await _communicationService.UpdateChatAsync(id, chatDto);
                if (chat == null)
                    return NotFound(new { success = false, message = "Chat not found" });

                return SuccessResponse(chat, "Chat updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating chat {ChatId}", id);
                return ErrorResponse("Failed to update chat", 500);
            }
        }

        /// <summary>
        /// Delete chat
        /// </summary>
        [HttpDelete("chats/{id}")]
        public async Task<IActionResult> DeleteChat(string id)
        {
            try
            {
                var deleted = await _communicationService.DeleteChatAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Chat not found" });

                return SuccessResponse(new { }, "Chat deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting chat {ChatId}", id);
                return ErrorResponse("Failed to delete chat", 500);
            }
        }

        /// <summary>
        /// Delete message
        /// </summary>
        [HttpDelete("chat-items/{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            try
            {
                var deleted = await _communicationService.DeleteMessageAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Message not found" });

                return SuccessResponse(new { }, "Message deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting message {MessageId}", id);
                return ErrorResponse("Failed to delete message", 500);
            }
        }
    }
}