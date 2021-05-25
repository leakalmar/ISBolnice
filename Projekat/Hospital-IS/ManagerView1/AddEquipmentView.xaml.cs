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
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : Page
    {
        public AddEquipmentView(EquipmentViewModel eqiupmentViewModel)
        {
            InitializeComponent();
            this.DataContext = eqiupmentViewModel;
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
