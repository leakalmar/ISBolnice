using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class TherapyPatientViewModel : BindableBase
    {
        public ObservableCollection<Therapy> Therapies { get; set; }
        public KeyValuePair<string, int>[] ChartData { get; set; }

        private Therapy therapy;
        private string name;
        private int quantity;
        private int timesADay;
        private string timeSpan;
        private string startTherapy;
        private string endTherapy;
        private string usage;
        private string sideEffects;

        public TherapyPatientViewModel()
        {
            Therapies = new ObservableCollection<Therapy>(ChartController.Instance.GetTherapiesByPatient(PatientMainWindowViewModel.Patient));
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
        }

        private void LoadTherapyChartData()
        {
            ChartData = new KeyValuePair<string, int>[]{
                new KeyValuePair<string,int>("Jan", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Jan")),
                new KeyValuePair<string,int>("Feb", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Feb")),
                new KeyValuePair<string,int>("Mar", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Mar")),
                new KeyValuePair<string,int>("Apr", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "Apr")),
                new KeyValuePair<string,int>("May", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "May")),
                new KeyValuePair<string,int>("June", ChartController.Instance.GetNumberOfTherapiesByMonth(PatientMainWindowViewModel.Patient.Id, "June"))
            };
        }
    }
}
