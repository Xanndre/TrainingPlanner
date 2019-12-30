using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class FavouriteRepository : BaseRepository, IFavouriteRepository
    {
        public FavouriteRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<FavouriteClub> CreateFavouriteClub(FavouriteClub favourite)
        {
            await _trainingPlannerDbContext.FavouriteClubs.AddAsync(favourite);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return favourite;
        }

        public async Task DeleteFavouriteClub(FavouriteClub favourite)
        {
            _trainingPlannerDbContext.FavouriteClubs.Remove(favourite);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<FavouriteClub> GetFavouriteClub(int clubId, string userId)
        {
            return await GetFavouriteClubs()
                .FirstAsync(fav => fav.ClubId == clubId && fav.UserId == userId);
        }

        public async Task<FavouriteTrainer> CreateFavouriteTrainer(FavouriteTrainer favourite)
        {
            await _trainingPlannerDbContext.FavouriteTrainers.AddAsync(favourite);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return favourite;
        }

        public async Task DeleteFavouriteTrainer(FavouriteTrainer favourite)
        {
            _trainingPlannerDbContext.FavouriteTrainers.Remove(favourite);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<FavouriteTrainer> GetFavouriteTrainer(int trainerId, string userId)
        {
            return await GetFavouriteTrainers()
                .FirstAsync(fav => fav.TrainerId == trainerId && fav.UserId == userId);
        }

        public async Task<IEnumerable<FavouriteClub>> GetUserFavouriteClubs(string userId)
        {
            return await GetFavouriteClubs()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavouriteTrainer>> GetUserFavouriteTrainers(string userId)
        {
            return await GetFavouriteTrainers()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavouriteClub>> GetFavouriteClubs(int clubId)
        {
            return await GetFavouriteClubs()
                .Where(c => c.ClubId == clubId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<FavouriteTrainer>> GetFavouriteTrainers(int trainerId)
        {
            return await GetFavouriteTrainers()
                .Where(c => c.TrainerId == trainerId)
                .AsNoTracking()
                .ToListAsync();
        }

        private IQueryable<FavouriteClub> GetFavouriteClubs()
        {
            return _trainingPlannerDbContext.FavouriteClubs;
        }

        private IQueryable<FavouriteTrainer> GetFavouriteTrainers()
        {
            return _trainingPlannerDbContext.FavouriteTrainers;
        }
    }
}
