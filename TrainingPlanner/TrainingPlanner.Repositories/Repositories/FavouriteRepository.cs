using Microsoft.EntityFrameworkCore;
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
            return await _trainingPlannerDbContext.FavouriteClubs
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
            return await _trainingPlannerDbContext.FavouriteTrainers
                .FirstAsync(fav => fav.TrainerId == trainerId && fav.UserId == userId);
        }
    }
}
