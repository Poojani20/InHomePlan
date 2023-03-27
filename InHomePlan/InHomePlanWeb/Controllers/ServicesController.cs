using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }
    }
}
