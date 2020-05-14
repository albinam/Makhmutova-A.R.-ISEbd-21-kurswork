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
                Travel element;
                if (model.Id.HasValue)
                {
                    element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
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
                element.Id = model.Id;
                element.DateOfBuying = model.DateOfBuying;
                element.DateStart = model.DateStart;
                element.ClientId = model.ClientId.Value;
                element.Duration = model.Duration;
                element.FinalCost = model.FinalCost;
                element.Status = model.Status;
                element.IsCredit = model.IsCredit;
                context.SaveChanges();
            }
        }
        public void Delete(TravelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Travel element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
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
                return context.Travels.Where(rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue)
                .Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    DateOfBuying = rec.DateOfBuying,
                    TravelName = rec.TravelName,
                    DateStart = rec.DateStart,
                    ClientId = rec.ClientId,
                    Duration = rec.Duration,
                    FinalCost = rec.FinalCost,
                    Status = rec.Status,
                    IsCredit = rec.IsCredit,
                    ClientFIO = rec.Client.ClientFIO,
                })
                .ToList();
            }
        }
    }
}