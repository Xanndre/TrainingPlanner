using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.UserStuff.UserTraining;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class UserTrainingService : IUserTrainingService
    {
        private const int MaxPageSize = 20;
        private readonly IUserTrainingRepository _userTrainingRepository;
        private readonly IMapper _mapper;

        public UserTrainingService(IUserTrainingRepository userTrainingRepository, IMapper mapper)
        {
            _userTrainingRepository = userTrainingRepository;
            _mapper = mapper;
        }

        public async Task<UserTrainingDTO> UpdateUserTraining(UserTrainingDTO training)
        {
            var mappedTraining = _mapper.Map<UserTraining>(training);
            await RemoveExercises(mappedTraining);
            var returnedTraining = await _userTrainingRepository.UpdateUserTraining(mappedTraining);
            return _mapper.Map<UserTrainingDTO>(returnedTraining);
        }

        public async Task<UserTrainingCreateDTO> CreateUserTraining(UserTrainingCreateDTO training)
        {
            var mappedTraining = _mapper.Map<UserTraining>(training);
            var returnedTraining = await _userTrainingRepository.CreateUserTraining(mappedTraining);
            return _mapper.Map<UserTrainingCreateDTO>(returnedTraining);
        }

        public async Task DeleteUserTraining(int id)
        {
            var training = await _userTrainingRepository.GetUserTraining(id);
            await _userTrainingRepository.DeleteUserTraining(training);
        }

        public async Task<UserTrainingDTO> GetUserTraining(int id)
        {
            var training = await _userTrainingRepository.GetUserTraining(id);
            return _mapper.Map<UserTrainingDTO>(training);
        }

        public async Task<PagedUserTrainingsDTO> GetAllUserTrainings(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var trainings = await _userTrainingRepository.GetUserTrainings(userId);
            var result = GetUserTrainings(pageNumber, pageSize, trainings);
            return result;
        }

        private PagedUserTrainingsDTO GetUserTrainings(
            int pageNumber, int pageSize, IEnumerable<UserTraining> trainings)
        {
            var userTrainings = _mapper.Map<IEnumerable<UserTrainingDTO>>(trainings);

            var result = GetPagedUserTrainings(userTrainings, pageNumber, pageSize);
            return result;
        }

        private PagedUserTrainingsDTO GetPagedUserTrainings(IEnumerable<UserTrainingDTO> trainings, int pageNumber, int pageSize)
        {
            var result = new PagedUserTrainingsDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedTrainings = trainings
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = trainings.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.UserTrainings = _mapper.Map<IEnumerable<UserTrainingBaseDTO>>(pagedTrainings);

            return result;
        }

        private async Task RemoveExercises(UserTraining mappedTraining)
        {
            var exercisesToDelete = await _userTrainingRepository.GetExercisesToDelete(mappedTraining);
            await _userTrainingRepository.RemoveExercises(exercisesToDelete, false);
        }
    }
}
