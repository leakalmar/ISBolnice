using Hospital_IS.DTOs;
using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for NotificationMedicineUpdate.xaml
    /// </summary>
    public partial class NotificationMedicineUpdate : Page
    {
        public NotificationMedicineUpdate()
        {
            InitializeComponent();
            this.DataContext = NotificationViewModel.Instance;
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

        private void DeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (composition.SelectedItem != null)
            {
                MedicineComponentDTO medicineComponentDTO = (MedicineComponentDTO)composition.SelectedItem;
                MedicineViewModel.Instance.CompositionDTO.Remove(medicineComponentDTO);
                composition.Items.Refresh();
            }
        }

        private void DeleteContent_Click(object sender, RoutedEventArgs e)
        {

            if (change.SelectedItem != null)
            {
                ReplaceMedicineNameDTO medicineNameDTO = (ReplaceMedicineNameDTO)change.SelectedItem;
                MedicineViewModel.Instance.ReplaceMedicineNameDTOs.Remove(medicineNameDTO);
                change.Items.Refresh();

            }
        }

        
        
    }
}
