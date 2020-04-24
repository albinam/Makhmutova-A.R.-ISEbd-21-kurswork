using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    public class PaymentBindingModel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public DateTime DatePayment { get; set; }
        public decimal Sum { get; set; }
    }
}
