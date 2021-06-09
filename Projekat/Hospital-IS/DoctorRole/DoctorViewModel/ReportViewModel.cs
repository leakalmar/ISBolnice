using DTOs;
using Hospital_IS.DoctorRole.Commands;
using System.Collections.ObjectModel;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class ReportViewModel : BindableBase
    {
        #region Feilds
        private ObservableCollection<PrescriptionDTO> prescriptions;
        private PrescriptionDTO selectedPrescription;
        private string anemnesis;

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
            DoctorNavigationController.Instance.NavigateToSearchMedicineCommand(this.Prescriptions);
        }

        #endregion

        #region Constructor
        public ReportViewModel()
        {
            Prescriptions = new ObservableCollection<PrescriptionDTO>();
            this.NavigateBackCommand = DoctorMainWindowModel.Instance.NavigateBackCommand;
            this.DeletePrescriptionCommand = new RelayCommand(Execute_DeletePrescriptionCommand, CanExecute_Command);
            this.SearchMedicineCommad = new RelayCommand(Execute_SearchMedicineCommad, CanExecute_Command);
        }
        #endregion


    }
}
