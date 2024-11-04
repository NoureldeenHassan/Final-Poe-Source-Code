using demowebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using demowebsite.Models; // This should point to where your models (e.g., Donation) are defined


namespace demowebsite
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Donation> Donations { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }


    }


}

