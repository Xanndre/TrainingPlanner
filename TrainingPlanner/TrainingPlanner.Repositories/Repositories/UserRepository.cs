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

        public async Task<IEnumerable<ApplicationUser>> GetSignedUpUsers(int trainingId)
        {
            var users = GetUserWithReservationQuery(trainingId)
                .Where(t => _trainingPlannerDbContext.Reservations
                    .Any(res => t.Id == res.UserId && res.Training.Id == trainingId))
                .OrderBy(t => _trainingPlannerDbContext.Reservations
                    .SingleAsync(x => x.TrainingId == trainingId && x.UserId == t.Id).Result.Date);

            return await users.ToList();
        }

        public async Task<IEnumerable<ApplicationUser>> GetNotSignedUpUsers(int trainingId, string userId)
        {
            var users = GetUserWithReservationQuery(trainingId)
                .Where(t => t.Id != userId && !_trainingPlannerDbContext.Reservations
                    .Any(res => t.Id == res.UserId && res.Training.Id == trainingId));

            return await users.ToList();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await GetUserQuery()
                .Include(u => u.Notification)
                .FirstAsync(u => u.Id == id);
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

        public async Task<IEnumerable<UserSport>> GetUserSportsToDelete(ApplicationUser user)
        {
            return await _trainingPlannerDbContext.UserSports
                .Where(u => u.UserId == user.Id)
                .Except(user.Sports)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsToDelete(ApplicationUser user)
        {
            var list = new List<Notification>
            {
                user.Notification
            };

            return await _trainingPlannerDbContext.Notifications
                .Where(u => u.UserId == user.Id)
                .Except(list)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserLocation>> GetUserLocationsToDelete(ApplicationUser user)
        {
            return await _trainingPlannerDbContext.UserLocations
                .Where(u => u.UserId == user.Id)
                .Except(user.Locations)
                .ToListAsync();
        }

        public async Task RemoveUserNotification(Notification notification, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.Notifications.Remove(notification);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveUserSports(IEnumerable<UserSport> sports, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.UserSports.RemoveRange(sports);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveUserLocations(IEnumerable<UserLocation> locations, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.UserLocations.RemoveRange(locations);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        private IQueryable<ApplicationUser> GetUserQuery()
        {
            return _trainingPlannerDbContext.Users
                .Include(t => t.Sports)
                .Include(t => t.Locations)
                .AsNoTracking();
        }

        private IAsyncEnumerable<ApplicationUser> GetUserWithReservationQuery(int trainingId)
        {
            return GetUserQuery()
                  .Include(x => x.Reservations)
                  .ToAsyncEnumerable()
                  .Select(x =>
                  {
                      x.Reservations = x.Reservations.Any() ? x.Reservations.Where(f => f.TrainingId == trainingId).ToList() : null;
                      return x;
                  });
        }

    }
}
