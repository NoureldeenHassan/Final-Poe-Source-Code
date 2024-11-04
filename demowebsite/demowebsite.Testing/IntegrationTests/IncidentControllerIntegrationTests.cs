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
    public class IncidentControllerIntegrationTests : IntegrationTestBase
    {
        private IncidentController _controller;

        [TestInitialize]
        public void TestSetup()
        {
            _controller = new IncidentController(_context);
        }

        [TestMethod]
        public async Task CreateIncident_ValidModel_AddsToDatabase()
        {
            // Arrange
            var model = new Incident
            {
                Description = "Flood in town",
                Location = "Downtown",
                Severity = "High",
                Type = "Natural Disaster" // Provide a valid type
            };

            // Act
            var result = await _controller.Create(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(1, _context.Incidents.Count());
            Assert.AreEqual("Flood in town", _context.Incidents.First().Description);
        }

        [TestMethod]
        public async Task CreateIncident_ValidIncident_ReturnsRedirect()
        {
            // Arrange
            var incident = new Incident
            {
                Description = "Flood in area",
                Date = DateTime.UtcNow,
                Severity = "High", // If you have this property
                Location = "City Center", // Provide a valid location
                Type = "Natural Disaster" // Provide a valid type
            };

            // Act
            var result = await _controller.Create(incident) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName); // Check if it redirects to Index
        }

        [TestMethod]
        public async Task Index_ReturnsViewWithIncidents()
        {
            // Arrange
            // Add some test incidents to the in-memory database if needed

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Incident>));
        }

        [TestMethod]
        public async Task GetIncidents_ReturnsAllIncidents()
        {
            // Arrange
            var incident = new Incident
            {
                Description = "Flood in area",
                Date = DateTime.UtcNow,
                Location = "City Center", // Example location
                Type = "Natural Disaster", // Example type, ensure it's a valid entry
                Severity = "High" // Make sure to set the Severity
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync(); // Save changes to the in-memory database

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            var model = result.Model as List<Incident>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count); // Ensure there's one incident
            Assert.AreEqual("Flood in area", model[0].Description); // Validate the incident
        }
    }
}
