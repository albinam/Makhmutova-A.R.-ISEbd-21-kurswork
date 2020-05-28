using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
    public class TourLogic : ITourLogic
    {
        private readonly string TourFileName = "C://Users//Альбина//Desktop//универ//data//Tour.xml";
        public List<Tour> Tours { get; set; }
        public TourLogic()
        {
            Tours = LoadTours();
        }
        private List<Tour> LoadTours()
        {
            var list = new List<Tour>();
            if (File.Exists(TourFileName))
            {
                XDocument xDocument = XDocument.Load(TourFileName);
                var xElements = xDocument.Root.Elements("Tour").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Tour
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TourName = elem.Element("TourName").Value,
                        Duration = Convert.ToInt32(elem.Element("Duration").Value),
                        Cost= Convert.ToInt32(elem.Element("Cost").Value),
                        TypeOfAllocation = elem.Element("TypeOfAllocation").Value,
                        Country = elem.Element("Country").Value,
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var tours = LoadTours();
            using (var context = new TravelAgencyDatabase())
            {
                foreach (var tour in tours)
                {
                    Tour element = context.Tours.FirstOrDefault(rec => rec.Id == tour.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Tour();
                        context.Tours.Add(element);
                    }
                    element.TourName = tour.TourName;
                    element.Duration = tour.Duration;
                    element.TypeOfAllocation = tour.TypeOfAllocation;
                    element.Country = tour.Country;
                    element.Cost = tour.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            SaveToDatabase();
            return Tours
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new TourViewModel
            {
                Id = rec.Id,
                TourName = rec.TourName,
                Duration=rec.Duration,
                Cost=rec.Cost,
                TypeOfAllocation = rec.TypeOfAllocation,
                Country = rec.Country
            })
            .ToList();
        }
    }
}
