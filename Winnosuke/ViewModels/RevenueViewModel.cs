using System.Collections.Generic;

namespace Winnosuke.ViewModels
{
    public class RevenueViewModel
    {
        public List<RevenueByMonth> MonthlyRevenue { get; set; } = new();
        public List<RevenueByGarage> GarageRevenue { get; set; } = new();
        public List<RevenueByService> ServiceRevenue { get; set; } = new();
    }

    public class RevenueByMonth
    {
        public string Month { get; set; } = "";
        public decimal Total { get; set; }
    }

    public class RevenueByGarage
    {
        public string GarageName { get; set; } = "";
        public decimal Total { get; set; }
    }

    public class RevenueByService
    {
        public string ServiceName { get; set; } = "";
        public decimal Total { get; set; }
    }
}
