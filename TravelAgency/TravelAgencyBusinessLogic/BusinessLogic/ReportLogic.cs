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
        private readonly IPaymentLogic paymentLogic;
        public ReportLogic(ITourLogic tourLogic, ITravelLogic travelLogic, IPaymentLogic paymentLogic)
        {
            this.tourLogic = tourLogic;
            this.travelLogic = travelLogic;
            this.paymentLogic = paymentLogic;
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
        public Dictionary<int, List<PaymentViewModel>> GetTravelPayments(TravelBindingModel model)
        {
            var travels = travelLogic.Read(model).ToList();
            Dictionary<int, List<PaymentViewModel>> payments = new Dictionary<int, List<PaymentViewModel>>();
            foreach (var travel in travels)
            {
                var travelPayments = paymentLogic.Read(new PaymentBindingModel { TravelId = travel.Id }).ToList();
                payments.Add(travel.Id, travelPayments);
            }
            return payments;
        }
        public void SaveTravelPaymentsToPdfFile(string fileName, TravelBindingModel travel, string email)
        {
            string title = "Список путешествий в период с " + travel.DateFrom.ToString() + " по " + travel.DateTo.ToString();
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Travels = travelLogic.Read(travel).ToList(),
                Payments = GetTravelPayments(travel)
            });
            SendMail(email, fileName, title);
        }
        public void SaveTravelToursToWordFile(string fileName, TravelViewModel travel, string email)
        {
            string title = "Список туров по путешествию №" + travel.Id;
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
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
