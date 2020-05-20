using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);
        void CreateOrUpdate(OrderBindingModel model);
        void Delete(OrderBindingModel model);
    }
}
