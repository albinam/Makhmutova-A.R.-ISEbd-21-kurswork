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
        public int TourName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
