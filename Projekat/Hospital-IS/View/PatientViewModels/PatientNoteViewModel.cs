using Controllers;
using Hospital_IS.Model;
using System;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientNoteViewModel : BindableBase
    {
        private int notificationTime;
        private string noteContent;
        private bool isNotifyChecked;
        public PatientNote Note { get; set; }
        public int AppointmentId { get; set; }

        public EventHandler OnRequestClose;
        public MyICommand AppointmentNote { get; set; }
        public MyICommand CloseNote { get; set; }

        public PatientNoteViewModel(int appointmentId)
        {
            Note = PatientController.Instance.GetNoteForPatientByAppointmentId(PatientMainWindowViewModel.Patient.Id ,appointmentId);
            AppointmentId = appointmentId;
            if (Note != null)
            {
                NotificationTime = Note.NotificationTime - 8;
                NoteContent = Note.NoteContent;
                IsNotifyChecked = Note.IsNotifyChecked;
            }
            AppointmentNote = new MyICommand(AppNote);
            CloseNote = new MyICommand(Close);
        }

        public int NotificationTime
        {
            get { return notificationTime; }
            set
            {
                if(notificationTime != value)
                {
                    notificationTime = value;
                    OnPropertyChanged("NotificationTime");
                }
            }
        }

        public string NoteContent
        {
            get { return noteContent; }
            set
            {
                if (noteContent != value)
                {
                    noteContent = value;
                    OnPropertyChanged("NoteContent");
                }
            }
        }

        public bool IsNotifyChecked
        {
            get { return isNotifyChecked; }
            set
            {
                if (isNotifyChecked != value)
                {
                    isNotifyChecked = value;
                    OnPropertyChanged("IsNotifyChecked");
                }
            }
        }

        private void AppNote()
        {
            if(Note != null)
            {
                Note.NotificationTime = NotificationTime + 8;
                Note.NoteContent = NoteContent;
                Note.IsNotifyChecked = IsNotifyChecked;
                PatientController.Instance.UpdatePatient(PatientMainWindowViewModel.Patient);
            }
            else
            {
                Note = new PatientNote(NotificationTime + 8, IsNotifyChecked, NoteContent, AppointmentId);
                PatientController.Instance.AddAppointmentNote(PatientMainWindowViewModel.Patient.Id, Note);
            }
            
            OnRequestClose(this, new EventArgs());
        }

        private void Close()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
