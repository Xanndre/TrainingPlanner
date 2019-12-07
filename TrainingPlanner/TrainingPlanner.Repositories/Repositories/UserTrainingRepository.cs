using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class UserTrainingRepository : BaseRepository, IUserTrainingRepository
    {
        public UserTrainingRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<UserTraining> UpdateUserTraining(UserTraining training)
        {
            _trainingPlannerDbContext.Update(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task<UserTraining> CreateUserTraining(UserTraining training)
        {
            await _trainingPlannerDbContext.UserTrainings.AddAsync(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task DeleteUserTraining(UserTraining training)
        {
            _trainingPlannerDbContext.UserTrainings.Remove(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTraining>> GetUserTrainings(string userId)
        {
            return await GetUserTrainingQuery()
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserTraining> GetUserTraining(int id)
        {
            return await GetUserTrainingQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetExercisesToDelete(UserTraining training)
        {
            return await _trainingPlannerDbContext.Exercises
                .Where(c => c.UserTrainingId == training.Id)
                .Except(training.Exercises)
                .ToListAsync();
        }

        public async Task RemoveExercises(IEnumerable<Exercise> exercises, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.Exercises.RemoveRange(exercises);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        private IQueryable<UserTraining> GetUserTrainingQuery()
        {
            return _trainingPlannerDbContext.UserTrainings
                .Include(t => t.Exercises);
        }
    }
}
