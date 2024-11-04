using demowebsite.Models;
using Microsoft.AspNetCore.Mvc;
using demowebsite.Controllers;


namespace demowebsite.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var donations = await Task.FromResult(_context.Donations.ToList());
            return View(donations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Donation model)
        {
            if (ModelState.IsValid)
            {
                // Map DonationViewModel to DonationsModel
                var donation = new Donation
                {
                    ItemName = model.ItemName,
                    Quantity = model.Quantity,
                    DonorName = model.DonorName,
                    ResourceType = model.ResourceType
                };

                // Add to database
                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                // Redirect after successful creation
                return RedirectToAction("Index", "Home");
            }

            // Return view with model if invalid
            return View(model);
        }

        [HttpGet]
        public IActionResult Donate() => View();

        [HttpPost]
        public async Task<IActionResult> Donate(Donation model)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
