using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class CardService : ICardService
    {
        private const int MaxPageSize = 20;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<TrainerCardUpdateDTO> UpdateTrainerCard(TrainerCardUpdateDTO card, bool isDeactivating)
        {
            var mappedCard = _mapper.Map<TrainerCard>(card);
            if (!isDeactivating)
            {
                if (!card.UnlimitedValidityPeriod)
                {
                    mappedCard.ExpirationDate = mappedCard.PurchaseDate.AddDays(card.ValidityPeriod);
                }
            }
            else
            {
                mappedCard.ExpirationDate = DateTime.Now;
            }
            var returnedCard = await _cardRepository.UpdateTrainerCard(mappedCard);
            return _mapper.Map<TrainerCardUpdateDTO>(returnedCard);
        }

        public async Task<TrainerCardCreateDTO> CreateTrainerCard(TrainerCardCreateDTO card)
        {
            var mappedCard = _mapper.Map<TrainerCard>(card);
            var days = !card.UnlimitedValidityPeriod ? card.ValidityPeriod : 0;
            mappedCard.PurchaseDate = DateTime.Now;
            if (!card.UnlimitedValidityPeriod)
            {
                mappedCard.ExpirationDate = DateTime.Now.AddDays(days);
            }
            var returnedCard = await _cardRepository.CreateTrainerCard(mappedCard);
            return _mapper.Map<TrainerCardCreateDTO>(returnedCard);
        }

        public async Task DeleteTrainerCard(int id)
        {
            var card = await _cardRepository.GetTrainerCard(id);
            await _cardRepository.DeleteTrainerCard(card);
        }

        public async Task<TrainerCardDTO> GetTrainerCard(int id)
        {
            var card = await _cardRepository.GetTrainerCard(id);
            return _mapper.Map<TrainerCardDTO>(card);
        }

        public async Task<PagedTrainerCardsDTO> GetUserTrainerCards(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var cards = await _cardRepository.GetUserTrainerCards(userId);
            var result = GetTrainerCards(pageNumber, pageSize, cards);
            return result;
        }

        public async Task<PagedTrainerCardsDTO> GetTrainerTrainerCards(
            int pageNumber,
            int pageSize,
            int trainerId)
        {
            var cards = await _cardRepository.GetTrainerTrainerCards(trainerId);
            var result = GetTrainerCards(pageNumber, pageSize, cards);
            return result;
        }

        public async Task<PagedTrainerCardsDTO> GetTrainerCards(
            int pageNumber,
            int pageSize,
            string userId,
            int trainerId)
        {
            var cards = await _cardRepository.GetTrainerCards(userId, trainerId);
            var result = GetTrainerCards(pageNumber, pageSize, cards);
            return result;
        }

        public async Task<ClubCardUpdateDTO> UpdateClubCard(ClubCardUpdateDTO card, bool isDeactivating)
        {
            var mappedCard = _mapper.Map<ClubCard>(card);
            if (!isDeactivating)
            {
                if (!card.UnlimitedValidityPeriod)
                {
                    mappedCard.ExpirationDate = mappedCard.PurchaseDate.AddDays(card.ValidityPeriod);
                } 
            }
            else
            {
                mappedCard.ExpirationDate = DateTime.Now;
            }
            var returnedCard = await _cardRepository.UpdateClubCard(mappedCard);
            return _mapper.Map<ClubCardUpdateDTO>(returnedCard);
        }

        public async Task<ClubCardCreateDTO> CreateClubCard(ClubCardCreateDTO card)
        {
            var mappedCard = _mapper.Map<ClubCard>(card);
            var days = !card.UnlimitedValidityPeriod ? card.ValidityPeriod : 0; 
            mappedCard.PurchaseDate = DateTime.Now;
            if (!card.UnlimitedValidityPeriod)
            {
                mappedCard.ExpirationDate = DateTime.Now.AddDays(days);
            }
            var returnedCard = await _cardRepository.CreateClubCard(mappedCard);
            return _mapper.Map<ClubCardCreateDTO>(returnedCard);
        }

        public async Task DeleteClubCard(int id)
        {
            var card = await _cardRepository.GetClubCard(id);
            await _cardRepository.DeleteClubCard(card);
        }

        public async Task<ClubCardDTO> GetClubCard(int id)
        {
            var card = await _cardRepository.GetClubCard(id);
            return _mapper.Map<ClubCardDTO>(card);
        }

        public async Task<PagedClubCardsDTO> GetUserClubCards(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var cards = await _cardRepository.GetUserClubCards(userId);
            var result = GetClubCards(pageNumber, pageSize, cards);
            return result;
        }

        public async Task<PagedClubCardsDTO> GetClubClubCards(
            int pageNumber,
            int pageSize,
            int clubId)
        {
            var cards = await _cardRepository.GetClubClubCards(clubId);
            var result = GetClubCards(pageNumber, pageSize, cards);
            return result;
        }

        public async Task<PagedClubCardsDTO> GetClubCards(
            int pageNumber,
            int pageSize,
            string userId,
            int clubId)
        {
            var cards = await _cardRepository.GetClubCards(userId, clubId);
            var result = GetClubCards(pageNumber, pageSize, cards);
            return result;
        }

        private PagedTrainerCardsDTO GetTrainerCards(
            int pageNumber, int pageSize, IEnumerable<TrainerCard> cards)
        {
            var trainerCards = cards.OrderByDescending(u => u.PurchaseDate);
            var result = GetPagedTrainerCards(trainerCards, pageNumber, pageSize);
            return result;
        }

        private PagedTrainerCardsDTO GetPagedTrainerCards(IEnumerable<TrainerCard> cards, int pageNumber, int pageSize)
        {
            var result = new PagedTrainerCardsDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedCards = cards
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = cards.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Cards = _mapper.Map<IEnumerable<TrainerCardBaseDTO>>(pagedCards);

            return result;
        }

        private PagedClubCardsDTO GetClubCards(
            int pageNumber, int pageSize, IEnumerable<ClubCard> cards)
        {
            var clubCards = cards.OrderByDescending(u => u.PurchaseDate);
            var result = GetPagedClubCards(clubCards, pageNumber, pageSize);
            return result;
        }

        private PagedClubCardsDTO GetPagedClubCards(IEnumerable<ClubCard> cards, int pageNumber, int pageSize)
        {
            var result = new PagedClubCardsDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedCards = cards
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = cards.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Cards = _mapper.Map<IEnumerable<ClubCardBaseDTO>>(pagedCards);

            return result;
        }
    }
}
