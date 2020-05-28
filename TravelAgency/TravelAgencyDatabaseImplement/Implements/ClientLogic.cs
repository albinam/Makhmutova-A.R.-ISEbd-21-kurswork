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
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Client element = model.Id.HasValue ? null : new Client();                
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.Email = model.Email;
                element.Login = model.Login;
                element.ClientFIO = model.ClientFIO;
                element.PhoneNumber = model.PhoneNumber;
                element.Block = model.Block;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }
        public void Delete(ClientBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Clients
                 .Where(rec => model == null
                   || rec.Id == model.Id
                   || rec.Login == model.Login && rec.Password == model.Password)
               .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    Login=rec.Login,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                    Password = rec.Password,
                    PhoneNumber=rec.PhoneNumber,
                    Block=rec.Block
                })
                .ToList();
            }
        }
    }
}