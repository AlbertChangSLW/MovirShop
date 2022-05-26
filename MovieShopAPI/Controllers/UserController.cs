using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            return Ok();
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie(Guid purchaseNumber, int movieId, int userId, decimal price)
        {
            var purchaseRequst = new PurchaseRequestModel()
            {
                MovieId = movieId,
                PurchaseNumber = purchaseNumber,
                Price = price
            };
            await _userService.PurchaseMovie(purchaseRequst, userId);
            return Ok();
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
 
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {

            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
        }

        [HttpGet]
        [Route("check-movie-favorite/(movieId)")]
        public async Task<IActionResult> FavoriteExists(int id, int movieId)
        {
            var favoriteExist = await _userService.FavoriteExists(id, movieId);
            if(favoriteExist)
            return Ok(favoriteExist);
            return NotFound(new { errorMessage = "The movies is not favorite" });
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview(ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieRevies(reviewRequest);
            return Ok();
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieRevies(reviewRequest);
            return Ok();
        }

        [HttpDelete]
        [Route("delete-review/(movieId)")]
        public async Task<IActionResult> DeleteReview(int userId, int movieId)
        {
            await _userService.DeleteMovieRevies(userId,movieId);
            return Ok();
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> MoviesByPurchase(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _userService.GetAllPurchasesForUser(id, pageSize, pageNumber);
            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("check-movie-purchased/(movieId)")]
        public async Task<IActionResult> PuchaseExists(PurchaseRequestModel purchaseRequest, int userId)
        {
            var favoriteExist = await _userService.IsMoviePurchased(purchaseRequest, userId);
            if (favoriteExist)
                return Ok(favoriteExist);
            return NotFound(new { errorMessage = "The movies didn't purchase yet" });
        }


        [HttpGet]
        [Route("favoritess")]
        public async Task<IActionResult> MoviesByFavorite(int id, int pageSize = 30, int pageNumber = 1)
        {

            var pagedMovies = await _userService.GetAllFavoritesForUser(id, pageSize, pageNumber);
            return Ok(pagedMovies);
        }

    }
}
