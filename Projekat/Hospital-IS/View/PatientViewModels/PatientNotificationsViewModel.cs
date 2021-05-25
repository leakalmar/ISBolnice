using Hospital_IS.Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientNotificationsViewModel : BindableBase
    {
        public ObservableCollection<Notification> NotificationMessages { get; set; }
        public PatientNotificationsViewModel()
        {
            List<Notification> notifications = NotificationController.Instance.GetAllByUser(PatientMainWindowViewModel.Patient.Id);
            NotificationMessages = new ObservableCollection<Notification>(notifications);


        }
    }
}
