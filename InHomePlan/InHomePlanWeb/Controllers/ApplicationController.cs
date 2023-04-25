using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;

namespace InHomePlanWeb.Controllers
{
    public class ApplicationController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
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
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(application);
        }

    }
}
