using System;
using System.Collections.Generic;
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
        public decimal Cost { get; set; }
    }
}
