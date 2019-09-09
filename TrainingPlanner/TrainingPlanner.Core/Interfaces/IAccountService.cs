using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDTO> Login(LoginDTO dto);
        Task Register(RegisterDTO dto);
        string GenerateJwtToken(string email, ApplicationUser user);
    }
}
