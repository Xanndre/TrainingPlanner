using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper, ITrainerRepository trainerRepository,
                            IClubRepository clubRepository)
        {
            _userRepository = repository;
            _mapper = mapper;
            _trainerRepository = trainerRepository;
            _clubRepository = clubRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var identityUser = await _userRepository.GetUser(user.Id);
            identityUser = _mapper.Map(user, identityUser);
            var appUser = await _userRepository.UpdateUser(identityUser);
            return _mapper.Map<UserDTO>(appUser);
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            var trainer = await _trainerRepository.GetTrainerByUser(id);
            var clubs = await _clubRepository.GetUserClubs(id);
            foreach (var club in clubs)
            {
                await _clubRepository.DeleteClub(club);
            }
            await _trainerRepository.DeleteTrainer(trainer);
            await _userRepository.DeleteUser(user);
        }
    }
}
