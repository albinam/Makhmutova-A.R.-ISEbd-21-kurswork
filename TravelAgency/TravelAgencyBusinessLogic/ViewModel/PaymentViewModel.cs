using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModel
{
    [DataContract]
    public class PaymentViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int TravelId { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime DatePayment { get; set; }
        [DataMember]
        [DisplayName("Сумма оплаты")]
        public decimal Sum { get; set; }
    }
}
