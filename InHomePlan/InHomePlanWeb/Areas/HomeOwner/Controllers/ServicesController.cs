using InHomePlanWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    //[Authorize(Roles =SD.Role_Admin)]
    public class ServicesController : Controller
    {
        [Area("HomeOwner")]
        public IActionResult Services()
        {
            return View();
        }
    }
}
