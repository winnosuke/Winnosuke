using System.Threading.Tasks;
namespace Winnosuke.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}