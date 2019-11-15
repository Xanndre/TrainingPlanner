using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class RateRepository : BaseRepository, IRateRepository
    {
        public RateRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<TrainerRate> UpdateTrainerRate(TrainerRate rate)
        {
            _trainingPlannerDbContext.Update(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return rate;
        }

        public async Task<TrainerRate> CreateTrainerRate(TrainerRate rate)
        {
            await _trainingPlannerDbContext.TrainerRatings.AddAsync(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return rate;
        }

        public async Task DeleteTrainerRate(TrainerRate rate)
        {
            _trainingPlannerDbContext.TrainerRatings.Remove(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<TrainerRate> GetTrainerRate(string userId, int trainerId)
        {
            return await GetTrainerRateQuery()
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TrainerId == trainerId);
        }

        public async Task<IEnumerable<TrainerRate>> GetTrainerRates(int trainerId)
        {
            return await GetTrainerRateQuery()
                .Include(t => t.User)
                .Where(t => t.TrainerId == trainerId)
                .ToListAsync();
        }

        public async Task<TrainerRate> GetTrainerRateById(int id)
        {
            return await GetTrainerRateQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<int>> GetTrainerRateValues(int trainerId)
        {
            return await GetTrainerRateQuery()
                .Include(t => t.User)
                .Where(t => t.TrainerId == trainerId)
                .Select(t => t.Rate)
                .ToListAsync();
        }

        public async Task<ClubRate> UpdateClubRate(ClubRate rate)
        {
            _trainingPlannerDbContext.Update(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return rate;
        }

        public async Task<ClubRate> CreateClubRate(ClubRate rate)
        {
            await _trainingPlannerDbContext.ClubRatings.AddAsync(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return rate;
        }

        public async Task DeleteClubRate(ClubRate rate)
        {
            _trainingPlannerDbContext.ClubRatings.Remove(rate);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<ClubRate> GetClubRate(string userId, int clubId)
        {
            return await GetClubRateQuery()
                .FirstOrDefaultAsync(t => t.UserId == userId && t.ClubId == clubId);
        }

        public async Task<IEnumerable<ClubRate>> GetClubRates(int clubId)
        {
            return await GetClubRateQuery()
                .Include(t => t.User)
                .Where(t => t.ClubId == clubId)
                .ToListAsync();
        }

        public async Task<ClubRate> GetClubRateById(int id)
        {
            return await GetClubRateQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<int>> GetClubRateValues(int clubId)
        {
            return await GetClubRateQuery()
                .Include(t => t.User)
                .Where(t => t.ClubId == clubId)
                .Select(t => t.Rate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubRate>> GetUserClubRates(string userId)
        {
            return await GetClubRateQuery()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerRate>> GetUserTrainerRates(string userId)
        {
            return await GetTrainerRateQuery()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        private IQueryable<ClubRate> GetClubRateQuery()
        {
            return _trainingPlannerDbContext.ClubRatings;
        }

        private IQueryable<TrainerRate> GetTrainerRateQuery()
        {
            return _trainingPlannerDbContext.TrainerRatings;
        }
    }
}
