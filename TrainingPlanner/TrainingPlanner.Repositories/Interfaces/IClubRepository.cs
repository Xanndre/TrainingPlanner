using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IClubRepository
    {
        Task<Club> GetClub(int id);
        Task<Club> UpdateClub(Club club);
        Task<Club> CreateClub(Club club);
        Task DeleteClub(Club club);
        IQueryable<Club> GetAllClubs();
        Task<IEnumerable<Club>> GetFavouriteClubs(string userId);
        Task<IEnumerable<Club>> GetAllClubs(string userId);
        Task<IEnumerable<Club>> GetUserClubs(string userId);
    }
}
