using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Travel
    {
        public int? Id { get; set; }
        [Required]
        public int? ClientId { get; set; }
        [Required]
        public string TravelName { get; set; }
        [Required]
        public decimal FinalCost { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public bool IsCredit { get; set; }
        [Required]
        public DateTime DateOfBuying { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public TravelStatus Status { get; set; }
        public virtual List<TravelTour> TravelTours { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public Client Client { get; set; }
    }
}
