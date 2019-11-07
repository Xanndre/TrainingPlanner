using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Account;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDTO> Login(LoginDTO dto);
        Task Register(RegisterDTO dto);
        Task<LoginResultDTO> ExternalLogin(ExternalLoginDTO dto);
        Task<string> ConfirmEmail(string id, string token);
        Task SendEmailAgain(string id);
        Task ChangePassword(ChangePasswordDTO dto);
        Task SendResetToken(string email);
        Task SendResetTokenAgain(string id);
        Task ResetPassword(ResetPasswordDTO dto);
    }
}
