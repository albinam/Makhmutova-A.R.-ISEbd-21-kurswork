using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModel;
using TravelAgencyWebClient.Models;

namespace TravelAgencyWebTravel.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourLogic _tour;
        public TourController(ITourLogic tour)
        {
            _tour = tour;
        }
        public IActionResult Tour()
        {
            ViewBag.Tours = _tour.Read(null);
            return View();
        }
    }
}