using System.Collections.Generic;

namespace Winnosuke.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalReports { get; set; }

        public List<GarageRatingViewModel> TopRatedGarages { get; set; } = new();
    }

    public class GarageRatingViewModel
    {
        public string GarageName { get; set; } = "";
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
