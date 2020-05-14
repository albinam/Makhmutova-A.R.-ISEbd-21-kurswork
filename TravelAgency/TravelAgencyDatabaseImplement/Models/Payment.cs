using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Payment
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public int TravelId { get; set; }
        [Required]
        public DateTime DatePayment { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public virtual Travel Travel { get; set; }
    }
}
