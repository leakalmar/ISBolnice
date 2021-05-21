using DoctorView;
using Hospital_IS.DoctorView;
using Hospital_IS.DTOs;
using Hospital_IS.ManagerViewModel;
using System;
using System.Collections.Generic;
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

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for MedicineRegistrationView.xaml
    /// </summary>
    public partial class MedicineRegistrationView : Page
    {
        public MedicineRegistrationView()
        {
            InitializeComponent();
            this.DataContext = MedicineViewModel.Instance;
        }



        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {

            ReplaceMedicineNameDTO newMedicine = new ReplaceMedicineNameDTO("");
            MedicineViewModel.Instance.ReplaceMedicineNameDTOs.Add(newMedicine);
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

            MedicineComponentDTO medicineComponent = new MedicineComponentDTO("");
            MedicineViewModel.Instance.CompositionDTO.Add(medicineComponent);
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
