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
    public class TourLogic : ITourLogic
    {
        public void CreateOrUpdate(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Tour element = context.Tours.FirstOrDefault(rec =>
               rec.TourName == model.TourName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть тур с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Tours.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Tour();
                    context.Tours.Add(element);
                }
                element.TourName = model.TourName;
                element.Country = model.Country;
                element.Duration = model.Duration;
                element.Cost = model.Cost;
                element.TypeOfAllocation = model.TypeOfAllocation;
                context.SaveChanges();
            }
        }
        public void Delete(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Tour element = context.Tours.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Tours.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new TourViewModel
                {
                    Id = rec.Id,
                    TourName = rec.TourName,
                    Duration=rec.Duration,
                    Cost=rec.Cost,
                    TypeOfAllocation=rec.TypeOfAllocation,
                    Country=rec.Country            
                })
                .ToList();
            }
        }
    }
}