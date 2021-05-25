using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientNotificationsViewModel : BindableBase
    {
        public ObservableCollection<NotificationDTO> NotificationMessages { get; set; }
        public PatientNotificationsViewModel()
        {
            List<NotificationDTO> notifications = SecretaryManagementController.Instance.GetAllByUser(PatientMainWindowViewModel.Patient.Id);
            NotificationMessages = new ObservableCollection<NotificationDTO>(notifications);


        }
    }
}
