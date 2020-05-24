using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Travel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int FinalCost { get; set; }
        [Required]
        public int Duration { get; set; }
        public DateTime DateOfBuying { get; set; }
        public TravelStatus Status { get; set; }
        [ForeignKey("TravelId")]
        public virtual List<TravelTour> TravelTours { get; set; }
        [Required]
        [ForeignKey("TravelId")]
        public List<Payment> Payments { get; set; }
        public Client Client { get; set; }
      
    }
}
