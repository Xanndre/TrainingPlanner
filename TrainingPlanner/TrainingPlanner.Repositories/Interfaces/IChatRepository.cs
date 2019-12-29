using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> CreateChat(Chat chat);
        Task<Message> CreateMessage(Message message);
        Task<IEnumerable<Chat>> GetAllChats(string userId);
        Task<IEnumerable<Message>> GetAllMessages(int chatId);
        Task<Chat> GetChat(string senderId, string receiverId);
        Task<Chat> GetChatById(int id);
        Task DeleteChat(Chat chat);
    }
}
