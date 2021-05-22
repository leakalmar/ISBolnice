using Controllers;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class Medicines : UserControl
    {
        public Medicines()
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
