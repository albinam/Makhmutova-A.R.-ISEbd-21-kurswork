using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModel;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class TravelLogic : ITravelLogic
    {
        public void CreateOrUpdate(TravelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Travel element = model.Id.HasValue ? null : new Travel();
                        if (model.Id.HasValue)
                        {
                            element = context.Travels.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            element.ClientId = model.ClientId;
                            element.DateOfBuying = model.DateOfBuying;
                            element.Duration = model.Duration;
                            element.FinalCost = model.FinalCost;
                            element.Status = model.Status;
                            context.SaveChanges();
                        }
                        else
                        {
                            element.ClientId = model.ClientId;
                            element.DateOfBuying = model.DateOfBuying;
                            element.Duration = model.Duration;
                            element.FinalCost = model.FinalCost;
                            element.Status = model.Status;
                            context.Travels.Add(element);
                            context.SaveChanges();
                            var groupTours = model.TravelTours
                               .GroupBy(rec => rec.TourId)
                               .Select(rec => new
                               {
                                   TourId = rec.Key,
                                   Count = rec.Sum(r => r.Count)
                               });

                            foreach (var groupTour in groupTours)
                            {
                                context.TravelTours.Add(new TravelTour
                                {
                                    TravelId = element.Id,
                                    TourId = groupTour.TourId,
                                    Count = groupTour.Count
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(TravelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Travel element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Travels.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<TravelViewModel> Read(TravelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Travels.Where(rec => rec.Id == model.Id || (rec.ClientId == model.ClientId) && (model.DateFrom==null && model.DateTo==null || rec.DateOfBuying >= model.DateFrom && rec.DateOfBuying <= model.DateTo))
                .Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    Duration = rec.Duration,
                    FinalCost = rec.FinalCost,
                    DateOfBuying = rec.DateOfBuying,
                    LeftSum =rec.FinalCost - context.Payments.Where(recP => recP.TravelId == rec.Id).Select(recP => recP.Sum).Sum(),
                    Status = rec.Status,
                    TravelTours = GetTravelTourViewModel(rec)
                })
            .ToList();
            }
        }
        public static List<TravelTourViewModel> GetTravelTourViewModel(Travel travel)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var TravelTours = context.TravelTours
                    .Where(rec => rec.TravelId == travel.Id)
                    .Include(rec => rec.Tour)
                    .Select(rec => new TravelTourViewModel
                    {
                        Id = rec.Id,
                        TravelId = rec.TravelId,
                        TourId = rec.TourId,
                        Count = rec.Count
                    }).ToList();
                foreach (var tour in TravelTours)
                {
                    var tourData = context.Tours.Where(rec => rec.Id == tour.TourId).FirstOrDefault();
                    tour.TourName = tourData.TourName;
                    tour.TypeOfAllocation = tourData.TypeOfAllocation;
                    tour.Country = tourData.Country;
                    tour.Duration = tourData.Duration;
                    tour.Cost = tourData.Cost;
                }
                return TravelTours;
            }
        }
    }
}
