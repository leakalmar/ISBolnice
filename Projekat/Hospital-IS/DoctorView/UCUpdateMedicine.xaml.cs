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
    public class AddKey : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public object UIGrid;
        public object UIList;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ReplaceMedicineName newMedicine = new ReplaceMedicineName("");
            ((ObservableCollection<ReplaceMedicineName>)UIList).Add(newMedicine);
            ((DataGrid)UIGrid).Items.Refresh();
            ((DataGrid)UIGrid).Focus();
            ((DataGrid)UIGrid).SelectedItem = newMedicine;
            ((DataGrid)UIGrid).ScrollIntoView(((DataGrid)UIGrid).Items.GetItemAt(((DataGrid)UIGrid).Items.Count - 1));
            var cellcontent = ((DataGrid)UIGrid).Columns[0].GetCellContent(((DataGrid)UIGrid).SelectedItem);
            var cell = cellcontent?.Parent as DataGridCell;
            if (cell != null)
            {
                cell.Focus();
            }
        }
    }
    public class AddCommandContext
    {
        public object list { get; set; }
        public object gridToAdd { get; set; }
        public ICommand AddCommand
        {
            get
            {
                return new AddKey()
                {

                    UIGrid = gridToAdd,
                    UIList = list
                };



            }
        }
    }
    public partial class UCUpdateMedicine : UserControl
    {
        public ObservableCollection<MedicineComponent> CompositionOfMedicine { get; set; }
        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines { get; set; }
        public String newUsage { get; set; }
        public String NewSideEffects { get; set; }
        public Medicine MedicineWhichIsUpdated { get; set; }
        public UCUpdateMedicine(Medicine medicine)
        {
            InitializeComponent();
            MedicineWhichIsUpdated = medicine;
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(medicine.ReplaceMedicine);
            CompositionOfMedicine = new ObservableCollection<MedicineComponent>(medicine.Composition);
            NewSideEffects = medicine.SideEffects;
            newUsage = medicine.Usage;
            medInfo.DataContext = MedicineWhichIsUpdated;
            usage.Text = newUsage;
            sideEff.Text = NewSideEffects;



            composition.ItemsSource = CompositionOfMedicine;
            change.ItemsSource = ReplaceMedicines;

            this.DataContext = new AddCommandContext()
            {
                gridToAdd = change,
                list = ReplaceMedicines
            };

            if(composition.Items.Count != 0)
            {
                composition.SelectedIndex = 0;
                var cellcontent = composition.Columns[0].GetCellContent(composition.SelectedItem);
                var cell = cellcontent?.Parent as DataGridCell;
                if (cell != null)
                {
                    cell.Focus();
                }
            }else
            {
                compositionAdd.Focus();
            }


        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MedicineWhichIsUpdated.Composition = CompositionOfMedicine.ToList();
            MedicineWhichIsUpdated.ReplaceMedicine = ReplaceMedicines.ToList();
            MedicineWhichIsUpdated.SideEffects = sideEff.Text;
            MedicineWhichIsUpdated.Usage = usage.Text;

            MedicineController.Instance.UpdateMedicine(MedicineWhichIsUpdated);
           // DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorMainWindow.Instance.Medicines.Visibility = Visibility.Visible;
        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {
           // DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorMainWindow.Instance.Medicines.Visibility = Visibility.Visible;
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
