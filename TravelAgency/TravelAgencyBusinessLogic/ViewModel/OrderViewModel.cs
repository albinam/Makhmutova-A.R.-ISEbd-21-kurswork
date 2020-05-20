using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }
        public int TravelId { get; set; }
        [DataMember]
        [DisplayName("Путешествие")]
        public string TravelName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public int FinalCost { get; set; }
        [DataMember]
        [DisplayName("Кредит")]
        public bool IsCredit { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateOfBuying { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public TravelStatus Status { get; set; }
        public List<PaymentViewModel> Payments { get; set; }
    }
}
