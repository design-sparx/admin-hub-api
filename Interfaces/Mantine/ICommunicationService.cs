using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface ICommunicationService
    {
        Task<ApiResponse<List<ChatDto>>> GetChatsAsync(ChatQueryParams queryParams);
        Task<ApiResponse<List<ChatMessageDto>>> GetChatMessagesAsync(ChatMessageQueryParams queryParams);
        Task<ChatDto?> GetChatByIdAsync(string id);
        Task<ChatMessageDto?> GetMessageByIdAsync(string id);
        Task<ChatDto> CreateChatAsync(ChatDto chatDto);
        Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto messageDto);
        Task<ChatDto?> UpdateChatAsync(string id, ChatDto chatDto);
        Task<bool> DeleteChatAsync(string id);
        Task<bool> DeleteMessageAsync(string id);
    }
}