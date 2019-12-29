using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.Reservation;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.DTOs.UserStuff;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Specifications.Filters.UserFilters;
using TrainingPlanner.Core.Specifications.Interfaces;
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
        private readonly IBodyMeasurementRepository _bodyMeasurementRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserTrainingRepository _userTrainingRepository;
        private readonly ITrainingService _trainingService;
        private readonly IChatRepository _chatRepository;

        public UserService(IUserRepository repository, IMapper mapper, ITrainerRepository trainerRepository,
                            IClubRepository clubRepository, IFavouriteRepository favouriteRepository,
                            IRateRepository rateRepository, IBodyMeasurementRepository bodyMeasurementRepository,
                            ITrainingRepository trainingRepository, IReservationRepository reservationRepository,
                            IUserTrainingRepository userTrainingRepository, ITrainingService trainingService,
                            IChatRepository chatRepository)
        {
            _userRepository = repository;
            _mapper = mapper;
            _trainerRepository = trainerRepository;
            _clubRepository = clubRepository;
            _rateRepository = rateRepository;
            _favouriteRepository = favouriteRepository;
            _bodyMeasurementRepository = bodyMeasurementRepository;
            _trainingRepository = trainingRepository;
            _reservationRepository = reservationRepository;
            _userTrainingRepository = userTrainingRepository;
            _trainingService = trainingService;
            _chatRepository = chatRepository;
        }

        public PagedUsersDTO GetAllUsers(
            int pageNumber,
            int pageSize,
            UserFilterData filterData)
        {
            var users = _userRepository.GetAllUsers();
            var result = GetUsers(pageNumber, pageSize, users, filterData);
            return result;
        }

        public async Task<PagedReservationUsersDTO> GetSignedUpUsers(
            int pageNumber,
            int pageSize,
            int trainingId,
            UserFilterData filterData)
        {
            var users = await _userRepository.GetSignedUpUsers(trainingId);
            var pagedUsers = GetUsers(pageNumber, pageSize, users, filterData);
            var training = await _trainingService.GetTraining(trainingId);
            var pagedReservationUsers = new List<ReservationUserDTO>();

            foreach (var user in pagedUsers.Users)
            {
                var reservation = await _reservationRepository.GetReservation(trainingId, user.Id);
                var reservationInfo = new ReservationInfoDTO();
                if (reservation != null)
                {
                    reservationInfo.IsSignedUp = true;
                    reservationInfo.IsReserveList = reservation.IsReserveList;
                }
                else
                {
                    reservationInfo.IsSignedUp = false;
                    reservationInfo.IsReserveList = false;
                }
                pagedReservationUsers.Add(new ReservationUserDTO
                {
                    User = user,
                    ReservationInfo = reservationInfo
                });
            }

            return new PagedReservationUsersDTO
            {
                TotalCount = pagedUsers.TotalCount,
                TotalPages = pagedUsers.TotalPages,
                Users = pagedReservationUsers,
                Training = training
            };
        }

        public async Task<PagedReservationUsersDTO> GetNotSignedUpUsers(
            int pageNumber,
            int pageSize,
            int trainingId,
            string userId,
            UserFilterData filterData)
        {
            var users = await _userRepository.GetNotSignedUpUsers(trainingId, userId);
            var pagedUsers = GetUsers(pageNumber, pageSize, users, filterData);
            var training = await _trainingService.GetTraining(trainingId);
            var pagedReservationUsers = new List<ReservationUserDTO>();

            foreach (var user in pagedUsers.Users)
            {
                var reservation = await _reservationRepository.GetReservation(trainingId, user.Id);
                var reservationInfo = new ReservationInfoDTO();
                if (reservation != null)
                {
                    reservationInfo.IsSignedUp = true;
                    reservationInfo.IsReserveList = reservation.IsReserveList;
                }
                else
                {
                    reservationInfo.IsSignedUp = false;
                    reservationInfo.IsReserveList = false;
                }
                pagedReservationUsers.Add(new ReservationUserDTO
                {
                    User = user,
                    ReservationInfo = reservationInfo
                });
            }

            return new PagedReservationUsersDTO
            {
                TotalCount = pagedUsers.TotalCount,
                TotalPages = pagedUsers.TotalPages,
                Users = pagedReservationUsers,
                Training = training
            };
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

            await RemoveUserNotifications(identityUser);
            var appUser = await _userRepository.UpdateUser(identityUser);
            return _mapper.Map<UserDTO>(appUser);
        }

        public async Task DeleteUser(string id)
        {
            var trainer = await _trainerRepository.GetTrainerByUser(id);
            if (trainer != null)
            {
                var trainings = await _trainingRepository.GetTrainerTrainings(trainer.Id);
                foreach (var training in trainings)
                {
                    await _trainingRepository.DeleteTraining(training);
                }

                await _trainerRepository.DeleteTrainer(trainer);
            }

            var clubs = await _clubRepository.GetUserClubs(id);
            if (clubs.Count() != 0)
            {
                foreach (var club in clubs)
                {
                    var trainings = await _trainingRepository.GetClubTrainings(club.Id);
                    foreach (var training in trainings)
                    {
                        await _trainingRepository.DeleteTraining(training);
                    }

                    await _clubRepository.DeleteClub(club);
                }
            }

            var favClubs = await _favouriteRepository.GetUserFavouriteClubs(id);
            if (favClubs.Count() != 0)
            {
                foreach (var club in favClubs)
                {
                    await _favouriteRepository.DeleteFavouriteClub(club);
                }
            }

            var favTrainers = await _favouriteRepository.GetUserFavouriteTrainers(id);
            if (favTrainers.Count() != 0)
            {
                foreach (var favTrainer in favTrainers)
                {
                    await _favouriteRepository.DeleteFavouriteTrainer(favTrainer);
                }
            }

            var clubRates = await _rateRepository.GetUserClubRates(id);
            if (clubRates.Count() != 0)
            {
                foreach (var rate in clubRates)
                {
                    await _rateRepository.DeleteClubRate(rate);
                }
            }

            var trainerRates = await _rateRepository.GetUserTrainerRates(id);
            if (trainerRates.Count() != 0)
            {
                foreach (var rate in trainerRates)
                {
                    await _rateRepository.DeleteTrainerRate(rate);
                }
            }

            var bodyMeasurements = await _bodyMeasurementRepository.GetBodyMeasurements(id);
            if (bodyMeasurements.Count() != 0)
            {
                foreach (var measurement in bodyMeasurements)
                {
                    await _bodyMeasurementRepository.DeleteBodyMeasurement(measurement);
                }
            }

            var reservedTrainings = await _trainingRepository.GetReservedTrainings(id, true);
            var reservations = await _reservationRepository.GetReservations(id);
            if (reservations.Count() != 0)
            {
                foreach (var reservation in reservations)
                {
                    await _reservationRepository.DeleteReservation(reservation);
                }
            }

            foreach (var trng in reservedTrainings)
            {
                await _trainingService.UpdateSignedUpList(trng);
            }

            var userTrainings = await _userTrainingRepository.GetUserTrainings(id);
            if (userTrainings.Count() != 0)
            {
                foreach (var userTraining in userTrainings)
                {
                    await _userTrainingRepository.DeleteUserTraining(userTraining);
                }
            }

            var chats = await _chatRepository.GetAllChats(id);
            if(chats.Count() != 0)
            {
                foreach(var chat in chats)
                {
                    await _chatRepository.DeleteChat(chat);
                }
            }

            var user = await _userRepository.GetUser(id);
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
            var user = await GetPartner(userId);

            var partners = _mapper.Map<IEnumerable<PartnerDTO>>(users);
            foreach (var partner in partners)
            {
                partner.Similarity = Math.Round(GetSimilarity(user, partner), 2);
            }
            partners = partners.Where(u => u.Similarity > 0 && u.Id != userId).OrderByDescending(u => u.Similarity);

            var result = GetPagedPartners(partners, pageNumber, pageSize, userId);
            return result;
        }

        private PagedPartnersDTO GetPagedPartners(IEnumerable<PartnerDTO> partners, int pageNumber, int pageSize, string userId)
        {
            var result = new PagedPartnersDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

            var pagedUsers = partners
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = partners.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Partners = pagedUsers;

            return result;
        }

        private PagedUsersDTO GetUsers(
            int pageNumber, int pageSize, IEnumerable<ApplicationUser> users, UserFilterData filterData)
        {
            users = Filter(filterData, users);
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

        private IEnumerable<ApplicationUser> Filter(UserFilterData filterData, IEnumerable<ApplicationUser> users)
        {
            users = users.Where(user => ApplyFilters(filterData)
                .IsSatisfiedBy(user))
                .ToList();
            return users;
        }

        private ISpecification<ApplicationUser> ApplyFilters(UserFilterData filterData)
        {
            return new UserMatchesKeywords(filterData.Keywords);
        }

        private async Task RemoveUserNotifications(ApplicationUser mappedUser)
        {
            var userNotificationsToDelete = await _userRepository.GetUserNotificationsToDelete(mappedUser);
            if(userNotificationsToDelete.Any())
            {
                await _userRepository.RemoveUserNotification(userNotificationsToDelete.First(), false);
            }
            
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
                var sim = (double)commonSports.Count() / user.Sports.Count() * 100;
                return sim;
            }
            else
            {
                return 0;
            }
        }
    }
}
