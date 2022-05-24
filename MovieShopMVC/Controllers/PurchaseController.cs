using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class PurchaseController : Controller
    {
        [HttpPost]
        //public async Task<IActionResult> PurchaseMovie(Guid guid)
        [HttpPost]
        public IActionResult Purchase(Guid purchaseNumber, int movieId, int userId)
        {

            return LocalRedirect("~/");
        }
    }
}
