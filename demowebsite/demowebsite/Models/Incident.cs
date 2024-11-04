using Microsoft.AspNetCore.Mvc;

namespace demowebsite.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public string Severity { get; set; } // e.g., "Low", "Medium", "High"


    }
}

