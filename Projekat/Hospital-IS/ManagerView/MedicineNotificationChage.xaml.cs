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
    /// Interaction logic for MedicineNotificationChage.xaml
    /// </summary>
    public partial class MedicineNotificationChage : Window
    { 
        public MedicineNotification MedicineNotification { get; set; }
        public ObservableCollection<MedicineComponent> CompositionOfMedicine { get; set; }
        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines { get; set; }

      
        public MedicineNotificationChage(MedicineNotification notification)
        {
            MedicineNotification = notification;
            InitializeComponent();
           
            MedicalInformation.DataContext = notification.Medicine;
            MedicineName.Text = notification.Medicine.Name;
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(notification.Medicine.ReplaceMedicine);
            CompositionOfMedicine = new ObservableCollection<MedicineComponent>(notification.Medicine.Composition);
            composition.ItemsSource = CompositionOfMedicine;
            change.ItemsSource = ReplaceMedicines;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NotificationInfoView notification = new NotificationInfoView(MedicineNotification);
            notification.Show();
            this.Close();
        }

        
       
        private void Send_Click(object sender, RoutedEventArgs e)
        {
           
         ChooseRecipient recipient = new ChooseRecipient(MedicineNotification.Medicine.Name, MedicineNotification.Medicine.SideEffects, MedicineNotification.Medicine.Usage,
            MedicineNotification.Medicine.ReplaceMedicine, MedicineNotification.Medicine.Composition, "notification",this);

            recipient.Show();
        }
        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {

            ReplaceMedicineName newMedicine = new ReplaceMedicineName("");
            ReplaceMedicines.Add(newMedicine);
            change.Items.Refresh();
            change.Focus();
            change.SelectedItem = newMedicine;
            change.ScrollIntoView(change.Items.GetItemAt(change.Items.Count - 1));
            var cellcontent = change.Columns[0].GetCellContent(change.SelectedItem);
            var cell = cellcontent?.Parent as DataGridCell;
            if (cell != null)
            {
                cell.Focus();
            }

        }

        private void AddContent_Click(object sender, RoutedEventArgs e)
        {

            MedicineComponent medicineComponent = new MedicineComponent("");
            CompositionOfMedicine.Add(medicineComponent);
            composition.Items.Refresh();
            composition.Focus();
            composition.SelectedItem = medicineComponent;
            composition.ScrollIntoView(composition.Items.GetItemAt(composition.Items.Count - 1));
            var cellcontent = composition.Columns[0].GetCellContent(composition.SelectedItem);
            var cell = cellcontent?.Parent as DataGridCell;
            if (cell != null)
            {
                cell.Focus();
            }
        }
    }
}
