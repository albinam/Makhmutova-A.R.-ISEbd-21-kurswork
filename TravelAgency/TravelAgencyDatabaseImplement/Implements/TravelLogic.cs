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
        public List<Travel> Travels { get; set; }
        public List<TravelTour> TravelTours { get; set; }
        public TravelLogic()
        {
            Travels = LoadTravels();
            TravelTours = LoadTravelTours();
        }
        private readonly string TravelFileName = "C://Users//Альбина//Desktop//универ//data//Travel.xml";
        private readonly string TravelTourFileName = "C://Users//Альбина//Desktop//универ//data//TravelTour.xml";
        private List<Travel> LoadTravels()
        {
            var list = new List<Travel>();
            if (File.Exists(TravelFileName))
            {
                XDocument xDocument = XDocument.Load(TravelFileName);
                var xElements = xDocument.Root.Elements("Travel").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Travel
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TravelName = elem.Element("TravelName").Value,
                        FinalCost = Convert.ToInt32(elem.Element("FinalCost").Value),
                        Duration = Convert.ToInt32(elem.Element("Duration").Value),
                    });
                }
            }
            return list;
        }
        private List<TravelTour> LoadTravelTours()
        {
            var list = new List<TravelTour>();
            if (File.Exists(TravelTourFileName))
            {
                XDocument xDocument = XDocument.Load(TravelTourFileName);
                var xElements = xDocument.Root.Elements("TravelTour").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new TravelTour
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TravelId = Convert.ToInt32(elem.Element("TravelId").Value),
                        TourId = Convert.ToInt32(elem.Element("TourId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        public List<TravelViewModel> Read(TravelBindingModel model)
        {
            return Travels.Where(rec => model == null
                || rec.Id == model.Id && model.Id.HasValue)
            .Select(rec => new TravelViewModel
            {
                Id = rec.Id,
                TravelName = rec.TravelName,
                Duration = rec.Duration,
                FinalCost = rec.FinalCost,
                TravelTours = TravelTours
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
