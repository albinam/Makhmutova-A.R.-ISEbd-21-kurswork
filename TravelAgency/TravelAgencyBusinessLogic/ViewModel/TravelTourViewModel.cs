using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModel
{
    [DataContract]
    public class TravelTourViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? TravelId { get; set; }
        [DataMember]
        public int TourId { get; set; }
        [DataMember]
        public string TourName { get; set; }
        [DataMember]
        public int Cost { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public string TypeOfAllocation { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
