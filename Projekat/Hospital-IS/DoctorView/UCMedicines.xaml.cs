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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCMedicines.xaml
    /// </summary>
    public partial class UCMedicines : UserControl
    {
        public UCMedicines()
        {
            InitializeComponent();
            medicines.DataContext = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());
        }

        private void medicines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Medicine medicine = (Medicine)medicines.SelectedItem;
           // DoctorHomePage.Instance.Home.Children.Add(new UCUpdateMedicine(medicine));

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            medicines.Items.Refresh();
        }
    }
}
