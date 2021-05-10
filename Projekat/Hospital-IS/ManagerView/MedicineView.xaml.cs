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
    /// Interaction logic for MedicineView.xaml
    /// </summary>
    public partial class MedicineView : Window
    {
        public MedicineView()
        {
            InitializeComponent();
            DataGridMedicine.DataContext = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.Show();
            this.Hide();
        }

        private void Eqiupment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.Show();
            this.Hide();
        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if(MedicineOptions.Visibility == Visibility.Visible)
            {
                MedicineOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                MedicineOptions.Visibility = Visibility.Visible;
            }
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            ManagerLogout managerLogout = new ManagerLogout("medicine");
            managerLogout.Show();
            this.Hide();
        }

        private void RegistrationMedicine_Click(object sender, RoutedEventArgs e)
        {
            MedicineRegistration medicineRegistration = new MedicineRegistration();
            medicineRegistration.Show();
            this.Hide();
        }

        private void MedicineInsight_Click(object sender, RoutedEventArgs e)
        {

            Medicine medicine = (Medicine)DataGridMedicine.SelectedItem;
            if (medicine != null)
            {
                MedicineInfoView medicineInfo = new MedicineInfoView(medicine, "info", null); ;

                medicineInfo.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Izaberite lijek");
            }
        }

        private void EditMedicine_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)DataGridMedicine.SelectedItem;

            if (medicine != null)
            {
                MedicineEditView medicineEdit = new MedicineEditView(medicine);
                medicineEdit.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Izaberite lijek");
            }
        }

        private void DeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)DataGridMedicine.SelectedItem;
            if (medicine != null)
            {
                MedicineController.Instance.DeleteMedicine(medicine);
                DataGridMedicine.DataContext = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());

            }
            else
            {
                MessageBox.Show("Izaberite lijek");
            }

        }

      
    }
}
