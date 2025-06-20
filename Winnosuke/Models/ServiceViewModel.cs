using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winnosuke.Models;
public class ServiceViewModel
{
    public int ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public string? image_url { get; set; }

    public int? GarageId { get; set; }  // ✅ Thêm dòng này để binding dropdown
    public string? GarageName { get; set; }

    public List<SelectListItem>? GarageList { get; set; } // ✅ Dropdown garage
    
}
