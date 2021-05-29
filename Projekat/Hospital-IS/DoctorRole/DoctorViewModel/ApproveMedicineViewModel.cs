using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System.Collections.Generic;

namespace Hospital_IS.DoctorViewModel
{
    public class ApproveMedicineViewModel : BindableBase
    {
        #region Fields
        private List<MedicineNotification> medicineNotifications;
        private MedicineNotification selectedNotification;

        public List<MedicineNotification> MedicineNotifications
        {
            get { return medicineNotifications; }
            set
            {
                medicineNotifications = value;
                OnPropertyChanged("MedicineNotifications");
            }
        }

        public MedicineNotification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
            }
        }
        #endregion

        #region Commands
        private RelayCommand showNotificationCommand;

        public RelayCommand ShowNotificationCommand
        {
            get { return showNotificationCommand; }
            set { showNotificationCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ShowNotificationCommand(object obj)
        {
            if(SelectedNotification != null)
            {
                DoctorMainWindow.Instance._ViewModel.NavigateToViewMedicineCommand.Execute(SelectedNotification);
            }
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        #endregion

        #region Constructor
        public ApproveMedicineViewModel()
        {
            this.MedicineNotifications = MedicineNotificationController.Instance.GetAllByDoctorId(DoctorMainWindow.Instance._ViewModel.Doctor.Id);
            this.ShowNotificationCommand = new RelayCommand(Execute_ShowNotificationCommand, CanExecute_NavigateCommand);
        }
        #endregion

    }
}
