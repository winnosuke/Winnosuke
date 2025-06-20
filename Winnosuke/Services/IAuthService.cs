using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User?> AuthenticateWithGoogleAsync(string email);
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> ConfirmEmailAsync(string token);
        Task ResendConfirmationAsync(string email);
    }
}
