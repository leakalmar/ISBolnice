using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
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
        private PatientDTO selectedPatient;
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

        public PatientDTO SelectedPatient
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
            view._ViewModel.Patient = SelectedPatient;
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
            ObservableCollection<PatientDTO> allPatients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Patients = new CollectionViewSource { Source = allPatients }.View;
        }
        #endregion
    }
}
