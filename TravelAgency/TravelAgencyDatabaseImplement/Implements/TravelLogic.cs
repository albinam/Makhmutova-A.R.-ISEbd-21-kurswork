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
                        element.TravelName = model.TravelName;
                        element.Duration = model.Duration;
                        element.FinalCost = model.FinalCost;
                        if (model.Id.HasValue)
                        {
                            var TravelTours = context.TravelTours.Where(rec
                           => rec.TravelId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.TravelTours.RemoveRange(TravelTours.Where(rec =>
                            !model.TravelTours.Contains(new TravelTourBindingModel { TourId = rec.TourId })).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateTour in TravelTours)
                            {
                                updateTour.Count =
                               model.TravelTours[updateTour.TourId].Count;
                                model.TravelTours.Remove(new TravelTourBindingModel { TourId = updateTour.TourId });
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var tt in model.TravelTours)
                        {
                            context.TravelTours.Add(new TravelTour
                            {
                                TravelId = element.Id,
                                TourId = tt.TourId,
                                Count = tt.Count
                            });
                            context.SaveChanges();
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
                    TravelName = rec.TravelName,
                    Duration = rec.Duration,
                    FinalCost = rec.FinalCost,
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
                })
                .ToList();
            }
        }
    }
}