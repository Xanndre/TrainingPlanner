using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.UserStuff.UserCalendarTraining;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class UserCalendarTrainingService : IUserCalendarTrainingService
    {
        private readonly IUserCalendarTrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public UserCalendarTrainingService(IUserCalendarTrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<UserCalendarTrainingDTO> GetUserCalendarTraining(int id)
        {
            var training = await _trainingRepository.GetUserCalendarTraining(id);
            return _mapper.Map<UserCalendarTrainingDTO>(training);
        }

        public async Task<UserCalendarTrainingDTO> UpdateUserCalendarTraining(UserCalendarTrainingDTO training)
        {
            if (training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<UserCalendarTraining>(training);
            var returnedTraining = await _trainingRepository.UpdateUserCalendarTraining(mappedTraining);
            return _mapper.Map<UserCalendarTrainingDTO>(returnedTraining);
        }

        public async Task<UserCalendarTrainingCreateDTO> CreateUserCalendarTraining(UserCalendarTrainingCreateDTO training)
        {
            if (training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<UserCalendarTraining>(training);
            var returnedTraining = await _trainingRepository.CreateUserCalendarTraining(mappedTraining);
            return _mapper.Map<UserCalendarTrainingCreateDTO>(returnedTraining);
        }

        public async Task DeleteUserCalendarTraining(int id)
        {
            var training = await _trainingRepository.GetUserCalendarTraining(id);
            await _trainingRepository.DeleteUserCalendarTraining(training);
        }

        public async Task<IEnumerable<UserCalendarTrainingDTO>> GetUserCalendarTrainings(string userId)
        {
            var trainings = await _trainingRepository.GetUserCalendarTrainings(userId);
            return _mapper.Map<IEnumerable<UserCalendarTrainingDTO>>(trainings);
        }

        public async Task<IEnumerable<UserCalendarTrainingCreateDTO>> CreateUserCalendarTrainingRange(IEnumerable<UserCalendarTrainingCreateDTO> trainings)
        {
            foreach (var training in trainings)
            {
                if (training.StartDate > training.EndDate)
                {
                    throw new Exception(DictionaryResources.InvalidDates);
                }
            }

            var mappedTrainings = _mapper.Map<IEnumerable<UserCalendarTraining>>(trainings);
            var returnedTrainings = await _trainingRepository.CreateUserCalendarTrainingRange(mappedTrainings);
            return _mapper.Map<IEnumerable<UserCalendarTrainingCreateDTO>>(returnedTrainings);
        }
    }
}
