using Microsoft.AspNetCore.Mvc;

namespace HousingIntel.Controllers
{
    public class HousingPricingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
