using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Input;
using System.IO;
using Controllers;
using Hospital_IS.DTOs;
using Enums;

namespace Hospital_IS.ManagerViewModel
{
    public class RenovationReportVIewModel: ViewModel
    {

        private DateTime dateStart = DateTime.Now;
        private DateTime dateEnd = DateTime.Now;
        private NavigationService navService;
        private RelayCommand makeReportCommand;



        public RelayCommand MakeReportCommand
        {
            get { return makeReportCommand; }
            set
            {
                makeReportCommand = value;
            }
        }
        public DateTime DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                if (value != dateStart)
                {

                    dateStart = value;
                    OnPropertyChanged("DateStart");
                }
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                if (value != dateEnd)
                {

                    dateEnd = value;
                    OnPropertyChanged("DateEnd");
                }
            }
        }


        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }


        private static RenovationReportVIewModel instance = null;
        public static RenovationReportVIewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationReportVIewModel();
                }
                return instance;
            }
        }
        private RenovationReportVIewModel()
        {
            this.MakeReportCommand = new RelayCommand(Execute_MakeReportCommand, CanExecute_MakeReportCommand);
          
        }

        private void Execute_MakeReportCommand(object obj)
        {

                var pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                string path = $"..//..//..//Reports//IzvestajRenovacija.pdf";
                PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.OpenOrCreate));
                pdfDoc.Open();

                var spacer = new Paragraph("")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };

                pdfDoc.Add(spacer);

                var DocumentDescription = new PdfPTable(new[] { .75f, 1f }) { };
                DocumentDescription.AddCell("Datum: ");
                DocumentDescription.AddCell(DateTime.Now.ToString("dd.MM.yyyy"));
                DocumentDescription.AddCell("Vreme: ");
                DocumentDescription.AddCell(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
                DocumentDescription.AddCell("Opis dokumenta: ");
                DocumentDescription.AddCell("Izveštaj o terminima renovacije");

                pdfDoc.Add(DocumentDescription);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);

                var columnWidth = new[] { 0.75f, 1.5f, 1.5f };

              
                RenovationDTO renovationDTO = new RenovationDTO(DateStart, DateEnd, AppointmentType.Renovation);
               List<RenovationReportDTO> renovationReports =  AppointmentController.Instance.FindAllRenovationAppBetweeenDates(renovationDTO);

                if (renovationReports.Count != 0)
                {
                    var pdfTableRenovation = new PdfPTable(columnWidth) { };
                    var zaglavlje = new PdfPCell(new Phrase("Termini renovacija"))
                    {
                        Colspan = 4,
                        HorizontalAlignment = 1,
                        MinimumHeight = 3
                    };
                    pdfTableRenovation.AddCell(zaglavlje);
                    pdfTableRenovation.AddCell("Broj sobe");
                    pdfTableRenovation.AddCell("Vreme pocetka");
                    pdfTableRenovation.AddCell("Vreme kraja");
                    foreach (RenovationReportDTO renovationReportDTO in renovationReports)
                    {
                        pdfTableRenovation.AddCell(renovationReportDTO.RoomNumber.ToString());
                        pdfTableRenovation.AddCell(renovationReportDTO.DateStart.ToString("dd.MM.yyyy"));
                        pdfTableRenovation.AddCell(renovationReportDTO.DateEnd.ToString("dd.MM.yyyy"));
                        
                    }

                    pdfDoc.Add(pdfTableRenovation);
                }
                else
                {
                    var nemaTermina = new Paragraph("    Nema zauzetih termina za lekara")
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f,
                    };

                    pdfDoc.Add(nemaTermina);
                }
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Close();
                MessageBox.Show("PDF fajl je uspešno izgenerisan!");
        
        }

        private bool CanExecute_MakeReportCommand(object obj)
        {
            if (DateStart >= DateEnd)
            {

                return false;
            }
            return true;
        }

    }
}
