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
            return Redirect("~/Movies/Details/" + purchaseRequst.MovieId);
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
