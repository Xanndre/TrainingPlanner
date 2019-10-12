using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class SportRepository : BaseRepository, ISportRepository
    {
        public SportRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Sport>> GetAllSports()
        {
            return await _trainingPlannerDbContext.Sports
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sport>> GetSportsByNames(string sportNames)
        {
            var sports = sportNames.Split(", ");
            return await _trainingPlannerDbContext.Sports
                .Where(s => sports.Contains(s.Name))
                .ToListAsync();
        }

        public async Task<Sport> GetSport(int id)
        {
            return await _trainingPlannerDbContext.Sports.FirstAsync(u => u.Id == id);
        }

        public async Task<Sport> UpdateSport(Sport sport)
        {
            _trainingPlannerDbContext.Update(sport);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return sport;
        }

        public async Task<Sport> CreateSport(Sport sport)
        {
            await _trainingPlannerDbContext.Sports.AddAsync(sport);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return sport;
        }

        public async Task DeleteSport(Sport sport)
        {
            _trainingPlannerDbContext.Sports.Remove(sport);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }
    }
}
