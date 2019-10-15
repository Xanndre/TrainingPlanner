using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<ClubDTO>> GetAllClubs();
        Task<ClubDTO> GetClub(int id);
        Task<ClubDTO> UpdateClub(ClubDTO club);
        Task<ClubCreateDTO> CreateClub(ClubCreateDTO club);
        Task DeleteClub(int id);
    }
}
