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
    public class PaymentLogic : IPaymentLogic
    {
        public void CreateOrUpdate(PaymentBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть платеж  с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Payments.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Payment();
                    context.Payments.Add(element);
                }
                element.TravelId = model.TravelId;
                element.ClientId = model.ClientId;
                element.Sum = model.Sum;
                element.DatePayment = model.DatePayment;
                context.SaveChanges();
            }
        }
        public void Delete(PaymentBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Payments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<PaymentViewModel> Read(PaymentBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Payments
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new PaymentViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    DatePayment = rec.DatePayment,
                    TravelId = rec.TravelId,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}

