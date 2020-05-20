using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModel
{
   public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public int TravelId { get; set; }
        public string TravelName { get; set; }
        public int Count { get; set; }
        public int FinalCost { get; set; }
        public bool IsCredit { get; set; }
        public DateTime DateOfBuying { get; set; }
        public TravelStatus Status { get; set; }
        public List<PaymentBindingModel> Payments { get; set; }
    }
}
