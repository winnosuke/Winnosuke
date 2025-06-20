using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class AuthService : IAuthService
    {
        private readonly MyGarageFinalContext _context;
        private readonly IEmailService _emailService;

        public AuthService(MyGarageFinalContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == username && u.PasswordHash == password && u.IsEmailConfirmed);
        }

        public async Task<User?> AuthenticateWithGoogleAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == email && u.IsEmailConfirmed);
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username || u.Email == model.Email))
                return false;

            var token = Guid.NewGuid().ToString();

            var user = new User
            {
                Username = model.Username,
                PasswordHash = model.Password,
                Email = model.Email,
                FullName = model.FullName,
                Role = "Customer",
                IsEmailConfirmed = false,
                EmailConfirmationToken = token
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var confirmUrl = $"https://localhost:44300/AccountRegister/ConfirmEmail?token={token}";
            var body = $"<p>Chào {model.FullName},</p><p>Vui lòng xác nhận email bằng cách <a href='{confirmUrl}'>click vào đây</a>.</p>";

            await _emailService.SendEmailAsync(model.Email, "Xác nhận tài khoản", body);
            return true;
        }

        public async Task<bool> ConfirmEmailAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
            if (user == null) return false;

            user.IsEmailConfirmed = true;
            user.EmailConfirmationToken = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ResendConfirmationAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsEmailConfirmed);
            if (user == null) return;

            user.EmailConfirmationToken = Guid.NewGuid().ToString();
            await _context.SaveChangesAsync();

            var confirmUrl = $"https://localhost:44300/AccountRegister/ConfirmEmail?token={user.EmailConfirmationToken}";
            var body = $"<p>Vui lòng xác nhận lại tài khoản bằng cách <a href='{confirmUrl}'>bấm vào đây</a>.</p>";

            await _emailService.SendEmailAsync(email, "Gửi lại xác nhận email", body);
        }
    }
}
