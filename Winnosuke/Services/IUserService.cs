using System.Collections.Generic;
using System.Threading.Tasks;
using Winnosuke.Models;

namespace Winnosuke.Services { 
public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> IsEmailConfirmedAsync(string email);
}
}