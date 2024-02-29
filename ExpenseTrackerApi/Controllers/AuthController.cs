using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
