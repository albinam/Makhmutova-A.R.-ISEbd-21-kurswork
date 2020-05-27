using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModel;
using TravelAgencyDatabaseImplement.Models;
using TravelAgencyWebClient.Models;

namespace TravelAgencyWebClient.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelLogic _travelLogic;
        private readonly ITourLogic _tourLogic;
        private readonly IPaymentLogic _paymentLogic;
        private readonly ReportLogic _reportLogic;
        public TravelController(ITravelLogic travelLogic, ITourLogic tourLogic, IPaymentLogic paymentLogic, ReportLogic reportLogic)
        {
            _travelLogic = travelLogic;
            _tourLogic = tourLogic;
            _paymentLogic = paymentLogic;
            _reportLogic = reportLogic;
        }

        public IActionResult Travel()
        {
            ViewBag.Travels = _travelLogic.Read(new TravelBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Travel(ReportModel model)
        {
            var paymentList =new List<PaymentViewModel>();
            var travels = new List<TravelViewModel>();
            travels = _travelLogic.Read(new TravelBindingModel
            {
                ClientId = Program.Client.Id,
                DateFrom = model.From,
                DateTo = model.To
            });
            var payments = _paymentLogic.Read(null);
            foreach(var travel in travels)
            {
                foreach(var payment in payments)
                {
                    if (payment.ClientId == Program.Client.Id && payment.TravelId == travel.Id)
                        paymentList.Add(payment);
                }
            }
            ViewBag.Payments = paymentList;
            ViewBag.Travels = travels;
            string fileName = "D:\\data\\pdfreport.pdf";
            if (model.SendMail)
            {
                _reportLogic.SaveTravelPaymentsToPdfFile(fileName, new TravelBindingModel
                {
                    ClientId = Program.Client.Id,
                    DateFrom = model.From,
                    DateTo = model.To
                }, Program.Client.Email);
            }
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
            if (travelTours.Count == 0)
            {
                ViewBag.Products = _tourLogic.Read(null);
                ModelState.AddModelError("", "Ни один тур не выбран");
                return View(model);
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
        public IActionResult Payment(int id)
        {
            var travel = _travelLogic.Read(new TravelBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.Travel = travel;
            ViewBag.LeftSum = CalculateLeftSum(travel);
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            TravelViewModel travel = _travelLogic.Read(new TravelBindingModel
            {
                Id = model.TravelId
            }).FirstOrDefault();
            int leftSum = CalculateLeftSum(travel);
            if (!ModelState.IsValid)
            {
                ViewBag.Travel = travel;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            if (leftSum < model.Sum)
            {
                ViewBag.Travel = travel;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            _paymentLogic.CreateOrUpdate(new PaymentBindingModel
            {
                TravelId = travel.Id,
                ClientId = Program.Client.Id,
                DatePayment = DateTime.Now,
                Sum = model.Sum
            });
            leftSum -= model.Sum;
            _travelLogic.CreateOrUpdate(new TravelBindingModel
            {
                Id = travel.Id,
                ClientId = travel.ClientId,
                DateOfBuying = travel.DateOfBuying,
                Duration = travel.Duration,
                Status = leftSum > 0 ? TravelStatus.Оплачен_не_полностью : TravelStatus.Оплачен,
                FinalCost = travel.FinalCost,
                TravelTours = travel.TravelTours.Select(rec => new TravelTourBindingModel
                {
                    Id = rec.Id,
                    TravelId = rec.TravelId,
                    TourId = rec.TourId,
                    Count = rec.Count
                }).ToList()
            });
            return RedirectToAction("Travel");
        }

        private int CalculateLeftSum(TravelViewModel travel)
        {
            int sum = travel.FinalCost;
            int paidSum = _paymentLogic.Read(new PaymentBindingModel
            {
                TravelId = travel.Id
            }).Select(rec => rec.Sum).Sum();

            return sum - paidSum;
        }
        public IActionResult SendWordReport(int id)
        {
            var travel = _travelLogic.Read(new TravelBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\data\\" + travel.Id + ".docx";
            _reportLogic.SaveTravelToursToWordFile(fileName, travel, Program.Client.Email);
            return RedirectToAction("Travel");
        }
        public IActionResult SendExcelReport(int id)
        {
            var travel = _travelLogic.Read(new TravelBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\data\\" + travel.Id + ".xlsx";
            _reportLogic.SaveTravelToursToExcelFile(fileName, travel, Program.Client.Email);
            return RedirectToAction("Travel");
        }
    }
}