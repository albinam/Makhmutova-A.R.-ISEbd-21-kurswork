using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class TravelTour
    {
        public int Id { get; set; }
        [Required]
        public int TravelId { get; set; }
        [Required]
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Travel Travel { get; set; }
    }
}
