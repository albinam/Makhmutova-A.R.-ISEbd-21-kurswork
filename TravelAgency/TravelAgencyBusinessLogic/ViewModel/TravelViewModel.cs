using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.ViewModel
{
    [DataContract]
    public class TravelViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }
        [DataMember]
        [DisplayName("Название путешествия")]
        public string TravelName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal FinalCost { get; set; }
        [DataMember]
        [DisplayName("Длительность")]
        public int Duration { get; set; }
         [DataMember]
        [DisplayName("Кредит")]
        public bool IsCredit { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateOfBuying { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public TravelStatus Status { get; set; }
        [DataMember]
        public List<TravelTourViewModel> TravelTours { get; set; }
        [DataMember]
        public List<PaymentViewModel> Payments { get; set; }
    }
}
