using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModel
{
    public class TravelBindingModel
    {
        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public string TravelName { get; set; }
        public int Duration { get; set; }
        public bool IsCredit { get; set; }
        public decimal FinalCost { get; set; }
        public DateTime DateOfBuying { get; set; }
        public DateTime DateStart { get; set; }
        public TravelStatus Status { get; set; }
        public List<TravelTourBindingModel> TravelTours { get; set; }
        public List<PaymentBindingModel> Payments { get; set; }
    }
}
