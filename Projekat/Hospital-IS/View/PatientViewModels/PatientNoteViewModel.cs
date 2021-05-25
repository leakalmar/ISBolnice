﻿using Controllers;
using Hospital_IS.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
                PatientController.Instance.SavePatients();
            }
            else
            {
                Note = new PatientNote(NotificationTime + 8, IsNotifyChecked, NoteContent, AppointmentId);
                PatientController.Instance.AddAppointmentNote(PatientMainWindowViewModel.Patient.Id, Note);
            }
            
            OnRequestClose(this, new EventArgs());
        }
    }
}
