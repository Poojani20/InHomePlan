using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
