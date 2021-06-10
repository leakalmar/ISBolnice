using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for CreateNotification.xaml
    /// </summary>
    public partial class CreateNotification : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NotificationDTO _notification;
        public NotificationDTO Notification 
        {
            get { return _notification; }
            set
            {
                if (value != _notification)
                {
                    _notification = value;
                    OnPropertyChanged("Notification");
                }
            }
        }

        public List<int> Ids { get; set; } = new List<int>();

        public UCNotificationsView ucn;

        public CreateNotification(UCNotificationsView ucn)
        {
            InitializeComponent();
            _notification = new NotificationDTO();
            this.DataContext = this;
            this.ucn = ucn;
            btnConfirm.IsEnabled = false;

            rbSelectSome.IsEnabled = false;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void postNotification(object sender, RoutedEventArgs e)
        {
            _notification.DatePosted = DateTime.Now;
            _notification.LastChanged = DateTime.Now;

            if (rbSelectSome.IsChecked == true)
            {
                foreach (int id in Ids)
                    _notification.Recipients.Add(id);
            }
            else if (rbSelectAll.IsChecked == true)
            {
                foreach (PatientDTO patient in SecretaryManagementController.Instance.GetAllRegisteredPatients())
                    _notification.Recipients.Add(patient.Id);

                foreach (Doctor doctor in DoctorController.Instance.GetAll())
                    _notification.Recipients.Add(doctor.Id);
            }

            ucn.Notifications.Insert(0, _notification);

            SecretaryManagementController.Instance.AddNotification(_notification);

            ucn.RefreshList();

            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            RecipientSelection rp = new RecipientSelection(this);
            rp.ShowDialog();
        }

        private void rbSelectAll_Click(object sender, RoutedEventArgs e)
        {
            rbSelectAll.IsChecked = true;
            rbSelectSome.IsChecked = false;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ManageConfirmationButton(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtText.Text))
                btnConfirm.IsEnabled = false;
            else
                btnConfirm.IsEnabled = true;
        }
    }
}
