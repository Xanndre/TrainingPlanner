using MimeKit;
using System.Threading.Tasks;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IEmailService
    {
        Task<MimeMessage> SendEmail(string email, string subject, string message);
    }
}
