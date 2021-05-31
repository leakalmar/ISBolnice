using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class ReportViewModel : BindableBase
    {
        #region Feilds
        private ObservableCollection<PrescriptionDTO> prescriptions;
        private PrescriptionDTO selectedPrescription;
        private string anemnesis;
        private SearchMedicine searchMedicine;
        private NavigationService mainNavigationService;

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
            }
        }

        public ObservableCollection<PrescriptionDTO> Prescriptions
        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged("Prescriptions");
            }
        }
        public PrescriptionDTO SelectedPrescription
        {
            get { return selectedPrescription; }
            set
            {
                selectedPrescription = value;
                OnPropertyChanged("SelectedPrescription");
            }
        }

        public string Anemnesis
        {
            get { return anemnesis; }
            set
            {
                anemnesis = value;
                OnPropertyChanged("Anemnesis");
            }
        }

        public SearchMedicine SearchMedicineView
        {
            get { return searchMedicine; }
            set
            {
                searchMedicine = value;
                OnPropertyChanged("SearchMedicine");
            }
        }
        #endregion

        #region Commands
        private RelayCommand deletePrescriptionCommad;
        private RelayCommand searchMedicineCommad;
        private RelayCommand navigateBackCommand;

        public RelayCommand DeletePrescriptionCommand
        {
            get { return deletePrescriptionCommad; }
            set { deletePrescriptionCommad = value; }
        }

        public RelayCommand SearchMedicineCommad
        {
            get { return searchMedicineCommad; }
            set { searchMedicineCommad = value; }
        }
        public RelayCommand NavigateBackCommand
        {
            get { return navigateBackCommand; }
            set { navigateBackCommand = value; }
        }


        #endregion

        #region Actions
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_DeletePrescriptionCommand(object obj)
        {
            this.Prescriptions.Remove(SelectedPrescription);
        }

        private void Execute_SearchMedicineCommad(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.SearchMedicineView._ViewModel.Prescriptions = Prescriptions;
            MainNavigationService.Navigate(DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.SearchMedicineView);
        }

        #endregion

        #region Constructor
        public ReportViewModel()
        {
            Prescriptions = new ObservableCollection<PrescriptionDTO>();
            this.NavigateBackCommand = DoctorMainWindow.Instance._ViewModel.NavigateBackCommand;
            this.DeletePrescriptionCommand = new RelayCommand(Execute_DeletePrescriptionCommand, CanExecute_Command);
            this.SearchMedicineCommad = new RelayCommand(Execute_SearchMedicineCommad, CanExecute_Command);
            this.MainNavigationService = DoctorMainWindow.Instance._ViewModel.NavigationService;
        }
        #endregion


    }
}
