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
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IRateRepository _rateRepository;

        public UserService(IUserRepository repository, IMapper mapper, ITrainerRepository trainerRepository,
                            IClubRepository clubRepository, IFavouriteRepository favouriteRepository,
                            IRateRepository rateRepository)
        {
            _userRepository = repository;
            _mapper = mapper;
            _trainerRepository = trainerRepository;
            _clubRepository = clubRepository;
            _rateRepository = rateRepository;
            _favouriteRepository = favouriteRepository;
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
            var favTrainers = await _favouriteRepository.GetUserFavouriteTrainers(id);
            var favClubs = await _favouriteRepository.GetUserFavouriteClubs(id);
            var clubRates = await _rateRepository.GetUserClubRates(id);
            var trainerRates = await _rateRepository.GetUserTrainerRates(id);

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

            if(favClubs.Count() != 0)
            {
                foreach (var club in favClubs)
                {
                    await _favouriteRepository.DeleteFavouriteClub(club);
                }
            }

            if(favTrainers.Count() != 0)
            {
                foreach (var favTrainer in favTrainers)
                {
                    await _favouriteRepository.DeleteFavouriteTrainer(favTrainer);
                }
            }

            if(clubRates.Count() != 0)
            {
                foreach (var rate in clubRates)
                {
                    await _rateRepository.DeleteClubRate(rate);
                }
            }

            if (trainerRates.Count() != 0)
            {
                foreach (var rate in trainerRates)
                {
                    await _rateRepository.DeleteTrainerRate(rate);
                }
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
