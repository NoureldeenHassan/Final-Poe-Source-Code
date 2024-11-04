using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demowebsite.Testing.IntegrationTests
{
    [TestClass]
    public abstract class IntegrationTestBase
    {
        protected ApplicationDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "IntegrationTestDatabase")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}
