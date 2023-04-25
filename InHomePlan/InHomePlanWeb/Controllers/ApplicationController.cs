using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace InHomePlanWeb.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ApplicationController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Application1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Application1(Application application)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                application.User = user;
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(application);
        }
    }
}
