using Controllers;
using DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    public partial class UCUpdateMedicine : UserControl
    {
        public ObservableCollection<MedicineComponent> CompositionOfMedicine { get; set; }
        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines { get; set; }
        public Medicine MedicineWhichIsUpdated { get; set; }
        public UCUpdateMedicine(Medicine medicine)
        {
            InitializeComponent();
            medInfo.DataContext = medicine;
            MedicineWhichIsUpdated = medicine;
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(medicine.ReplaceMedicine);
            CompositionOfMedicine = new ObservableCollection<MedicineComponent>(medicine.Composition);
            composition.ItemsSource = CompositionOfMedicine;
            composition.CanUserAddRows = true;
            change.CanUserAddRows = true;
            change.ItemsSource = ReplaceMedicines;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MedicineWhichIsUpdated.Composition = CompositionOfMedicine.ToList();
            MedicineWhichIsUpdated.ReplaceMedicine = ReplaceMedicines.ToList();
            MedicineWhichIsUpdated.SideEffects = sideEff.Text;
            MedicineWhichIsUpdated.Usage = usage.Text;

            MedicineController.Instance.UpdateMedicine(MedicineWhichIsUpdated);
            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Medicines.Visibility = Visibility.Visible;
        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Medicines.Visibility = Visibility.Visible;
        }

        private void CompositionAdd_Click(object sender, RoutedEventArgs e)
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

        private void ChangeAdd_Click(object sender, RoutedEventArgs e)
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
    }
}
