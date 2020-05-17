using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    [DataContract]
    public class TravelTourBindingModel
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
        public int Count { get; set; }
    }
}
