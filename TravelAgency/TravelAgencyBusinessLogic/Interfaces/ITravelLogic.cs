using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface ITravelLogic
    {
        List<TravelViewModel> Read(TravelBindingModel model);
    }
}
