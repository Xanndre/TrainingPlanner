using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class TrainerService : ITrainerService
    {
        private const int MaxPageSize = 20;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<TrainerDTO> GetTrainer(int id)
        {
            var trainer = await _trainerRepository.GetTrainer(id);
            return _mapper.Map<TrainerDTO>(trainer);
        }

        public async Task<TrainerDTO> GetTrainerByUser(string userId)
        {
            var trainer = await _trainerRepository.GetTrainerByUser(userId);
            return _mapper.Map<TrainerDTO>(trainer);
        }

        public async Task<TrainerUpdateDTO> UpdateTrainer(TrainerUpdateDTO trainer)
        {
            var mappedTrainer = _mapper.Map<Trainer>(trainer);

            await RemoveTrainerSports(mappedTrainer);
            await RemoveTrainerPrices(mappedTrainer);

            var returnedTrainer = await _trainerRepository.UpdateTrainer(mappedTrainer);
            return _mapper.Map<TrainerUpdateDTO>(returnedTrainer);
        }

        public async Task<TrainerCreateDTO> CreateTrainer(TrainerCreateDTO trainer)
        {
            var mappedTrainer = _mapper.Map<Trainer>(trainer);
            var returnedTrainer = await _trainerRepository.CreateTrainer(mappedTrainer);
            return _mapper.Map<TrainerCreateDTO>(returnedTrainer);
        }

        public async Task DeleteTrainer(int id)
        {
            var trainer = await _trainerRepository.GetTrainer(id);
            await _trainerRepository.DeleteTrainer(trainer);
        }

        public async Task<PagedTrainersDTO> GetFavouriteTrainers(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var trainers = await _trainerRepository.GetFavouriteTrainers(userId);
            var result = GetTrainers(pageNumber, pageSize, trainers);
            return result;
        }

        public async Task<PagedTrainersDTO> GetAllTrainers(
            int pageNumber,
            int pageSize,
            string userId)
        {
            IEnumerable<Trainer> trainers;

            if (!string.IsNullOrEmpty(userId))
            {
                trainers = await _trainerRepository.GetAllTrainers(userId);
            }
            else
            {
                trainers = _trainerRepository.GetAllTrainers();
            }

            var result = GetTrainers(pageNumber, pageSize, trainers);
            return result;
        }

        private async Task RemoveTrainerSports(Trainer mappedTrainer)
        {
            var trainerSportsToDelete = await _trainerRepository.GetTrainerSportsToDelete(mappedTrainer);
            await _trainerRepository.RemoveTrainerSports(trainerSportsToDelete, false);
        }

        private async Task RemoveTrainerPrices(Trainer mappedTrainer)
        {
            var trainerPricesToDelete = await _trainerRepository.GetTrainerPricesToDelete(mappedTrainer);
            await _trainerRepository.RemoveTrainerPrices(trainerPricesToDelete, false);
        }

        private PagedTrainersDTO GetTrainers(
            int pageNumber, int pageSize, IEnumerable<Trainer> trainers)
        {
            var result = GetPagedTrainers(trainers, pageNumber, pageSize);
            return result;
        }

        private PagedTrainersDTO GetPagedTrainers(IEnumerable<Trainer> trainers, int pageNumber, int pageSize)
        {
            var result = new PagedTrainersDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedTrainers = trainers
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = trainers.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Trainers = _mapper.Map<IEnumerable<TrainerBaseDTO>>(pagedTrainers);

            return result;
        }

    }
}
