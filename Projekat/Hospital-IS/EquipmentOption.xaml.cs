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
using System.Windows.Shapes;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for EquipmentOption.xaml
    /// </summary>
    public partial class EquipmentOption : Window
    {

        public ObservableCollection<Equipment> TempEquip { get; set; }
        public ObservableCollection<Room> TempRoom { get; set; }
        public EquipmentOption(Room room,int index)
        {
            InitializeComponent();
            this.DataContext = this;
             TempRoom = Hospital.Room;
              Combo.SelectedIndex = index;
               Combo.IsEnabled = false;
            TempEquip = new ObservableCollection<Equipment>();
            foreach (Equipment eq in room.Equipment)
            {
                TempEquip.Add(eq);
            }
        }



        private void TransferEquipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.Show();
            this.Hide();
           
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
