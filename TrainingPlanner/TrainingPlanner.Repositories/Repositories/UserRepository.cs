using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return GetUserQuery();
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

        public async Task<IdentityUserLogin<string>> GetUserLogin(string provider, string id)
        {
            var user = await _trainingPlannerDbContext.UserLogins
                .SingleOrDefaultAsync(u => u.LoginProvider == provider && u.UserId == id);
            return user;
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            _trainingPlannerDbContext.Users.Remove(user);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetLocations()
        {
            var locations = await _trainingPlannerDbContext.Users
                .Where(u => u.City != null)
                .Select(u => u.City)
                .Distinct()
                .ToListAsync();
            return locations;
        }

        private IQueryable<ApplicationUser> GetUserQuery()
        {
            return _trainingPlannerDbContext.Users.AsNoTracking();
        }

    }
}
