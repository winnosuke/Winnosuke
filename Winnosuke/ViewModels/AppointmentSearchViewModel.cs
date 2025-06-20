namespace Winnosuke.ViewModels
{
    public class AppointmentSearchViewModel
    {
        public string? CustomerName { get; set; }
        public string? LicensePlate { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
