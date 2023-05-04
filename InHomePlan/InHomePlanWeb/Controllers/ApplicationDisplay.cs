using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class ApplicationDisplay : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
