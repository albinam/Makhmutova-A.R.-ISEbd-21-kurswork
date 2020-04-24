using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModel
{
    [DataContract]
    public class CreateTravelBindingModel
    {
        [DataMember]
        public int TravelId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public decimal FinalCost { get; set; }
    }
}
