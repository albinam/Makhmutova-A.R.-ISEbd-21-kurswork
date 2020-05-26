using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }   
        public List<TravelViewModel> Travels { get; set; }
        public Dictionary<int, List<PaymentViewModel>> Payments { get; set; }
    }
}
