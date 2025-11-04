using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CommunicationService> _logger;

        public CommunicationService(ApplicationDbContext context, ILogger<CommunicationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<ChatDto>>> GetChatsAsync(ChatQueryParams queryParams)
        {
            try
            {
                var query = _context.Chats.AsQueryable();

                // Apply filters
                if (queryParams.Type.HasValue)
                {
                    query = query.Where(c => c.Type == queryParams.Type.Value);
                }

                var total = await query.CountAsync();
                var chats = await query
                    .OrderByDescending(c => c.LastMessageAt ?? c.CreatedAt)
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var chatsDto = chats.Select(c => new ChatDto
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Type = c.Type,
                    ParticipantCount = c.ParticipantCount,
                    LastMessage = c.LastMessage,
                    LastMessageAt = c.LastMessageAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    CreatedAt = c.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                }).ToList();

                return new ApiResponse<List<ChatDto>>
                {
                    Data = chatsDto,
                    Meta = new PaginationMeta
                    {
                        Page = queryParams.Page,
                        Limit = queryParams.Limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chats");
                throw;
            }
        }

        public async Task<ApiResponse<List<ChatMessageDto>>> GetChatMessagesAsync(ChatMessageQueryParams queryParams)
        {
            try
            {
                var query = _context.ChatMessages.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.ChatId))
                {
                    if (Guid.TryParse(queryParams.ChatId, out var chatGuid))
                    {
                        query = query.Where(cm => cm.ChatId == chatGuid);
                    }
                }

                if (!string.IsNullOrEmpty(queryParams.SenderId))
                {
                    if (Guid.TryParse(queryParams.SenderId, out var senderGuid))
                    {
                        query = query.Where(cm => cm.SenderId == senderGuid);
                    }
                }

                if (queryParams.MessageType.HasValue)
                {
                    query = query.Where(cm => cm.MessageType == queryParams.MessageType.Value);
                }

                var total = await query.CountAsync();
                var messages = await query
                    .OrderByDescending(cm => cm.CreatedAt)
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var messagesDto = messages.Select(cm => new ChatMessageDto
                {
                    Id = cm.Id.ToString(),
                    ChatId = cm.ChatId.ToString(),
                    SenderId = cm.SenderId.ToString(),
                    Content = cm.Content,
                    MessageType = cm.MessageType,
                    CreatedAt = cm.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                }).ToList();

                return new ApiResponse<List<ChatMessageDto>>
                {
                    Data = messagesDto,
                    Meta = new PaginationMeta
                    {
                        Page = queryParams.Page,
                        Limit = queryParams.Limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat messages");
                throw;
            }
        }

        public async Task<ChatDto?> GetChatByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var chat = await _context.Chats.FindAsync(guidId);
                if (chat == null) return null;

                return new ChatDto
                {
                    Id = chat.Id.ToString(),
                    Name = chat.Name,
                    Type = chat.Type,
                    ParticipantCount = chat.ParticipantCount,
                    LastMessage = chat.LastMessage,
                    LastMessageAt = chat.LastMessageAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    CreatedAt = chat.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat with ID {ChatId}", id);
                throw;
            }
        }

        public async Task<ChatMessageDto?> GetMessageByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var message = await _context.ChatMessages.FindAsync(guidId);
                if (message == null) return null;

                return new ChatMessageDto
                {
                    Id = message.Id.ToString(),
                    ChatId = message.ChatId.ToString(),
                    SenderId = message.SenderId.ToString(),
                    Content = message.Content,
                    MessageType = message.MessageType,
                    CreatedAt = message.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving message with ID {MessageId}", id);
                throw;
            }
        }

        public async Task<ChatDto> CreateChatAsync(ChatDto chatDto)
        {
            try
            {
                var chat = new Chats
                {
                    Id = Guid.NewGuid(),
                    Name = chatDto.Name,
                    Type = chatDto.Type,
                    ParticipantCount = chatDto.ParticipantCount,
                    LastMessage = chatDto.LastMessage,
                    LastMessageAt = string.IsNullOrEmpty(chatDto.LastMessageAt) ? null : DateTime.Parse(chatDto.LastMessageAt),
                    CreatedAt = DateTime.UtcNow
                };

                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();

                chatDto.Id = chat.Id.ToString();
                chatDto.CreatedAt = chat.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                return chatDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new chat");
                throw;
            }
        }

        public async Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto messageDto)
        {
            try
            {
                var message = new ChatMessages
                {
                    Id = Guid.NewGuid(),
                    ChatId = Guid.Parse(messageDto.ChatId),
                    SenderId = Guid.Parse(messageDto.SenderId),
                    Content = messageDto.Content,
                    MessageType = messageDto.MessageType,
                    CreatedAt = DateTime.UtcNow
                };

                _context.ChatMessages.Add(message);

                // Update the chat's last message
                var chat = await _context.Chats.FindAsync(message.ChatId);
                if (chat != null)
                {
                    chat.LastMessage = message.Content.Length > 50
                        ? message.Content.Substring(0, 50) + "..."
                        : message.Content;
                    chat.LastMessageAt = message.CreatedAt;
                }

                await _context.SaveChangesAsync();

                messageDto.Id = message.Id.ToString();
                messageDto.CreatedAt = message.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                return messageDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new message");
                throw;
            }
        }

        public async Task<ChatDto?> UpdateChatAsync(string id, ChatDto chatDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var chat = await _context.Chats.FindAsync(guidId);
                if (chat == null) return null;

                chat.Name = chatDto.Name;
                chat.Type = chatDto.Type;
                chat.ParticipantCount = chatDto.ParticipantCount;

                await _context.SaveChangesAsync();

                return chatDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating chat with ID {ChatId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteChatAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var chat = await _context.Chats.FindAsync(guidId);
                if (chat == null) return false;

                // Also delete all messages in this chat
                var messages = await _context.ChatMessages.Where(cm => cm.ChatId == guidId).ToListAsync();
                _context.ChatMessages.RemoveRange(messages);

                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting chat with ID {ChatId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteMessageAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var message = await _context.ChatMessages.FindAsync(guidId);
                if (message == null) return false;

                _context.ChatMessages.Remove(message);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting message with ID {MessageId}", id);
                throw;
            }
        }
    }
}