using Controllers;
using DoctorView;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UpdateMedicine : UserControl
    {

            private UpdateMedicineViewModel viewModel;

            public UpdateMedicineViewModel _ViewModel
            {
                get { return viewModel; }
                set { viewModel = value; }
            }

            public UpdateMedicine()
            {
                InitializeComponent();
                this._ViewModel = new UpdateMedicineViewModel();
                this.DataContext = _ViewModel;

            }


            //fokusiranje tacno odredjene celije
           /* if(composition.Items.Count != 0)
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
            }*/

           /* MedicineComponent medicineComponent = new MedicineComponent("");
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
            }*/
    }
}
