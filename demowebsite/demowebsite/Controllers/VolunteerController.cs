using demowebsite.Models;
using Microsoft.AspNetCore.Mvc;
using demowebsite.Controllers;

namespace demowebsite.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Volunteer model)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
