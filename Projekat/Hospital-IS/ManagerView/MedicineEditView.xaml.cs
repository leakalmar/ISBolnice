using Controllers;
using DoctorView;
using Hospital_IS.DoctorView;
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
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView
{
    /// <summary>
    /// Interaction logic for MedicineEditView.xaml
    /// </summary>
    public partial class MedicineEditView : Window
    {
        public ObservableCollection<MedicineComponent> CompositionOfMedicine { get; set; }
        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines { get; set; }

        private Medicine currentMedicine;
        public MedicineEditView(Medicine medicine)
        {
            InitializeComponent();
            currentMedicine = medicine;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<ReplaceMedicineName> medicineNames = ReplaceMedicines.ToList();
            List<MedicineComponent> medicineComponents = CompositionOfMedicine.ToList();
            String sideEffect = SideEffecct.Text;
            String therapeutic = Usage.Text;
            String name = MedicineName.Text;

            MedicineController.Instance.UpdateMedicineWithName(currentMedicine.Name, name, medicineComponents, sideEffect, therapeutic, medicineNames);
            MedicineView medicineView = new MedicineView();
            medicineView.Show();
            this.Hide();

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
