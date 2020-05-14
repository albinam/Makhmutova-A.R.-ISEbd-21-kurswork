using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly ITravelLogic TravelLogic;
        private readonly object locker = new object();
        public MainLogic(ITravelLogic TravelLogic)
        {
            this.TravelLogic = TravelLogic;
        }
        public void CreateTravel(CreateTravelBindingModel model)
        {
            TravelLogic.CreateOrUpdate(new TravelBindingModel
            {
                DateOfBuying = DateTime.Now,
                Id=model.TravelId,
                ClientId = model.ClientId,
                Duration = model.Duration,
                FinalCost = model.FinalCost,
                Status =TravelStatus.Принят,
                IsCredit = model.IsCredit, 
            });
        }
    }
}