using Controllers;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class ApproveMedicine : UserControl
    {
        public ObservableCollection<MedicineNotification> MedicineNotifications { get; set; } = new ObservableCollection<MedicineNotification>();

        public ApproveMedicine()
        {
            InitializeComponent();
            MedicineNotifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(DoctorMainWindow.Instance._ViewModel.Doctor.Id));


            if (MedicineNotifications.Count > 0)
                ListViewNotifications.ItemsSource = MedicineNotifications;

        }

        private void ShowNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification n = findNotification(Int32.Parse(button.Tag.ToString()));
            NotificationView nv = new NotificationView(n);
            nv.Show();

        }

        private Notification findNotification(int id)
        {
            //for (int i = 0; i < MedicineNotifications.Count; i++)
            //{
            //    if (id == MedicineNotifications[i].Id)
            //        return MedicineNotifications[i];
            //}
            return null;
        }

        private void notification_selected(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Visibility = Visibility.Collapsed;
                MedicineNotification selectedNotification = (MedicineNotification)medicine.Content;
               // DoctorHomePage.Instance.Home.Children.Add(new UCViewMedicine(selectedNotification));
            }
        }

        private void visibility_change(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListViewNotifications.ItemsSource = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(DoctorMainWindow.Instance._ViewModel.Doctor.Id));
        }
    }
}
