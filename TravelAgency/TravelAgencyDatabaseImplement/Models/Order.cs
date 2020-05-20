using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TravelId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int FinalCost { get; set; }
        [Required]
        public TravelStatus Status { get; set; }
        [Required]
        public DateTime DateOfBuying { get; set; }
        [Required]
        public bool IsCredit { get; set; }
        public List<Payment> Payments { get; set; }
        public virtual Travel Travel { get; set; }
        public Client Client { get; set; }
    }
}
