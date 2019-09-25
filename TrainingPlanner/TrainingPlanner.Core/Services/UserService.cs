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
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
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
    }
}
