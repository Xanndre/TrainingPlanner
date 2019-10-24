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
