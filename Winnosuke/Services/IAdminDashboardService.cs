using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public interface IAdminDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}
