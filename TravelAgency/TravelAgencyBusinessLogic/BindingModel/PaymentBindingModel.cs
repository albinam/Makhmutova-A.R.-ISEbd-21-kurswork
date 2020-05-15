using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    [DataContract]
    public class PaymentBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int TravelId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public DateTime DatePayment { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
