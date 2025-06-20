﻿using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IServiceService
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task CreateAsync(Service service);
        Task<bool> UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
