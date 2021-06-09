using Controllers;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class RecipientViewModel:ViewModel
    { 

        private ObservableCollection<Doctor> doctors;
        private List<Doctor> selectedDoctors = new List<Doctor>();
        private Medicine notificationMedicine;
        private MedicineNotification notification;
        private NavigationService navService;
        private RelayCommand sendNotificationToDoctorCommand;
        private RelayCommand sendReNotificationToDoctorCommand;


        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }
        public MedicineNotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                if (value != notification)
                {

                    notification = value;
                    OnPropertyChanged("Notification");

                }
            }
        }

        public Medicine NotificationMedicine
        {
            get
            {
                return notificationMedicine;
            }
            set
            {
                if (value != notificationMedicine)
                {

                    notificationMedicine = value;
                    OnPropertyChanged("NotificationMedicine");

                }
            }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                if (value != doctors)
                {

                    doctors = value;
                    OnPropertyChanged("Doctors");

                }
            }
        }

        public RelayCommand SendReNotificationToDoctorCommand
        {
            get { return sendReNotificationToDoctorCommand; }
            set
            {
                sendReNotificationToDoctorCommand = value;
            }
        }


        public RelayCommand SendNotificationToDoctorCommand
        {
            get { return sendNotificationToDoctorCommand; }
            set
            {
                sendNotificationToDoctorCommand = value;
            }
        }


        private static RecipientViewModel instance = null;
        public static RecipientViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RecipientViewModel();
                }
                return instance;
            }
        }

        private RecipientViewModel()
        {
            Doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            this.SendNotificationToDoctorCommand = new RelayCommand(Execute_SendNotificationToDoctor, CanExecute_IfDoctorIsSelected);
            this.SendReNotificationToDoctorCommand = new RelayCommand(Execute_SendReNotificationToDoctor, CanExecute_IfDoctorIsSelected);

            
        }

        private bool CanExecute_IfDoctorIsSelected(object obj)
        {

            System.Collections.IList items = (System.Collections.IList)obj;
            selectedDoctors.Clear();
            foreach (var doctor in items)
            {
                selectedDoctors.Add((Doctor)doctor);
            }
            
            return selectedDoctors.Count >= 2;
        }

        private void Execute_SendNotificationToDoctor(object obj)
        {
            List<int> doctorIds = new List<int>();
            foreach(Doctor d in selectedDoctors.ToList())
            {
                if (!doctorIds.Contains(d.Id))
                {
                    doctorIds.Add(d.Id);
                }         
            }
            MedicineNotificationDTO notificationDTO = new MedicineNotificationDTO(NotificationMedicine.Name, NotificationMedicine.SideEffects, NotificationMedicine.Usage, NotificationMedicine.ReplaceMedicine,
               NotificationMedicine.Composition, doctorIds);
            MedicineNotificationController.Instance.CreateNotification(notificationDTO);
            selectedDoctors = new List<Doctor>();
        }

        private void Execute_SendReNotificationToDoctor(object obj)
        {
            List<int> doctorIds = new List<int>();
            foreach (Doctor d in selectedDoctors.ToList())
            {
                if (!doctorIds.Contains(d.Id))
                {
                    doctorIds.Add(d.Id);
                }

            }
            MedicineNotificationDTO notificationDTO = new MedicineNotificationDTO(NotificationMedicine.Name, NotificationMedicine.SideEffects, NotificationMedicine.Usage, NotificationMedicine.ReplaceMedicine,
                NotificationMedicine.Composition, doctorIds);
            MedicineNotificationController.Instance.CreateReNotification(notificationDTO);
            MedicineNotificationController.Instance.DeleteNotification(Notification);
            NotificationViewModel.Instance.Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(6));
            this.NavService.Navigate(
                  new Uri("ManagerView1/NotificationView.xaml", UriKind.Relative));
            selectedDoctors = new List<Doctor>();
        }

    }
}
