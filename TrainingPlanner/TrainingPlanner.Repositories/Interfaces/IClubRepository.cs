using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubs();
        Task<Club> GetClub(int id);
        Task<Club> UpdateClub(Club club);
        Task<Club> CreateClub(Club club);
        Task DeleteClub(Club club);
    }
}
