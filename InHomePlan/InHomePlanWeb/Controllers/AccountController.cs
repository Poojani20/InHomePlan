using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        { 
            return View();  
        } 
    }
}
