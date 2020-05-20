using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public string TypeOfAllocation { get; set; }
        [Required]
        public string Country { get; set; }
      //  [ForeignKey("TourId")]
       // public virtual List<TravelTour> TravelTours { get; set; }
    }
}
