using demowebsite.Models;
using Microsoft.AspNetCore.Mvc;
using demowebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace demowebsite.Controllers
{
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Incident> incidents = await _context.Incidents.ToListAsync();
            return View(incidents);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Incident/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Incidents.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Adjust according to your action
            }
            return View(incident);
        }

        [HttpGet]
        public IActionResult Report() => View();

        [HttpPost]
        public async Task<IActionResult> Report(Incident model)
        {
            if (ModelState.IsValid)
            {
                _context.Incidents.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
    