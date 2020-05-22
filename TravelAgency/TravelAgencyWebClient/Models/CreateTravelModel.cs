using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyWebClient.Models
{
    public class CreateTravelModel
    {
        public Dictionary<int, int> TravelTours { get; set; }
        public List<TourViewModel> AllTours { get; set; }
    }
}
