using DoctorView;
using Hospital_IS.DoctorView;
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
    /// Interaction logic for MedicineInfoView.xaml
    /// </summary>
    public partial class MedicineInfoView : Window
    {
        public ObservableCollection<MedicineComponent> CompositionOfMedicine { get; set; }
        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines { get; set; }

        public MedicineNotification medicineNotification1 { get; set; }
        public MedicineInfoView(Medicine medicine, String parent, MedicineNotification medicineNotification)
        {
            medicineNotification1 = medicineNotification;
            InitializeComponent();
            if (parent.Equals("notification"))
            {
                Back.Visibility = Visibility.Collapsed;
            }
            else
            {
                BackTonotification.Visibility = Visibility.Collapsed;
            }
            MedicalInformation.DataContext = medicine;
            MedicineName.Text = medicine.Name;
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(medicine.ReplaceMedicine);
            CompositionOfMedicine = new ObservableCollection<MedicineComponent>(medicine.Composition);
            composition.ItemsSource = CompositionOfMedicine;
            change.ItemsSource = ReplaceMedicines;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MedicineView medicineView = new MedicineView();
           
            medicineView.Show();
            this.Hide();
        }

        private void BackTonotification_Click(object sender, RoutedEventArgs e)
        {
            NotificationInfoView notificationInfoView = new NotificationInfoView(medicineNotification1);
            notificationInfoView.Show();
            this.Hide();

        }
    }
}
