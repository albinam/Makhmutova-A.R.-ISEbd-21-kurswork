using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        Travel element = context.Travels.FirstOrDefault(rec =>
                       rec.TravelName == model.TravelName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть путешествие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Travels.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Travel();
                            context.Travels.Add(element);
                        }
                        element.DateOfBuying = model.DateOfBuying;
                        element.ClientId = model.ClientId;
                        element.Duration = model.Duration;
                        element.FinalCost = model.FinalCost;
                        element.Status = model.Status;
                        element.IsCredit = model.IsCredit;
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
                            transaction.Commit();
                        }
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.TravelTours.RemoveRange(context.TravelTours.Where(rec =>
                        rec.TravelId == model.Id));
                        Travel element = context.Travels.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Travels.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
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
        public List<TravelViewModel> Read(TravelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Travels.Where(rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue)
                .Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    DateOfBuying = rec.DateOfBuying,
                    TravelName = rec.TravelName,
                    ClientId = rec.ClientId,
                    Duration = rec.Duration,
                    FinalCost = rec.FinalCost,
                    Status = rec.Status,
                    IsCredit = rec.IsCredit,
                    ClientFIO = rec.Client.ClientFIO,
                    TravelTours = context.TravelTours
                 .Where(recCI => recCI.TravelId == rec.Id)
                 .Select(recCI => new TravelTourViewModel
                   {
                       Id = recCI.Id,
                       TravelId = recCI.TravelId,
                       TourId = recCI.TourId,
                       Count = recCI.Count
                    })
                    .ToList()
                })
                .ToList();
            }
        }
    }
}