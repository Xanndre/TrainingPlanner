using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Club> GetAllClubs()
        {
            return GetClubQuery();
        }


        public async Task<IEnumerable<Club>> GetAllClubs(string userId)
        {
            var clubs = GetClubWithFavouriteQuery(userId);
            return await clubs.ToList();
        }

        public async Task<IEnumerable<Club>> GetUserClubs(string userId)
        {
            var clubs = GetClubWithFavouriteQuery(userId)
                .Where(c => c.UserId == userId);
            return await clubs.ToList();
        }

        public async Task<IEnumerable<Club>> GetFavouriteClubs(string userId)
        {
            var clubs = GetClubWithFavouriteQuery(userId)
                .Where(c => _trainingPlannerDbContext.FavouriteClubs
                    .Any(fav => c.Id == fav.ClubId && fav.User.Id == userId));

            return await clubs.ToList();
        }

        public async Task<Club> GetClub(int id)
        {
            return await GetClubQuery()
                .FirstAsync(c => c.Id == id);
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

        public async Task<int> GetClubQuantity(string userId)
        {
            return await _trainingPlannerDbContext.Clubs
                .Where(c => c.UserId == userId)
                .CountAsync();
        }

        public async Task<IEnumerable<int>> GetClubIds(string userId)
        {
            return await _trainingPlannerDbContext.Clubs
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubActivity>> GetClubActivitiesToDelete(Club club)
        {
            return await _trainingPlannerDbContext.ClubActivities
                .Where(c => c.ClubId == club.Id)
                .Except(club.Activities)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubTrainer>> GetClubTrainersToDelete(Club club)
        {
            return await _trainingPlannerDbContext.ClubTrainers
                .Where(c => c.ClubId == club.Id)
                .Except(club.Trainers)
                .ToListAsync();
        }

        public async Task<IEnumerable<Picture>> GetClubPicturesToDelete(Club club)
        {
            return await _trainingPlannerDbContext.Pictures
                .Where(c => c.ClubId == club.Id)
                .Except(club.Pictures)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubPrice>> GetClubPricesToDelete(Club club)
        {
            return await _trainingPlannerDbContext.ClubPrices
                .Where(c => c.ClubId == club.Id)
                .Except(club.PriceList)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubWorkingHours>> GetClubWorkingHoursToDelete(Club club)
        {
            return await _trainingPlannerDbContext.ClubWorkingHours
                .Where(c => c.ClubId == club.Id)
                .Except(club.WorkingHours)
                .ToListAsync();
        }

        public async Task RemoveClubActivities(IEnumerable<ClubActivity> activities, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.ClubActivities.RemoveRange(activities);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveClubTrainers(IEnumerable<ClubTrainer> trainers, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.ClubTrainers.RemoveRange(trainers);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveClubPictures(IEnumerable<Picture> pictures, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.Pictures.RemoveRange(pictures);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveClubPrices(IEnumerable<ClubPrice> priceList, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.ClubPrices.RemoveRange(priceList);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveClubWorkingHours(IEnumerable<ClubWorkingHours> workingHours, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.ClubWorkingHours.RemoveRange(workingHours);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        private IQueryable<Club> GetClubQuery()
        {
            return _trainingPlannerDbContext.Clubs
                .Include(c => c.Activities)
                .Include(c => c.Pictures)
                .Include(c => c.PriceList)
                .Include(c => c.Trainers)
                .Include(c => c.WorkingHours);
        }

        private IAsyncEnumerable<Club> GetClubWithFavouriteQuery(string userId)
        {
            return GetClubQuery()
                  .Include(x => x.Favourites)
                  .ToAsyncEnumerable()
                  .Select(x =>
                  {
                      x.Favourites = x.Favourites.Any() ? x.Favourites.Where(f => f.UserId == userId).ToList() : null;
                      return x;
                  });
        }
    }
}
