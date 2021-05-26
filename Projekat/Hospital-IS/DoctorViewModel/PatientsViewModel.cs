using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Hospital_IS.DoctorViewModel
{
    public class PatientsViewModel : BindableBase
    {
        #region Fields
        private ICollectionView patients;
        private Patient selectedPatient;
        private string searchText;

        public ICollectionView Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged("Patients");
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }
        #endregion

        #region Commands
        private RelayCommand openChartCommand;

        public RelayCommand OpenChartCommand
        {
            get { return openChartCommand; }
            set { openChartCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_OpenChartCommand(object obj)
        {
            PatientChart view = new PatientChart();
            //view._ViewModel.Patient = SelectedPatient;
            DoctorMainWindow.Instance._ViewModel.PatientChartView = view;
            DoctorMainWindow.Instance._ViewModel.NavigateToChartCommand.Execute(obj);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        #endregion

        #region Constructor
        public PatientsViewModel()
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_Command);
            ObservableCollection<Patient> allPatients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
            Patients = new CollectionViewSource { Source = allPatients }.View;
        }
        #endregion
    }
}
