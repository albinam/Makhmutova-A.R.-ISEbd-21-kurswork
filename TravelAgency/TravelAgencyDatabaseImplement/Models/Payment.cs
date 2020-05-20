﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int TravelId { get; set; }
        [Required]
        public DateTime DatePayment { get; set; }
        [Required]
        public int Sum { get; set; }
        public virtual Order Order { get; set; }
        public virtual Client Client { get; set; }
    }
}
