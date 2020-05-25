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
                return context.Travels.Where(rec => rec.Id == model.Id || rec.ClientId == model.ClientId)
                .Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    Duration = rec.Duration,
                    FinalCost = rec.FinalCost,
                    DateOfBuying = rec.DateOfBuying,
                    Status=rec.Status,
                    TravelTours = context.TravelTours
                .Where(recTT => recTT.TravelId == rec.Id)
                .Select(recTT => new TravelTourViewModel
                {
                    Id = recTT.Id,
                    TravelId = recTT.TravelId,
                    TourId = recTT.TourId,
                    Count = recTT.Count
                })
                .ToList(),
                    Payments = context.Payments
                    .Where(recP => recP.TravelId == rec.Id)
                .Select(recP => new PaymentViewModel
                {
                    Id = recP.Id,
                    TravelId = recP.TravelId,
                    ClientId = recP.ClientId,
                    Sum = recP.Sum,
                    DatePayment=recP.DatePayment
                })
                .ToList(),
                })
            .ToList();
            }
        }
    }
}
