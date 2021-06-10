using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Model;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class ReviewMedicineViewModel : BindableBase
    {
        #region Fields
        private string note;
        private MedicineNotification medicineNotification;
        private bool showDisapproveNote;

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        public MedicineNotification MedicineNotification
        {
            get { return medicineNotification; }
            set
            {
                medicineNotification = value;
                OnPropertyChanged("MedicineNotification");
            }
        }

        public bool ShowDisapproveNote
        {
            get { return showDisapproveNote; }
            set
            {
                showDisapproveNote = value;
                OnPropertyChanged("ShowDisapproveNote");
            }
        }
        #endregion

        #region Commands
        private RelayCommand disapproveCommand;
        private RelayCommand approveCommand;
        private RelayCommand sendNoteCommand;

        public RelayCommand ApproveCommand
        {
            get { return approveCommand; }
            set { approveCommand = value; }
        }
        public RelayCommand DisapproveCommand
        {
            get { return disapproveCommand; }
            set { disapproveCommand = value; }
        }

        public RelayCommand SendNoteCommand
        {
            get { return sendNoteCommand; }
            set { sendNoteCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ApproveCommand(object obj)
        {
            MedicineNotificationController.Instance.ApproveMedicine(MedicineNotification);
            DoctorMainWindowModel.Instance.NavigateToApproveMedicineCommand.Execute(obj);
        }

        private void Execute_DisapproveCommand(object obj)
        {
            ShowDisapproveNote = true;
        }

        private void Execute_SendNoteCommand(object obj)
        {
            MedicineNotificationController.Instance.DisapproveMedicine(MedicineNotification, Note);
            DoctorMainWindowModel.Instance.NavigateToApproveMedicineCommand.Execute(obj);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }


        
        #endregion

        #region Constructor
        public ReviewMedicineViewModel()
        {
            ShowDisapproveNote = false;
            this.ApproveCommand = new RelayCommand(Execute_ApproveCommand, CanExecute_Command);
            this.DisapproveCommand = new RelayCommand(Execute_DisapproveCommand, CanExecute_Command);
            this.SendNoteCommand = new RelayCommand(Execute_SendNoteCommand, CanExecute_Command);
        }
        #endregion
    }
}
