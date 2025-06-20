using Winnosuke.Models;
using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task CreateAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
        Task<List<Appointment>> SearchAsync(AppointmentSearchViewModel filter);
    }
}
