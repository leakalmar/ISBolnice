using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Linq;
using Enums;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class HistoryViewModel : BindableBase
    {
        #region Fields
        private bool started;
        private PatientDTO patient;
        private ICollectionView reports;
        private ReportDTO selectedReport;
        private DateTime fromDate;
        private DateTime toDate;

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                InitializeFields();
                OnPropertyChanged("Patient");

            }
        }
        public ICollectionView Reports
        {
            get { return reports; }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
            }
        }

        public ReportDTO SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
            }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                if (value > ToDate)
                {
                    fromDate = ToDate;
                }
                else
                {
                    fromDate = value;
                }
                FilterHistory();
                OnPropertyChanged("FromDate");
            }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                if (value < FromDate || value > DateTime.Now)
                {
                    toDate = DateTime.Now;
                }
                else
                {
                    toDate = value;
                }
                FilterHistory();
                OnPropertyChanged("ToDate");
            }
        }
        #endregion

        #region Commands
        private RelayCommand oldReportCommand;
        private RelayCommand printCommand;

        public RelayCommand OldReportCommand
        {
            get { return oldReportCommand; }
            set { oldReportCommand = value; }
        }

        public RelayCommand PrintCommand
        {
            get { return printCommand; }
            set { printCommand = value; }
        }

        #endregion

        #region Actions

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_OldReportCommand(object obj)
        {
            DoctorInsideNavigationController.Instance.NavigateToOldReportCommand(SelectedReport);
        }

        private void Execute_PrintCommand(object obj)
        {

            var pdfDoc = new iTextSharp.text.Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
            string path = $"..//..//..//Reports//" + Patient.Name + Patient.Surname + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".pdf";
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
            DocumentDescription.AddCell("Izveštaj o pregledu stanja pacijenta");
            DocumentDescription.AddCell("Period za koji se izveštaj kreira:");
            DocumentDescription.AddCell(FromDate.ToString("dd.MM.yyyy.") + " - " + ToDate.ToString("dd.MM.yyyy."));
            DocumentDescription.AddCell("Ime i prezime pacijenta:");
            DocumentDescription.AddCell(Patient.Name + " " + Patient.Surname + ", " + Patient.BirthDate.ToString("dd.MM.yyyy."));

            pdfDoc.Add(DocumentDescription);
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);

            var columnWidth = new[] { 0.85f, 1.5f, 0.6f, 2f };

            if (reports.Cast<object>().Count() != 0)
            {
                var pdfTable = new PdfPTable(columnWidth) { };
                var zaglavlje = new PdfPCell(new Phrase("Istorija pacijenta"))
                {
                    Colspan = 5,
                    HorizontalAlignment = 1,
                    MinimumHeight = 3
                };
                pdfTable.AddCell(zaglavlje);
                pdfTable.AddCell("Datum održavanja");
                pdfTable.AddCell("Doktor");
                pdfTable.AddCell("Tip");
                pdfTable.AddCell("Razlog održavanja");

                foreach (ReportDTO report in Reports)
                {
                    pdfTable.AddCell(report.AppointmentStart.ToString("dd.MM.yyyy."));
                    pdfTable.AddCell(report.DoctorName + " " + report.DoctorSurname);
                    if (report.Type == AppointmentType.CheckUp)
                    {
                        pdfTable.AddCell("Pregled");
                    }
                    else
                    {
                        pdfTable.AddCell("Operacija");
                    }
                    pdfTable.AddCell(report.AppointmentCause);


                }

                pdfDoc.Add(pdfTable);
            }
            else
            {
                var empty = new Paragraph("    Nema istorije za izabrane datume")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };

                pdfDoc.Add(empty);
            }
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);
            pdfDoc.Close();
            new ExitMess("PDF fajl je uspešno kreiran!", "info").ShowDialog();
        }
        #endregion

        #region Methods
        private void FilterHistory()
        {
            if (FromDate != null && ToDate != null)
            {
                List<ReportDTO> app = ChartController.Instance.GetReportsByPatient(Patient.Id);
                ICollectionView view = new CollectionViewSource { Source = app }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    return ((ReportDTO)item).AppointmentStart.Date <= ToDate.Date & ((ReportDTO)item).AppointmentStart.Date >= FromDate.Date;
                };

                Reports = view;
            }
        }

        private void InitializeFields()
        {
            ToDate = DateTime.Now;
            FromDate = DateTime.Now.AddMonths(-1);
            FilterHistory();
        }
        #endregion

        #region Constructor
        public HistoryViewModel()
        {
            this.OldReportCommand = new RelayCommand(Execute_OldReportCommand, CanExecute_Command);
            this.PrintCommand = new RelayCommand(Execute_PrintCommand, CanExecute_Command);
        }
        #endregion
    }
}
