using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : Page
    {
        public AddEquipmentView()
        {
            InitializeComponent();
            this.DataContext = AddEquipmentViewModel.Instance;
        }

        private void EquipmentName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(EquipmentName.Text.Equals("Unesite ime opreme"))
            {
                EquipmentName.Text = "";
            }
        }

        private void Producer_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Producer.Text.Equals("Unesite ime proizvodjaca"))
            {
                Producer.Text = "";
            }
        }

        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Quantity.Text.Equals("Unesite kolicinu"))
            {
                Quantity.Text = "";
            }

        }
    }
}
