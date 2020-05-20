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
                        TypeOfAllocation = elem.Element("TypeOfAllocation").Value,
                        Country = elem.Element("Country").Value,
                    });
                }
            }
            return list;
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            return Tours
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new TourViewModel
            {
                Id = rec.Id,
                TourName = rec.TourName,
                TypeOfAllocation = rec.TypeOfAllocation,
                Country = rec.Country
            })
            .ToList();
        }
    }
}
