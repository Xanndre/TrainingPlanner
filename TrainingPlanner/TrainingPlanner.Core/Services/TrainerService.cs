using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainerDTO>> GetAllTrainers()
        {
            var trainers = await _trainerRepository.GetAllTrainers();
            return _mapper.Map<IEnumerable<TrainerDTO>>(trainers);
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

        public async Task<TrainerDTO> UpdateTrainer(TrainerDTO trainer)
        {
            var mappedTrainer = _mapper.Map<Trainer>(trainer);
            var returnedTrainer = await _trainerRepository.UpdateTrainer(mappedTrainer);
            return _mapper.Map<TrainerDTO>(returnedTrainer);
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

    }
}
