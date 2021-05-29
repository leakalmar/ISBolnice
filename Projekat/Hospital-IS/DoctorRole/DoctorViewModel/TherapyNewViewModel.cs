using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;
using System.Collections.Generic;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class TherapyNewViewModel : BindableBase
    {
        #region Fields
        private List<Medicine> medicines;
        private Medicine selectedMedicine;
        private int pills;
        private int takings;
        private string therapyEnd;

        public List<Medicine> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
                OnPropertyChanged("Medicines");
            }
        }
        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
            }
        }


        public int Pills
        {
            get { return pills; }
            set
            {
                pills = value;
                OnPropertyChanged("Pills");
            }
        }

        public int Takings
        {
            get { return takings; }
            set
            {
                takings = value;
                OnPropertyChanged("Takings");
            }
        }

        public string TherapyEnd
        {
            get { return therapyEnd; }
            set
            {
                therapyEnd = value;
                OnPropertyChanged("TherapyEnd");
            }
        }
        #endregion

        #region Commands
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
                OnPropertyChanged("SaveCommand");
            }
        }

        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
                OnPropertyChanged("CancelCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_SaveCommand(object obj)
        {
            DateTime endDate = DateTime.Now;
            string[] parts = TherapyEnd.Split('.');
            if (parts.Length != 4)
            {
                new ExitMess("Nispravan datum završetka terapije. Unesite datum u obliku dd.mm.yyyy.").Show();
            }
            else
            {
                try
                {
                    endDate = new DateTime(Int32.Parse(parts[2]), Int32.Parse(parts[1]), Int32.Parse(parts[0]));
                    Therapy newTherapy = new Therapy(SelectedMedicine, Pills, Takings, DateTime.Now, endDate);
                    ChartController.Instance.AddTherapy(newTherapy, DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient);
                    DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("4");
                    DoctorMainWindow.Instance._ViewModel.NavigateToChartCommand.Execute(null);
                }
                catch
                {
                    new ExitMess("Nispravan datum završetka terapije. Unesite datum u obliku dd.mm.yyyy.").Show();
                }
            }
           
        }

        private void Execute_CancelCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.NavigateToChartCommand.Execute(null);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TherapyNewViewModel()
        {
            this.Takings = 1;
            this.Pills = 1;
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_Command);
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            this.Medicines = MedicineController.Instance.GetAll();
        }
        #endregion
    }
}
