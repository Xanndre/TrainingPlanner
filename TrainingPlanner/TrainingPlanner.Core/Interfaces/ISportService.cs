using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ISportService
    {
        Task<IEnumerable<SportDTO>> GetAllSports();
        Task<IEnumerable<SportDTO>> GetSportsByNames(string sportNames);
        Task<SportDTO> GetSport(int id);
        Task<SportDTO> UpdateSport(SportDTO sport);
        Task<SportDTO> CreateSport(SportDTO sport);
        Task DeleteSport(int id);
    }
}
