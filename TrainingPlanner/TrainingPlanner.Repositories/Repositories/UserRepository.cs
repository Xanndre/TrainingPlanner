using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _trainingPlannerDbContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _trainingPlannerDbContext.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
        {
            _trainingPlannerDbContext.Update(user);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return user;
        }
    }
}
