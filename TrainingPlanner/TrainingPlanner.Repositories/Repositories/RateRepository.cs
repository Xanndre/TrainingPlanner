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
            return await _trainingPlannerDbContext.TrainerRatings
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TrainerId == trainerId);
        }

        public async Task<IEnumerable<TrainerRate>> GetTrainerRates(int trainerId)
        {
            return await _trainingPlannerDbContext.TrainerRatings
                .Where(t => t.TrainerId == trainerId)
                .ToListAsync();
        }

        public async Task<TrainerRate> GetTrainerRateById(int id)
        {
            return await _trainingPlannerDbContext.TrainerRatings
                .FirstOrDefaultAsync(t => t.Id == id);
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
            return await _trainingPlannerDbContext.ClubRatings
                .FirstOrDefaultAsync(t => t.UserId == userId && t.ClubId == clubId);
        }

        public async Task<IEnumerable<ClubRate>> GetClubRates(int clubId)
        {
            return await _trainingPlannerDbContext.ClubRatings
                .Where(t => t.ClubId == clubId)
                .ToListAsync();
        }

        public async Task<ClubRate> GetClubRateById(int id)
        {
            return await _trainingPlannerDbContext.ClubRatings
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
