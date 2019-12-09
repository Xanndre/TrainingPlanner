using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class UserCalendarTrainingRepository : BaseRepository, IUserCalendarTrainingRepository
    {
        public UserCalendarTrainingRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<UserCalendarTraining> UpdateUserCalendarTraining(UserCalendarTraining training)
        {
            _trainingPlannerDbContext.Update(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task<UserCalendarTraining> CreateUserCalendarTraining(UserCalendarTraining training)
        {
            await _trainingPlannerDbContext.UserCalendarTrainings.AddAsync(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task DeleteUserCalendarTraining(UserCalendarTraining training)
        {
            _trainingPlannerDbContext.UserCalendarTrainings.Remove(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCalendarTraining>> GetUserCalendarTrainings(string userId)
        {
            return await GetUserCalendarTrainingQuery()
                .Where(t => t.UserTraining.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserCalendarTraining> GetUserCalendarTraining(int id)
        {
            return await GetUserCalendarTrainingQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<UserCalendarTraining>> CreateUserCalendarTrainingRange(IEnumerable<UserCalendarTraining> trainings)
        {
            await _trainingPlannerDbContext.UserCalendarTrainings.AddRangeAsync(trainings);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return trainings;
        }

        private IQueryable<UserCalendarTraining> GetUserCalendarTrainingQuery()
        {
            return _trainingPlannerDbContext.UserCalendarTrainings
                .Include(t => t.UserTraining)
                    .ThenInclude(t => t.Exercises);
        }
    }
}
