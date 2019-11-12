using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.DTOs.UserStuff;
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

        public async Task<PartnerDTO> GetPartner(string id)
        {
            var user = await _userRepository.GetUser(id);
            return _mapper.Map<PartnerDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(UserDTO user, bool isPartner)
        {
            var identityUser = await _userRepository.GetUser(user.Id);
            identityUser = _mapper.Map(user, identityUser);
            if (isPartner)
            {
                await RemoveUserSports(identityUser);
                await RemoveUserLocations(identityUser);
            }
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

        public async Task<IEnumerable<string>> GetLocations()
        {
            return await _userRepository.GetLocations();
        }

        public async Task<PagedPartnersDTO> GetAllPartners(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var users = _userRepository.GetAllUsers();
            var result = await GetPartners(pageNumber, pageSize, userId, users);
            return result;
        }

        private async Task<PagedPartnersDTO> GetPartners(
            int pageNumber, int pageSize, string userId, IEnumerable<ApplicationUser> users)
        {
            var authenticatedUser = await _userRepository.GetUser(userId);

            var partners = users.Where(u => GetSimilarity(authenticatedUser, u) > 0);

            var result = await GetPagedPartners(partners, pageNumber, pageSize, userId);
            return result;
        }

        private async Task<PagedPartnersDTO> GetPagedPartners(IEnumerable<ApplicationUser> users, int pageNumber, int pageSize, string userId)
        {
            var result = new PagedPartnersDTO();
            var user = await GetPartner(userId);

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedUsers = users
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = users.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Partners = _mapper.Map<IEnumerable<PartnerDTO>>(pagedUsers);
            foreach (var partner in result.Partners)
            {
                partner.Similarity = Math.Round(GetSimilarity(user, partner), 2);
            }
            return result;
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

        private async Task RemoveUserSports(ApplicationUser mappedUser)
        {
            var userSportsToDelete = await _userRepository.GetUserSportsToDelete(mappedUser);
            await _userRepository.RemoveUserSports(userSportsToDelete, false);
        }

        private async Task RemoveUserLocations(ApplicationUser mappedUser)
        {
            var userLocationsToDelete = await _userRepository.GetUserLocationsToDelete(mappedUser);
            await _userRepository.RemoveUserLocations(userLocationsToDelete, false);
        }

        private double GetSimilarity(ApplicationUser user, ApplicationUser partner)
        {
            if (user.Locations.Any(c => c.Location == partner.City) && user.Sports.Count() != 0)
            {
                var commonSports = new List<UserSport>();
                foreach (var sport in partner.Sports)
                {
                    if (user.Sports.Any(c => c.Sport == sport.Sport))
                    {
                        commonSports.Add(sport);
                    }
                }

                return commonSports.Count() / user.Sports.Count() * 100;
            }
            else
            {
                return 0;
            }
        }

        private double GetSimilarity(PartnerDTO user, PartnerDTO partner)
        {
            if (user.Locations.Any(c => c.Location == partner.City) && user.Sports.Count() != 0)
            {
                var commonSports = new List<UserSportDTO>();
                foreach (var sport in partner.Sports)
                {
                    if (user.Sports.Any(c => c.Sport == sport.Sport))
                    {
                        commonSports.Add(sport);
                    }
                }

                return commonSports.Count() / user.Sports.Count() * 100;
            }
            else
            {
                return 0;
            }
        }
    }
}
