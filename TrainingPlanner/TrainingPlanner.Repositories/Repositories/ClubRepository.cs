using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class ClubRepository : BaseRepository, IClubRepository
    {
        public ClubRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            return await _trainingPlannerDbContext.Clubs.ToListAsync();
        }

        public async Task<Club> GetClub(int id)
        {
            return await _trainingPlannerDbContext.Clubs.FirstAsync(c => c.Id == id);
        }

        public async Task<Club> UpdateClub(Club club)
        {
            _trainingPlannerDbContext.Update(club);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return club;
        }

        public async Task<Club> CreateClub(Club club)
        {
            await _trainingPlannerDbContext.Clubs.AddAsync(club);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return club;
        }

        public async Task DeleteClub(Club club)
        {
            _trainingPlannerDbContext.Clubs.Remove(club);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }
    }
}
