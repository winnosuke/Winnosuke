using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly MyGarageFinalContext _context;

        public AppointmentService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Garage)
                .Include(a => a.Service)
                .Include(a => a.Vehicle)
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Garage)
                .Include(a => a.Service)
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            var existing = await _context.Appointments.FindAsync(appointment.AppointmentId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Appointment>> SearchAsync(AppointmentSearchViewModel filter)
        {
            var query = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Vehicle)
                .Include(a => a.Garage)
                .Include(a => a.Service)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                query = query.Where(a => a.User.FullName.Contains(filter.CustomerName));
            }

            if (!string.IsNullOrEmpty(filter.LicensePlate))
            {
                query = query.Where(a => a.Vehicle.LicensePlate.Contains(filter.LicensePlate));
            }

            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(a => a.Status.Contains(filter.Status));
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(a => a.AppointmentTime >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(a => a.AppointmentTime <= filter.ToDate.Value);
            }

            return await query.OrderByDescending(a => a.AppointmentTime).ToListAsync();
        }

    }
}
