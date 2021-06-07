using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows;
using Hospital_IS.DTOs;

namespace Hospital_IS.View.PatientViewModels
{
    public class TherapyPatientViewModel : BindableBase
    {
        public ObservableCollection<Therapy> Therapies { get; set; }
        public KeyValuePair<string, int>[] ChartData { get; set; }
        public MyICommand GenerateReport { get; set; }
        public MyICommand ShowReport { get; set; }

        private Therapy therapy;
        private string name;
        private int quantity;
        private int timesADay;
        private string timeSpan;
        private string startTherapy;
        private string endTherapy;
        private string usage;
        private string sideEffects;
        private bool showTherapyInfo = false;
        private bool chooseItem = true;
        private bool shouldShowRecipe = false;
        private bool report = false;
        private DateTime reportStart;
        private DateTime reportEnd;

        public TherapyPatientViewModel()
        {
            Therapies = new ObservableCollection<Therapy>(ChartController.Instance.GetTherapiesByPatient(PatientMainWindowViewModel.Patient));
            GenerateReport = new MyICommand(GenerateRep);
            ShowReport = new MyICommand(ShowRep);
            ReportStart = DateTime.Today.Date;
            ReportEnd = DateTime.Today.Date.AddDays(7);
            LoadTherapyChartData();
        }

        public Therapy Therapy
        {
            get { return therapy; }
            set
            {
                if (therapy != value)
                {
                    therapy = value;
                    OnPropertyChanged("dataGridTherapy");
                    SetTherapyInfo();
                }
            }
        }

        public string TimeSpan
        {
            get { return timeSpan; }
            set
            {
                if (timeSpan != value)
                {
                    timeSpan = value;
                    OnPropertyChanged("TimeSpan");
                }
            }
        }

        public string StartTherapy
        {
            get { return startTherapy; }
            set
            {
                if (startTherapy != value)
                {
                    startTherapy = value;
                    OnPropertyChanged("StartTherapy");
                }
            }
        }

