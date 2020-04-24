using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    public class TourBindingModel
    {
        public int? Id { get; set; }
        public string TourName { get; set; }
        public string TypeOfAllocation { get; set; }
        public int Duration { get; set; }
        public string Country { get; set; }
        public decimal Cost { get; set; }
    }
}
