using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyBusinessLogic.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<TourViewModel> Tours { get; set; }
    }
}
