using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyWebClient.Models
{
    public class TourModel
    {
        public int Id { get; set; }
        [DisplayName("Название тура")]
        public string TourName { get; set; }
        [DisplayName("Длительность")]
        public int Duration { get; set; }
        [DisplayName("Цена")]
        public int Cost { get; set; }
        [DisplayName("Страна")]
        public string Country { get; set; }
        [DisplayName("Тип размещения")]
        public string TypeOfAllocation { get; set; }
    }
}
