using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    [DataContract]
    public class TourBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string TourName { get; set; }
        [DataMember]
        public string TypeOfAllocation { get; set; }
        [DataMember]
        public string Country { get; set; }
    }
}
