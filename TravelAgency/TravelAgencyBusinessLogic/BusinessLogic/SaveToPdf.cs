using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.HelperModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            foreach (var travel in info.Travels)
            {
                var travelLabel = section.AddParagraph("Путешествие №" + travel.Id + " от " + travel.DateOfBuying.ToShortDateString());
                travelLabel.Style = "NormalTitle";
                travelLabel.Format.SpaceBefore = "1cm";
                travelLabel.Format.SpaceAfter = "0,25cm";
                var toursLabel = section.AddParagraph("Туры:");
                toursLabel.Style = "NormalTitle";
                var tourTable = document.LastSection.AddTable();
                List<string> headerWidths = new List<string> { "1cm", "3cm", "2cm", "3cm", "3cm","3cm", "2,5cm"};
                foreach (var elem in headerWidths)
                {
                    tourTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = tourTable,
                    Texts = new List<string> { "№", "Название", "Страна", "Тип размещения", "Длительность", "Цена", "Количество"},
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                int i = 1;
                foreach (var tour in travel.TravelTours)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = tourTable,
                        Texts = new List<string> { i.ToString(), tour.TourName, tour.Country, tour.TypeOfAllocation,(tour.Duration*tour.Count).ToString(), (tour.Cost * tour.Count).ToString(), tour.Count.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }

                CreateRow(new PdfRowParameters
                {
                    Table = tourTable,
                    Texts = new List<string> { "", "", "","","", "Итого:", travel.FinalCost.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
                CreateRow(new PdfRowParameters
                {
                    Table = tourTable,
                    Texts = new List<string> { "", "", "", "", "", "Оплачено:", travel.PaidSum.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
                if (info.Payments[travel.Id].Count == 0)
                {
                    continue;
                }
                var paymentsLabel = section.AddParagraph("Платежи:");
                paymentsLabel.Style = "NormalTitle";
                var paymentTable = document.LastSection.AddTable();
                headerWidths = new List<string> { "1cm", "3cm", "3cm", "3cm" };
                foreach (var elem in headerWidths)
                {
                    paymentTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = paymentTable,
                    Texts = new List<string> { "№", "Дата", "Сумма" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                i = 1;
                foreach (var payment in info.Payments[travel.Id])
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = paymentTable,
                        Texts = new List<string> { i.ToString(), payment.DatePayment.ToString(), payment.Sum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}