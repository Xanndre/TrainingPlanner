using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class ChatRepository : BaseRepository, IChatRepository
    {
        public ChatRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Chat> CreateChat(Chat chat)
        {
            await _trainingPlannerDbContext.Chats.AddAsync(chat);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return chat;
        }

        public async Task<Message> CreateMessage(Message message)
        {
            await _trainingPlannerDbContext.Messages.AddAsync(message);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<Chat>> GetAllChats(string userId)
        {
            return await GetChatQuery()
                .Include(c => c.Messages)
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int chatId)
        {
            return await GetMessageQuery()
                .Where(c => c.ChatId == chatId)
                .OrderByDescending(c => c.SentAt)
                .ToListAsync();
        }

        public async Task<Chat> GetChat(string senderId, string receiverId)
        {
            return await GetChatQuery()
                .Include(c => c.Messages)
                .FirstAsync(c => c.ReceiverId == receiverId && c.SenderId == senderId);
        }

        public async Task<Chat> GetChatById(int id)
        {
            return await GetChatQuery()
                .Include(c => c.Messages)
                .FirstAsync(c => c.Id == id);
        }

        private IQueryable<Chat> GetChatQuery()
        {
            return _trainingPlannerDbContext.Chats
                .Include(c => c.Receiver)
                .Include(c => c.Sender);
        }

        private IQueryable<Message> GetMessageQuery()
        {
            return _trainingPlannerDbContext.Messages;
        }

    }
}
