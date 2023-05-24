using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IUserService _userService;

        public FavoriteController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteMovie(int movieId, int userId)
        {
            var favoriteRequst = new FavoriteRequestModel()
            {
                MovieId = movieId,
            };
            await _userService.AddFavorite(favoriteRequst, userId);
            return RedirectToAction("MoviesByFavorite", new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFavorite(int movieId, int userId)
        {
            /*var favoriteRequst = new FavoriteRequestModel()
            {
                MovieId = movieId,
            };
            await _userService.RemoveFavorite(favoriteRequst, userId);*/
            await _userService.RemoveFavorite(movieId, userId);
            return RedirectToAction("MoviesByFavorite", new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> MoviesByFavorite( int pageSize = 30, int pageNumber = 1)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pagedMovies = await _userService.GetAllFavoritesForUser(userId, pageSize, pageNumber);

            if (pagedMovies.Count > 0)
                return View(pagedMovies);
            else return Redirect("~/");
        }
    }
}
