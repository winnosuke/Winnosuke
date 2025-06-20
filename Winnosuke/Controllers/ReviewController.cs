using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminList()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyReviews()
        {
            var userId = GetCurrentUserId();
            var reviews = await _reviewService.GetByUserIdAsync(userId);
            return View(reviews);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Review model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = GetCurrentUserId();
                model.CreatedAt = DateTime.Now;
                await _reviewService.CreateAsync(model);
                return RedirectToAction(nameof(MyReviews));
            }
            return View(model);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null || review.UserId != GetCurrentUserId())
                return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewService.DeleteAsync(id);
            return RedirectToAction(nameof(MyReviews));
        }
    }
}
