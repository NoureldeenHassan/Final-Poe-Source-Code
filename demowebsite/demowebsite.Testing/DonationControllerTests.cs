using demowebsite.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using demowebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace demowebsite.Testing
{
    [TestClass]
    public class DonationControllerTests
    {
        private DonationController _controller;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging()// Use in-memory database
                .Options;

            // Create a new context using the in-memory database
            _context = new ApplicationDbContext(options);

            var donation = new Donation
            {
                ItemName = "Clothing",
                Quantity = 5,
                DonorName = "Jane Doe",
                ResourceType = "Clothes"  // Set ResourceType to avoid missing required field
            };

            // Seed the database with test data if needed
            _context.Donations.Add(new Donation { ItemName = "Clothes", Quantity = 5, DonorName = "Jane Smith" , ResourceType = "Clothes" });
            _context.SaveChanges();

            // Create the controller with the in-memory context
            _controller = new DonationController(_context);
        }



        [TestMethod]
        public async Task Create_ValidModel_RedirectsToHome()
        {
            // Arrange
            var model = new Donation
            {
                Id = 1,
                ItemName = "Food",
                Quantity = 10,
                DonorName = "John Doe",
                ResourceType = "Food"
            };

            // Act
            var result = await _controller.Create(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("ItemName", "Required");
            var model = new Donation(); // Invalid model

            // Act
            var result = await _controller.Create(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public async Task Create_AddsDonationToDatabase()
        {
            // Arrange
            var model = new Donation
            {
                Id = 1,
                ItemName = "Clothes",
                Quantity = 5,
                DonorName = "Jane Smith",
                ResourceType = "Food"
            };

            // Act
            await _controller.Create(model);
            var donations = await _context.Donations.ToListAsync();

            // Assert
            Assert.AreEqual(2, donations.Count); // Should now include the newly created donation
            var donation = donations.Last();
            Assert.AreEqual("Clothes", donation.ItemName);
        }


    }
}