using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModel;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ITourLogic tourLogic;
        private readonly ITravelLogic travelLogic;
        public ReportLogic(ITourLogic tourLogic, ITravelLogic travelLogic)
        {
            this.tourLogic = tourLogic;
            this.travelLogic = travelLogic;
        }
        public List<TourViewModel> GetTravelTours(TravelViewModel travel)
        {
            var tours = new List<TourViewModel>();

            foreach (var tour in travel.TravelTours)
            {
                tours.Add(tourLogic.Read(new TourBindingModel
                {
                    Id = tour.TourId
                }).FirstOrDefault());

            }
            return tours;
        }
        public void SaveTravelToursToWordFile(string fileName, TravelViewModel travel, string email)
        {
            string title = "Список туров по путешествию №" + travel.Id;
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = "Список туров по путешествию №" + travel.Id,
                Tours = GetTravelTours(travel)
            });
            SendMail(email, fileName, title);
        }
        public void SaveTravelToursToExcelFile(string fileName, TravelViewModel travel, string email)
        {
            string title = "Список туров по путешествию №" + travel.Id;
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Tours = GetTravelTours(travel)
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("albinaforlabs@gmail.com", "Туристическая фирма «Иван Сусанин»");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("albinaforlabs@gmail.com", "alu050600!");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
