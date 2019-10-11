using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface ISportRepository
    {
        Task<IEnumerable<Sport>> GetAllSports();
        Task<IEnumerable<Sport>> GetSportsByNames(string sportNames);
        Task<Sport> GetSport(int id);
        Task<Sport> UpdateSport(Sport sport);
        Task<Sport> CreateSport(Sport sport);
        Task DeleteSport(Sport sport);
    }
}
