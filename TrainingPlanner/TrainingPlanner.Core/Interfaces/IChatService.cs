using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Chat;
using TrainingPlanner.Core.DTOs.Paged;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IChatService
    {
        Task<ChatDTO> CreateChat(ChatCreateDTO chat);
        Task<MessageDTO> CreateMessage(MessageBaseDTO message);
        Task<IEnumerable<ChatDTO>> GetAllChats(string userId);
        Task<PagedMessagesDTO> GetAllMessages(int pageNumber, int pageSize, int chatId);
        Task<ChatDTO> GetChat(string senderId, string receiverId);
        Task<ChatDTO> GetChatById(int id);
    }
}
