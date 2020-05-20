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
       
    }
}