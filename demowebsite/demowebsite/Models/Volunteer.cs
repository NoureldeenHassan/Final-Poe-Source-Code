using Microsoft.AspNetCore.Mvc;

namespace demowebsite.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TaskAssigned { get; set; }
        public DateTime TaskDate { get; set; }
    }
}
