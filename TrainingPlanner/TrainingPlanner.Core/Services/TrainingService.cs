using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<TrainingDTO> GetTraining(int id)
        {
            var training = await _trainingRepository.GetTraining(id);     
            var mappedTraining = _mapper.Map<TrainingDTO>(training);
            return mappedTraining;
        }

        public async Task<TrainingDTO> UpdateTraining(TrainingDTO training)
        {
            if (training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<Training>(training);

            var returnedTraining = await _trainingRepository.UpdateTraining(mappedTraining);
            return _mapper.Map<TrainingDTO>(returnedTraining);
        }

        public async Task<TrainingCreateDTO> CreateTraining(TrainingCreateDTO training)
        {
            if(training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<Training>(training);
            var returnedTraining = await _trainingRepository.CreateTraining(mappedTraining);
            return _mapper.Map<TrainingCreateDTO>(returnedTraining);
        }

        public async Task DeleteTraining(int id)
        {
            var training = await _trainingRepository.GetTraining(id);
            await _trainingRepository.DeleteTraining(training);
        }

        public async Task<IEnumerable<TrainingDTO>> GetTrainerTrainings(int trainerId)
        {
            var trainings = await _trainingRepository.GetTrainerTrainings(trainerId);
            return _mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }

        public async Task<IEnumerable<TrainingDTO>> GetClubTrainings(int clubId)
        {
            var trainings = await _trainingRepository.GetClubTrainings(clubId);
            return _mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }
    }
}
