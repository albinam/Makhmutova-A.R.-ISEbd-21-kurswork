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
        public DateTime DateCreate { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> TravelTours { get; set; }
    }
}
