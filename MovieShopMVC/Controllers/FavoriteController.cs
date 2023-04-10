using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> MoviesByFavorite(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _userService.GetAllFavoritesForUser(id, pageSize, pageNumber);
            return View(pagedMovies);
        }
    }
}
