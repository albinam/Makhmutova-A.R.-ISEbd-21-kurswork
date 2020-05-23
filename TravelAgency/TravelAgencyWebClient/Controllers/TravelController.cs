using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyWebClient.Models;

namespace TravelAgencyWebClient.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelLogic _travelLogic;
        private readonly ITourLogic _tourLogic;

        public TravelController(ITravelLogic travelLogic, ITourLogic tourLogic)
        {
            _travelLogic = travelLogic;
            _tourLogic = tourLogic;
        }

        public IActionResult Travel()
        {
            var travels = _travelLogic.Read(new TravelBindingModel
            {
                ClientId = Program.Client.Id
            });
            var travelModels = new List<TravelModel>();
            foreach (var travel in travels)
            {
                var tours = new List<TravelTourModel>();
                foreach (var tour in travel.TravelTours)
                {
                    var tourData = _tourLogic.Read(new TourBindingModel
                    {
                        Id = tour.TourId
                    }).FirstOrDefault();

                    if (tourData != null)
                    {
                        tours.Add(new TravelTourModel
                        {
                            TourName = tourData.TourName,
                            Country = tourData.Country,
                            TypeOfAllocation = tourData.TypeOfAllocation,
                            Count = tour.Count,
                            Cost = tour.Count * tourData.Cost,
                            Duration = tourData.Duration * tour.Count
                        });
                    }
                }
                travelModels.Add(new TravelModel
                {
                    Id = travel.Id,
                    DateOfBuying = travel.DateOfBuying,
                    Status = travel.Status,
                    Duration = travel.Duration,
                    FinalCost = travel.FinalCost,
                    TravelTours = tours
                });
            }
            ViewBag.Travels = travelModels;
            return View();
        }
        public IActionResult CreateTravel()
        {
            ViewBag.TravelTours = _tourLogic.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateTravel(CreateTravelModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TravelTours = _tourLogic.Read(null);
                return View(model);
            }

            var travelTours = new List<TravelTourBindingModel>();

            foreach (var tour in model.TravelTours)
            {
                if (tour.Value > 0)
                {
                    travelTours.Add(new TravelTourBindingModel
                    {
                        TourId = tour.Key,
                        Count = tour.Value
                    });
                }
            }
            _travelLogic.CreateOrUpdate(new TravelBindingModel
            {
                ClientId = Program.Client.Id,
                DateOfBuying = DateTime.Now,
                Status = TravelStatus.Принят,
                FinalCost = CalculateSum(travelTours),
                Duration = CalculateDuration(travelTours),
                TravelTours = travelTours
            });
            return RedirectToAction("Travel");
        }
        private int CalculateSum(List<TravelTourBindingModel> travelTours)
        {
            int sum = 0;

            foreach (var tour in travelTours)
            {
                var tourData = _tourLogic.Read(new TourBindingModel { Id = tour.TourId }).FirstOrDefault();

                if (tourData != null)
                {
                    for (int i = 0; i < tour.Count; i++)
                        sum += tourData.Cost;
                }
            }
            return sum;
        }
        private int CalculateDuration(List<TravelTourBindingModel> travelTours)
        {
            int duration = 0;
            foreach (var tour in travelTours)
            {
                var tourData = _tourLogic.Read(new TourBindingModel { Id = tour.TourId }).FirstOrDefault();
                if (tourData != null)
                {
                    for (int i = 0; i < tour.Count; i++)
                        duration += tourData.Duration;
                }
            }
            return duration;
        }
    }
}