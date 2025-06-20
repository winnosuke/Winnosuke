namespace Winnosuke.ViewModels
{
    public class MaintenanceSuggestionViewModel
    {
        public string VehicleName { get; set; }
        public string LicensePlate { get; set; }
        public List<string> Suggestions { get; set; } = new();
    }
}
