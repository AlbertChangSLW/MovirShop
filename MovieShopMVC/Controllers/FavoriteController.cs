using ApplicationCore.Contracts.Services;
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
        public async Task<IActionResult> MoviesByFavorite(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _userService.GetAllFavoritesForUser(id, pageSize, pageNumber);
            return View(pagedMovies);
        }
    }
}
