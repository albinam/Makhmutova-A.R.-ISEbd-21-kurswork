using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.Enums
{
    public enum OrderStatus
    {
        Принят = 0,   
        Оплачен_не_полностью = 1,
        Оплачен = 2
    }
}