        public string EndTherapy
        {
            get { return endTherapy; }
            set
            {
                if (endTherapy != value)
                {
                    endTherapy = value;
                    OnPropertyChanged("EndTherapy");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public int TimesADay
        {
            get { return timesADay; }
            set
            {
                if (timesADay != value)
                {
                    timesADay = value;
                    OnPropertyChanged("TimesADay");
                }
            }
        }

        public string Usage
        {
            get { return usage; }
            set
            {
                if (usage != value)
                {
                    usage = value;
                    OnPropertyChanged("Usage");
                }
            }
        }

        public string SideEffects
        {
            get { return sideEffects; }
            set
            {
                if (sideEffects != value)
                {
                    sideEffects = value;
                    OnPropertyChanged("SideEffects");
                }
            }
        }

        public bool ShowTherapyInfo
        {
            get { return showTherapyInfo; }
            set
            {
                if (showTherapyInfo != value)
                {
                    showTherapyInfo = value;
                    OnPropertyChanged("ShowTherapyInfo");
                }
            }
        }

        public bool ChooseItem
        {
            get { return chooseItem; }
            set
            {
                if (chooseItem != value)
                {
                    chooseItem = value;
                    OnPropertyChanged("ChooseItem");
                }
            }
        }

        public bool ShouldShowRecipe
        {
            get { return shouldShowRecipe; }
            set
            {
                if (shouldShowRecipe != value)
                {
                    shouldShowRecipe = value;
                    OnPropertyChanged("ShouldShowRecipe");
                }
            }
        }

        public bool Report
        {
            get { return report; }
            set
            {
                if (report != value)
                {
                    report = value;
                    OnPropertyChanged("Report");
                }
            }
        }

        public DateTime ReportStart
        {
            get { return reportStart; }
            set
            {
                if (reportStart != value)
                {
                    reportStart = value;
                    OnPropertyChanged("ReportStart");
                }
            }
        }

        public DateTime ReportEnd
        {
            get { return reportEnd; }
            set
            {
                if (reportEnd != value)
                {
                    reportEnd = value;
                    OnPropertyChanged("ReportEnd");
                }
            }
        }

        private void SetTherapyInfo()
        {
            int usageHourDifference = (int)24 / Therapy.TimesADay;
            Name = Therapy.Medicine.Name;
            Quantity = Therapy.Quantity;
            TimesADay = Therapy.TimesADay;
            TimeSpan = usageHourDifference.ToString() + "h";
            StartTherapy = Therapy.TherapyStart.ToString("dd.MM.yyyy.");
            EndTherapy = Therapy.TherapyEnd.ToString("dd.MM.yyyy.");
            Usage = Therapy.Medicine.Usage;
            SideEffects = Therapy.Medicine.SideEffects;
            ChooseItem = false;
            Report = false;
            ShowTherapyInfo = true;
        }

        private void LoadTherapyChartData()
        {
            ChartData = new KeyValuePair<string, int>[]{
                new KeyValuePair<string,int>("Jan", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Jan")),
                new KeyValuePair<string,int>("Feb", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Feb")),
                new KeyValuePair<string,int>("Mar", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Mar")),
                new KeyValuePair<string,int>("Apr", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Apr")),
                new KeyValuePair<string,int>("Maj", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "May")),
                new KeyValuePair<string,int>("Jun", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "June"))
            };
        }

        private void GenerateRep()
        {
            var pdfDoc = new iTextSharp.text.Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
            string path = $"..//..//..//Reports//IzvestajTerapija.pdf";
            PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.OpenOrCreate));
            pdfDoc.Open();

            var spacer = new iTextSharp.text.Paragraph("")
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
            DocumentDescription.AddCell("Izveštaj o uzimanju terapije");
            DocumentDescription.AddCell("Datum pocetka vremenskog opsega: ");
            DocumentDescription.AddCell(ReportStart.Date.ToString("dd.MM.yyyy"));
            DocumentDescription.AddCell("Datum kraja vremenskog opsega: ");
            DocumentDescription.AddCell(ReportEnd.Date.ToString("dd.MM.yyyy"));

            pdfDoc.Add(DocumentDescription);
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);

            var columnWidth = new[] { 1f, 1f, 1f, 1f };
            TherapyReportDTO therapyReportDTO = new TherapyReportDTO(PatientMainWindowViewModel.Patient.Id, ReportStart, ReportEnd);
            List<Therapy> reportTherapies = ChartController.Instance.FindTherapiesInTimeRange(therapyReportDTO);
            if (reportTherapies.Count != 0)
            {
                var pdfTableTherapy = new PdfPTable(columnWidth) { };
                var head = new PdfPCell(new Phrase("Uzimanje terapije"))
                {
                    Colspan = 4,
                    HorizontalAlignment = 1,
                    MinimumHeight = 3
                };

                pdfTableTherapy.AddCell(head);
                pdfTableTherapy.AddCell("Naziv leka");
                pdfTableTherapy.AddCell("Koliko puta dnevno");
                pdfTableTherapy.AddCell("Datum pocetka");
                pdfTableTherapy.AddCell("Datum kraja");


                foreach (Therapy therapy in reportTherapies)
                {
                    pdfTableTherapy.AddCell(therapy.Medicine.Name);
                    pdfTableTherapy.AddCell(therapy.TimesADay.ToString());
                    pdfTableTherapy.AddCell(therapy.TherapyStart.Date.ToString("dd.MM.yyyy"));
                    pdfTableTherapy.AddCell(therapy.TherapyEnd.Date.ToString("dd.MM.yyyy"));

                }

                pdfDoc.Add(pdfTableTherapy);
            }
            else
            {
                var noTherapies = new iTextSharp.text.Paragraph("Nema terapija u izabranom vremenskom opsegu!")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };

                pdfDoc.Add(noTherapies);
            }
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);
            pdfDoc.Close();
            MessageBox.Show("PDF fajl je uspešno izgenerisan!");
        }

        private void ShowRep()
        {
            Report = true;
            ChooseItem = false;
            ShowTherapyInfo = false;
        }
    }
}
