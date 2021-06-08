using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System.Collections.ObjectModel;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class TestsViewModel : BindableBase
    {
        #region Feilds
        private bool started;
        private ObservableCollection<Test> tests;
        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }
        public ObservableCollection<Test> Tests
        {
            get { return tests; }
            set
            {
                tests = value;
                OnPropertyChanged("Tests");
            }
        }
        #endregion

        #region Commands
        private RelayCommand newTestCommand;

        public RelayCommand NewTestCommand
        {
            get { return newTestCommand; }
            set
            {
                newTestCommand = value;
                OnPropertyChanged("NewTestCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_NewTestCommand(object obj)
        {
            PatientChartViewModel.Instance.InsideNavigationService.Navigate(new TestNew());
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TestsViewModel()
        {
            this.NewTestCommand = new RelayCommand(Execute_NewTestCommand, CanExecute_Command);
            this.Tests = new ObservableCollection<Test>(ChartController.Instance.GetTestsByPatient(PatientChartViewModel.Instance.Patient));
        }
        #endregion
    }
}
