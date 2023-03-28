using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Application1()
        {
            return View();
        }
        public IActionResult Application2() 
        { 
            return View();
        }
    }
}
