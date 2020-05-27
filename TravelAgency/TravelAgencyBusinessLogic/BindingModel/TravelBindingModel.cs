using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModel
{
    [DataContract]
    public class TravelBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public DateTime DateOfBuying { get; set; }
        [DataMember]
        public int FinalCost { get; set; }
        [DataMember]
        public int LeftSum { get; set; }
        [DataMember]
        public TravelStatus Status { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public List<TravelTourBindingModel> TravelTours { get; set; }
    }
}
