using Winnosuke.ViewModels;
using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public interface IRevenueService
    {
        Task<RevenueViewModel> GetRevenueAsync();
    }
}
