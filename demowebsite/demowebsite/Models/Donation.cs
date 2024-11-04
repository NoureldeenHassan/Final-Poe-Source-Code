using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demowebsite.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public int Quantity { get; set; }
        public DateTime DonationDate { get; set; }

        public string DonorName { get; set; }

        public string ItemName { get; set; }




    }

}
