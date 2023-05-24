using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUserService _userService;

        public ReviewController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Review(int id, int pageSize = 50, int pageNumber = 1)
        {
            var reviews = await _userService.GetAllReviewForMovie(id, pageSize, pageNumber);
            if(reviews != null)
                return View(reviews);
            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReview(int movieId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var reviewquest = await _userService.MovieReviewByUser(userId, movieId);
            if (reviewquest == null)
            {
                reviewquest = new ReviewRequestModel()
                {
                    UserId = userId,
                    MovieId = movieId
                };

            }
            return View(reviewquest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel requestModel)
        {
            var review = await _userService.MovieReviewByUser(requestModel.UserId, requestModel.MovieId);
            
            if(review == null)
            {
                await _userService.AddMovieRevies(requestModel);
            }
            else
            {
                await _userService.UpdateMovieRevies(requestModel);
            }

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReview(int movieId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var review = await _userService.MovieReviewByUser(userId, movieId);
            await _userService.DeleteMovieRevies(review);

            return Redirect("~/");
        }
    }
}
