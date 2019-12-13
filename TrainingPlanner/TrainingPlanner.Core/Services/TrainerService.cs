using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.Trainer;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Specifications.Extensions;
using TrainingPlanner.Core.Specifications.Filters.TrainerFilters;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class TrainerService : ITrainerService
    {
        private const int MaxPageSize = 20;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper, IRateRepository rateRepository,
                                IFavouriteRepository favouriteRepository, ITrainingRepository trainingRepository)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
            _rateRepository = rateRepository;
            _favouriteRepository = favouriteRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<TrainerDTO> GetTrainer(int id, bool isIncrementingViewCounter)
        {
            var trainer = await _trainerRepository.GetTrainer(id);
            if (isIncrementingViewCounter)
            {
                trainer.ViewCounter++;
            }
            var updatedTrainer = await _trainerRepository.UpdateTrainer(trainer);
            var mappedTrainer = _mapper.Map<TrainerDTO>(updatedTrainer);
            mappedTrainer.Average = await CalculateAverageTrainerRate(mappedTrainer.Id);
            return mappedTrainer;
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
            var rates = await _rateRepository.GetTrainerRates(id);
            var favs = await _favouriteRepository.GetFavouriteTrainers(id);
            var trainings = await _trainingRepository.GetTrainerTrainings(id);

            if (favs.Count() != 0)
            {
                foreach (var fav in favs)
                {
                    await _favouriteRepository.DeleteFavouriteTrainer(fav);
                }
            }

            if (rates.Count() != 0)
            {
                foreach (var rate in rates)
                {
                    await _rateRepository.DeleteTrainerRate(rate);
                }
            }

            if (trainings.Count() != 0)
            {
                foreach (var training in trainings)
                {
                    await _trainingRepository.DeleteTraining(training);
                }
            }

            var trainer = await _trainerRepository.GetTrainer(id);
            await _trainerRepository.DeleteTrainer(trainer);
        }

        public async Task<PagedTrainersDTO> GetFavouriteTrainers(
            int pageNumber,
            int pageSize,
            string userId,
            TrainerFilterData filterData)
        {
            var trainers = await _trainerRepository.GetFavouriteTrainers(userId);
            var result = GetTrainers(pageNumber, pageSize, trainers, filterData);
            return result;
        }

        public async Task<PagedTrainersDTO> GetAllTrainers(
            int pageNumber,
            int pageSize,
            string userId,
            TrainerFilterData filterData)
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

            var result = GetTrainers(pageNumber, pageSize, trainers, filterData);
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
            int pageNumber, int pageSize, IEnumerable<Trainer> trainers, TrainerFilterData filterData)
        {
            trainers = Filter(filterData, trainers);
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

        private IEnumerable<Trainer> Filter(TrainerFilterData filterData, IEnumerable<Trainer> trainers)
        {
            trainers = trainers.Where(trainer => ApplyFilters(filterData)
                .IsSatisfiedBy(trainer))
                .ToList();
            return trainers;
        }

        private ISpecification<Trainer> ApplyFilters(TrainerFilterData filterData)
        {
            return new TrainerMatchesLocation(filterData.Location)
                .And(new TrainerMatchesKeywords(filterData.Keywords))
                .And(new TrainerMatchesSports(filterData.SportIds));
        }

        private async Task<double> CalculateAverageTrainerRate(int trainerId)
        {
            var rates = await _rateRepository.GetTrainerRateValues(trainerId);
            return rates.Count() != 0 ? rates.Average() : 0;
        }

    }
}
