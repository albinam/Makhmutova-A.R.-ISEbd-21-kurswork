using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgencyWebClient.Models
{
    public class TravelTourModel
    {
        public string TourName { get; set; }
        public int Cost { get; set; }
        public int Duration { get; set; }
        public string TypeOfAllocation { get; set; }
        public string Country { get; set; }
        public int Count { get; set; }
    }
}
