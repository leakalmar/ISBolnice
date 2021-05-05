using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView
{
    /// <summary>
    /// Interaction logic for NotificationInfoView.xaml
    /// </summary>
    public partial class NotificationInfoView : Window
    {
      
        public static MedicineNotification MedicineNotification { get; set; }
        public NotificationInfoView(MedicineNotification notification)
        {
            InitializeComponent();
            MedicineNotification = notification;
            AllDoctors.ItemsSource = new ObservableCollection<Doctor>(DoctorController.Instance.GetAllDoctorsByIds(notification.SenderId));
            NotificationInformation.DataContext = notification;
            DateLabel.Content = notification.DateSent.ToShortDateString();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ManagerNotificationView managerNotification = new ManagerNotificationView();
            managerNotification.Show();
            this.Hide();
        }

        private void MedicineInsight_Click(object sender, RoutedEventArgs e)
        {
            MedicineInfoView medicineInfo = new MedicineInfoView(MedicineNotification.Medicine, "notification", MedicineNotification);
            medicineInfo.Show();
            this.Hide();
        }

        private void MedicineChange_Click(object sender, RoutedEventArgs e)
        {
            MedicineNotificationChage medicineNotification = new MedicineNotificationChage(MedicineNotification);
            medicineNotification.Show();
            this.Close();
        }

        private void MedicineDelete_Click(object sender, RoutedEventArgs e)
        {

            MedicineNotificationController.Instance.DeleteNotification(MedicineNotification);
            
            ManagerNotificationView managerNotification = new ManagerNotificationView();
            managerNotification.Show();
            this.Hide();
        }
    }
}
