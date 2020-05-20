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
        [Required]
        public string TravelName { get; set; }
        [Required]
        public int FinalCost { get; set; }
        [Required]
        public int Duration { get; set; }
        [ForeignKey("TravelId")]
        public virtual List<TravelTour> TravelTours { get; set; }
    }
}
