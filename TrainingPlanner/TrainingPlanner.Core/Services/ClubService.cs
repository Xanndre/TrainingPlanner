using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Club;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ClubService : IClubService
    {
        private const int MaxPageSize = 20;
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
        }

        public async Task<ClubDTO> GetClub(int id)
        {
            var club = await _clubRepository.GetClub(id);
            return _mapper.Map<ClubDTO>(club);
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

            var club = await _clubRepository.GetClub(id);
            await _clubRepository.DeleteClub(club);
        }

        public async Task<PagedClubsDTO> GetFavouriteClubs(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var clubs = await _clubRepository.GetFavouriteClubs(userId);
            var result = GetClubs(pageNumber, pageSize, clubs);
            return result;
        }

        public async Task<PagedClubsDTO> GetUserClubs(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var clubs = await _clubRepository.GetUserClubs(userId);
            var result = GetClubs(pageNumber, pageSize, clubs);
            return result;
        }

        public async Task<PagedClubsDTO> GetAllClubs(
            int pageNumber,
            int pageSize,
            string userId)
        {
            IEnumerable<Club> clubs;

            if (!string.IsNullOrEmpty(userId))
            {
                clubs = await _clubRepository.GetAllClubs(userId);
            }
            else
            {
                clubs = _clubRepository.GetAllClubs();
            }

            var result = GetClubs(pageNumber, pageSize, clubs);
            return result;
        }

        private PagedClubsDTO GetClubs(
            int pageNumber, int pageSize, IEnumerable<Club> clubs)
        {
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
    }
}
