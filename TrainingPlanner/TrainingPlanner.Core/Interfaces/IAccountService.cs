using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Account;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDTO> Login(LoginDTO dto);
        Task Register(RegisterDTO dto);
        Task<LoginResultDTO> ExternalLogin(ExternalLoginDTO dto);
    }
}
