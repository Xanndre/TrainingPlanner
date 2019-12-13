using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Club;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Specifications.Extensions;
using TrainingPlanner.Core.Specifications.Filters.ClubFilters;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ClubService : IClubService
    {
        private const int MaxPageSize = 20;
        private readonly IClubRepository _clubRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, IMapper mapper, IRateRepository rateRepository,
                            IFavouriteRepository favouriteRepository, ITrainingRepository trainingRepository)
        {
            _clubRepository = clubRepository;
            _rateRepository = rateRepository;
            _favouriteRepository = favouriteRepository;
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<ClubDTO> GetClub(int id, bool isIncrementingViewCounter)
        {
            var club = await _clubRepository.GetClub(id);
            if (isIncrementingViewCounter)
            {
                club.ViewCounter++;
            }
            var updatedClub = await _clubRepository.UpdateClub(club);
            var mappedClub = _mapper.Map<ClubDTO>(updatedClub);
            mappedClub.Average = await CalculateAverageClubRate(mappedClub.Id);
            return mappedClub;
        }

        public async Task<int> GetClubQuantity(string userId)
        {
            return await _clubRepository.GetClubQuantity(userId);
        }

        public async Task<IEnumerable<int>> GetClubIds(string userId)
        {
            return await _clubRepository.GetClubIds(userId);
        }

        public async Task<ClubUpdateDTO> UpdateClub(ClubUpdateDTO club)
        {
            var mappedClub = _mapper.Map<Club>(club);

            await RemoveClubActivities(mappedClub);
            await RemoveClubTrainers(mappedClub);
            await RemoveClubPictures(mappedClub);
            await RemoveClubWorkingHours(mappedClub);
            await RemoveClubPrices(mappedClub);

            var returnedClub = await _clubRepository.UpdateClub(mappedClub);
            return _mapper.Map<ClubUpdateDTO>(returnedClub);
        }

        public async Task<ClubCreateDTO> CreateClub(ClubCreateDTO club)
        {
            var mappedClub = _mapper.Map<Club>(club);
            var returnedClub = await _clubRepository.CreateClub(mappedClub);
            return _mapper.Map<ClubCreateDTO>(returnedClub);
        }

        public async Task DeleteClub(int id)
        {
            var rates = await _rateRepository.GetClubRates(id);
            var favs = await _favouriteRepository.GetFavouriteClubs(id);
            var trainings = await _trainingRepository.GetClubTrainings(id);

            if (favs.Count() != 0)
            {
                foreach (var fav in favs)
                {
                    await _favouriteRepository.DeleteFavouriteClub(fav);
                }
            }

            if (rates.Count() != 0)
            {
                foreach (var rate in rates)
                {
                    await _rateRepository.DeleteClubRate(rate);
                }
            }

            if(trainings.Count() != 0)
            {
                foreach (var training in trainings)
                {
                    await _trainingRepository.DeleteTraining(training);
                }
            }

            var club = await _clubRepository.GetClub(id);
            await _clubRepository.DeleteClub(club);
        }

        public async Task<PagedClubsDTO> GetFavouriteClubs(
            int pageNumber,
            int pageSize,
            string userId,
            ClubFilterData filterData)
        {
            var clubs = await _clubRepository.GetFavouriteClubs(userId);
            var result = GetClubs(pageNumber, pageSize, clubs, filterData);
            return result;
        }

        public async Task<PagedClubsDTO> GetUserClubs(
            int pageNumber,
            int pageSize,
            string userId,
            ClubFilterData filterData)
        {
            var clubs = await _clubRepository.GetUserClubs(userId);
            var result = GetClubs(pageNumber, pageSize, clubs, filterData);
            return result;
        }

        public async Task<PagedClubsDTO> GetAllClubs(
            int pageNumber,
            int pageSize,
            string userId,
            ClubFilterData filterData)
        {
            IEnumerable<Club> clubs;
            
            clubs = await _clubRepository.GetAllClubs(userId);
            
            var result = GetClubs(pageNumber, pageSize, clubs, filterData);
            return result;
        }

        public async Task<IEnumerable<string>> GetClubTrainerNames(int clubId)
        {
            var result = await _clubRepository.GetClubTrainerNames(clubId);
            return result;
        }

        private PagedClubsDTO GetClubs(
            int pageNumber, int pageSize, IEnumerable<Club> clubs, ClubFilterData filterData)
        {
            clubs = Filter(filterData, clubs);
            var result = GetPagedClubs(clubs, pageNumber, pageSize);
            return result;
        }

        private PagedClubsDTO GetPagedClubs(IEnumerable<Club> clubs, int pageNumber, int pageSize)
        {
            var result = new PagedClubsDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedClubs = clubs
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = clubs.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Clubs = _mapper.Map<IEnumerable<ClubBaseDTO>>(pagedClubs);

            return result;
        }

        private IEnumerable<Club> Filter(ClubFilterData filterData, IEnumerable<Club> clubs)
        {
            clubs = clubs.Where(club => ApplyFilters(filterData)
                .IsSatisfiedBy(club))
                .ToList();
            return clubs;
        }

        private ISpecification<Club> ApplyFilters(ClubFilterData filterData)
        {
            return new ClubMatchesLocation(filterData.Location)
                .And(new ClubMatchesKeywords(filterData.Keywords));
        }

        private async Task RemoveClubActivities(Club mappedClub)
        {
            var clubActivitiesToDelete = await _clubRepository.GetClubActivitiesToDelete(mappedClub);
            await _clubRepository.RemoveClubActivities(clubActivitiesToDelete, false);
        }

        private async Task RemoveClubTrainers(Club mappedClub)
        {
            var clubTrainersToDelete = await _clubRepository.GetClubTrainersToDelete(mappedClub);
            await _clubRepository.RemoveClubTrainers(clubTrainersToDelete, false);
        }

        private async Task RemoveClubPictures(Club mappedClub)
        {
            var clubPicturesToDelete = await _clubRepository.GetClubPicturesToDelete(mappedClub);
            await _clubRepository.RemoveClubPictures(clubPicturesToDelete, false);
        }

        private async Task RemoveClubWorkingHours(Club mappedClub)
        {
            var clubWorkingHoursToDelete = await _clubRepository.GetClubWorkingHoursToDelete(mappedClub);
            await _clubRepository.RemoveClubWorkingHours(clubWorkingHoursToDelete, false);
        }

        private async Task RemoveClubPrices(Club mappedClub)
        {
            var clubPricesToDelete = await _clubRepository.GetClubPricesToDelete(mappedClub);
            await _clubRepository.RemoveClubPrices(clubPricesToDelete, false);
        }

        private async Task<double> CalculateAverageClubRate(int clubId)
        {
            var rates = await _rateRepository.GetClubRateValues(clubId);
            return rates.Count() != 0 ? rates.Average() : 0;
        }
    }
}
