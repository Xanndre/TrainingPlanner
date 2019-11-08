using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class UserService : IUserService
    {
        private const int MaxPageSize = 20;
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

        public PagedUsersDTO GetAllUsers(
            int pageNumber,
            int pageSize)
        {
            var users = _userRepository.GetAllUsers();
            var result = GetUsers(pageNumber, pageSize, users);
            return result;
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
            if(clubs.Count() != 0)
            {
                foreach (var club in clubs)
                {
                    await _clubRepository.DeleteClub(club);
                }
            }
            
            if(trainer != null)
            {
                await _trainerRepository.DeleteTrainer(trainer);
            }
           
            await _userRepository.DeleteUser(user);
      
        }

        private PagedUsersDTO GetUsers(
            int pageNumber, int pageSize, IEnumerable<ApplicationUser> users)
        {
            var result = GetPagedUsers(users, pageNumber, pageSize);
            return result;
        }

        private PagedUsersDTO GetPagedUsers(IEnumerable<ApplicationUser> users, int pageNumber, int pageSize)
        {
            var result = new PagedUsersDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedUsers = users
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = users.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Users = _mapper.Map<IEnumerable<UserDTO>>(pagedUsers);

            return result;
        }
    }
}
