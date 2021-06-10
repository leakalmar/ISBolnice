using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class PatientsViewModel : BindableBase
    {
        #region Fields
        private ICollectionView patients;
        private PatientDTO selectedPatient;
        private string searchText;
        private List<string> genders;
        private string selectedGender;
        private bool isAdmitted;

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
                FilterPatients();
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

        public List<string> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        public string SelectedGender
        {
            get { return selectedGender; }
            set
            {
                selectedGender = value;
                FilterPatients();
                OnPropertyChanged("SelectedGender");
            }
        }

        public bool IsAdmitted
        {
            get { return isAdmitted; }
            set
            {
                isAdmitted = value;
                FilterPatients();
                OnPropertyChanged("IsAdmitted");
            }
        }
        #endregion

        #region Commands
        private RelayCommand openChartCommand;
        private RelayCommand gotFocusCommand;
        private RelayCommand lostFocusCommand;

        public RelayCommand OpenChartCommand
        {
            get { return openChartCommand; }
            set { openChartCommand = value; }
        }
        public RelayCommand GotFocusCommand
        {
            get { return gotFocusCommand; }
            set { gotFocusCommand = value; }
        }
        public RelayCommand LostFocusCommand
        {
            get { return lostFocusCommand; }
            set { lostFocusCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_OpenChartCommand(object obj)
        {
            if (PatientChartViewModel.Instance.Started)
            {
                new ExitMess("Termin je trenutno u toku! Molimo vas završite termin pre otvaranja kartona drugog pacijenta.", "info").ShowDialog();
                return;
            }
            else
            {
                OpenChart(obj);
            }

        }

        private void OpenChart(object obj)
        {
            DoctorNavigationController.Instance.NavigateToChartCommand();
            PatientChartViewModel.Instance.Patient = SelectedPatient;
            PatientChartViewModel.Instance.Started = false;
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_GotFocusCommand(object obj)
        {
            if (SearchText.Equals("Pretraži..."))
            {
                SearchText = string.Empty;
            }
        }
        private void Execute_LostFocusCommand(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                SearchText = "Pretraži...";
            }
        }
        #endregion

        #region Methods
        private void FilterPatients()
        {

            if (SelectedGender != null && SearchText != null)
            {
                List<PatientDTO> patients = SecretaryManagementController.Instance.GetAllRegisteredPatients();
                ICollectionView view = new CollectionViewSource { Source = patients }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    PatientDTO patient = item as PatientDTO;
                    if (patient.Gender == null)
                    {
                        return true;
                    }
                    if (SelectedGender != null && SelectedGender != "")
                    {
                        return CheckIfPatientMeetsSearchCriteria(patient) && patient.Gender.Equals(SelectedGender) && patient.IsAdmitted == IsAdmitted;
                    }
                    else
                    {
                        return CheckIfPatientMeetsSearchCriteria(patient) && patient.IsAdmitted == IsAdmitted;
                    }
                };

                Patients = view;
            }

        }

        private bool CheckIfPatientMeetsSearchCriteria(PatientDTO patient)
        {
            string[] search = SearchText.ToLower().Split(" ");
            if (SearchText.Equals("Pretraži..."))
                search[0] = string.Empty;

            if (search.Length <= 1)
                return patient.Name.ToLower().Contains(search[0]) | patient.Surname.ToLower().Contains(search[0]);
            else
            {
                bool FirstName = true;
                bool LastName = true;
                int cnt = 0;

                for (int i = 0; i < search.Length; i++)
                {
                    if (patient.Name.ToLower().Contains(search[i]) && FirstName)
                    {
                        FirstName = false;
                        cnt++;
                        continue;
                    }
                    if (patient.Surname.ToLower().Contains(search[i]) && LastName)
                    {
                        LastName = false;
                        cnt++;
                        continue;
                    }
                }

                return cnt == search.Length;
            }

        }
        #endregion

        #region Constructor
        public PatientsViewModel()
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_Command);
            this.LostFocusCommand = new RelayCommand(Execute_LostFocusCommand, CanExecute_Command);
            this.GotFocusCommand = new RelayCommand(Execute_GotFocusCommand, CanExecute_Command);
            ObservableCollection<PatientDTO> allPatients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Patients = new CollectionViewSource { Source = allPatients }.View;
            this.Genders = new List<string>();
            Genders.Add("");
            Genders.Add("Muško");
            Genders.Add("Žensko");
            SelectedGender = Genders[0];
            SearchText = "Pretraži...";
            this.IsAdmitted = true;
        }
        #endregion
    }
}
