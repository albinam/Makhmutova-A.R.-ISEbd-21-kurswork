using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyWebClient.Models
{
    public class TravelModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfBuying { get; set; }
        public int FinalCost { get; set; }
        public int LeftSum { get; set; }
        public TravelStatus Status { get; set; }
        public List<TravelTourModel> TravelTours { get; set; }
    }
}
