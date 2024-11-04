using demowebsite.Controllers;
using demowebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demowebsite.Testing.IntegrationTests
{
    [TestClass]
    public class DonationControllerIntegrationTests : IntegrationTestBase
    {
        private DonationController _controller;

        [TestInitialize]
        public void TestSetup()
        {
            _controller = new DonationController(_context);
        }

        [TestMethod]
        public async Task CreateDonation_ValidModel_AddsToDatabase()
        {
            // Arrange
            var model = new Donation
            {
                ItemName = "Blankets",
                Quantity = 10,
                DonorName = "Alice",
                ResourceType = "Clothing"
            };

            // Act
            var result = await _controller.Create(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(1, _context.Donations.Count());
            Assert.AreEqual("Blankets", _context.Donations.First().ItemName);
        }

        [TestMethod]
        public async Task GetDonations_ReturnsAllDonations()
        {
            // Arrange
            _context.Donations.Add(new Donation { ItemName = "Food", Quantity = 15, DonorName = "Bob", ResourceType = "Food" });
            _context.Donations.Add(new Donation { ItemName = "Water", Quantity = 20, DonorName = "Carol", ResourceType = "Water" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Index() as ViewResult;
            var donations = result?.Model as IEnumerable<Donation>;

            // Assert
            Assert.IsNotNull(donations);
            Assert.AreEqual(2, donations.Count());
        }
    }
}
