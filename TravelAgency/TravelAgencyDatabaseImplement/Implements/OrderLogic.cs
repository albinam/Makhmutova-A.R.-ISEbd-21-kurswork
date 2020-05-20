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
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Order element;
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }
                element.TravelId = model.TravelId == 0 ? element.TravelId : model.TravelId;
                element.Count = model.Count;
                element.DateOfBuying = model.DateOfBuying;
                element.ClientId = model.ClientId.Value;
                element.Status = model.Status;
                element.IsCredit = model.IsCredit;
                element.FinalCost = model.FinalCost;
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Orders.Where(rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue                  
                    || model.ClientId.HasValue && rec.ClientId == model.ClientId)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,      
                    TravelId = rec.TravelId,
                    DateOfBuying = rec.DateOfBuying,
                    IsCredit = rec.IsCredit,
                    Status = rec.Status,
                    Count = rec.Count,
                    FinalCost = rec.FinalCost,
                    ClientFIO = rec.Client.ClientFIO,                  
                    TravelName = rec.Travel.TravelName,
                    Payments = context.Payments
                 .Where(recP => recP.TravelId == rec.Id)
                 .Select(recP => new PaymentViewModel
                 {
                     Id = recP.Id,
                     TravelId = recP.TravelId,
                     DatePayment = recP.DatePayment,
                     Sum= recP.Sum
                 })
                    .ToList()
                })
                .ToList();
            }
        }
        public int GetBalance(int id)
        {
            using (var context = new TravelAgencyDatabase())
            {
                int balance = 0;
                Order rec = context.Orders.FirstOrDefault(rec1 => rec1.Id == id);
                if (rec != null)
                {
                    var order = new OrderViewModel
                    {
                        FinalCost = rec.FinalCost,
                        Payments = context.Payments
                     .Where(recP => recP.TravelId == rec.Id)
                    .Select(recP => new PaymentViewModel
                    {
                        Id = recP.Id,
                        TravelId = recP.TravelId,
                        DatePayment = recP.DatePayment,
                        Sum = recP.Sum
                    })
                    .ToList()
                    };
                    balance = order.FinalCost;
                    foreach (var Payment in order.Payments)
                    {
                        balance -= Payment.Sum;
                    }
                    return balance;
                }
                throw new Exception("Элемент не найден");
            }
        }
    }
}
