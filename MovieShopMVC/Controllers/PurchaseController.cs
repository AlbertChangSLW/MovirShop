using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IUserService _userService;

        public PurchaseController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost]
        //public async Task<IActionResult> PurchaseMovie(Guid guid)
        [HttpGet]
        public async Task<IActionResult> PurchaseMovie(Guid purchaseNumber, int movieId, int userId, decimal price)
        //public IActionResult PurchaseMovie(Guid purchaseNumber, int movieId, int userId, decimal price)
        {
            var purchaseRequst = new PurchaseRequestModel()
            {
                MovieId = movieId,
                PurchaseNumber = purchaseNumber,
                Price = price
            };
            await _userService.PurchaseMovie(purchaseRequst, userId);
            return RedirectToAction("MoviesByPurchase" , new {id = userId});
        }

        [HttpGet]
        public async Task<IActionResult> MoviesByPurchase(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _userService.GetAllPurchasesForUser(id, pageSize, pageNumber);
            return View(pagedMovies);
        }
        //[HttpGet]
        //public IActionResult PurchaseMovie()
        //{
        //    //var purchaseRequst = new PurchaseRequestModel()
        //    //{
        //    //    MovieId = movieId,
        //    //    PurchaseNumber = purchaseNumber,
        //    //    Price = price
        //    //};
        //    //await _userService.PurchaseMovie(purchaseRequst, userId);
        //    return View();
        //}
    }
}
