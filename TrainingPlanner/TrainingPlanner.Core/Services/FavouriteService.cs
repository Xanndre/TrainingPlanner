using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Favourite;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IMapper _mapper;

        public FavouriteService(IFavouriteRepository repository, IMapper mapper)
        {
            _favouriteRepository = repository;
            _mapper = mapper;
        }

        public async Task<FavouriteClubDTO> CreateFavouriteClub(FavouriteClubDTO favourite)
        {
            var mappedFavourite = _mapper.Map<FavouriteClub>(favourite);
            var fav = await _favouriteRepository.CreateFavouriteClub(mappedFavourite);
            return _mapper.Map<FavouriteClubDTO>(fav);
        }

        public async Task DeleteFavouriteClub(int clubId, string userId)
        {
            var favourite = await _favouriteRepository.GetFavouriteClub(clubId, userId);
            await _favouriteRepository.DeleteFavouriteClub(favourite);
        }

        public async Task<FavouriteTrainerDTO> CreateFavouriteTrainer(FavouriteTrainerDTO favourite)
        {
            var mappedFavourite = _mapper.Map<FavouriteTrainer>(favourite);
            var fav = await _favouriteRepository.CreateFavouriteTrainer(mappedFavourite);
            return _mapper.Map<FavouriteTrainerDTO>(fav);
        }

        public async Task DeleteFavouriteTrainer(int trainerId, string userId)
        {
            var favourite = await _favouriteRepository.GetFavouriteTrainer(trainerId, userId);
            await _favouriteRepository.DeleteFavouriteTrainer(favourite);
        }

    }
}
