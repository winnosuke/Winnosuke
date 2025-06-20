using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Customer")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var notifications = await _notificationService.GetByUserIdAsync(userId);
            return View(notifications);
        }

        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null || notification.UserId != GetCurrentUserId())
                return NotFound();

            await _notificationService.MarkAsReadAsync(id);
            return View(notification);
        }
    }
}
