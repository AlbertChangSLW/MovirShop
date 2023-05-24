using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

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
        public async Task<IActionResult> MoviesByPurchase(int pageSize = 30, int pageNumber = 1)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pagedMovies = await _userService.GetAllPurchasesForUser(userId, pageSize, pageNumber);

            if (pagedMovies.Count > 0)
                return View(pagedMovies);
            else return Redirect("~/");
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
