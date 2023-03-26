using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
