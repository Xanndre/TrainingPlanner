using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Chat;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ChatService : IChatService
    {
        private const int MaxPageSize = 20;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<ChatDTO> GetChat(string senderId, string receiverId)
        {
            var chat = await _chatRepository.GetChat(senderId, receiverId);
            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<ChatDTO> GetChatById(int id)
        {
            var chat = await _chatRepository.GetChatById(id);
            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<ChatDTO> CreateChat(ChatCreateDTO chat)
        {
            var mappedChat = _mapper.Map<Chat>(chat);
            var createdChat = await _chatRepository.CreateChat(mappedChat);

            var returnedChat = _mapper.Map<ChatDTO>(createdChat);
            return returnedChat;
        }

        public async Task<MessageDTO> CreateMessage(MessageBaseDTO message)
        {
            var mappedMessage = _mapper.Map<Message>(message);
            var returnedMessage = await _chatRepository.CreateMessage(mappedMessage);
            return _mapper.Map<MessageDTO>(returnedMessage);
        }

        public async Task<IEnumerable<ChatDTO>> GetAllChats(string userId)
        {
            var chats = await _chatRepository.GetAllChats(userId);
            return _mapper.Map<IEnumerable<ChatDTO>>(chats);
        }

        public async Task<PagedMessagesDTO> GetAllMessages(
            int pageNumber,
            int pageSize,
            int chatId)
        {
            var messages = await _chatRepository.GetAllMessages(chatId);
            var result = GetMessages(pageNumber, pageSize, messages);
            return result;
        }

        private PagedMessagesDTO GetMessages(
            int pageNumber, int pageSize, IEnumerable<Message> messages)
        {
            var result = GetPagedMessages(messages, pageNumber, pageSize);
            return result;
        }

        private PagedMessagesDTO GetPagedMessages(IEnumerable<Message> messages, int pageNumber, int pageSize)
        {
            var result = new PagedMessagesDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedMessages = messages
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = messages.Count();
            result.CurrentPage = pageNumber;
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Messages = _mapper.Map<IEnumerable<MessageDTO>>(pagedMessages);

            return result;
        }
    }
}
