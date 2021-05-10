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
using System.Windows.Shapes;
using Controllers;
using Model;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {
        private Equipment currentEquip;
     
        private Room currentRoom;

        public EditEquipment(Room room, Equipment equipment)
        {
            currentEquip = equipment;
            currentRoom = room;


            InitializeComponent();

            name.Text = equipment.Name;
            if (equipment.EquipType.Equals(EquiptType.Dynamic))
            {
                type.SelectedIndex = 0;
            }
            else
            {
                type.SelectedIndex = 1;
            }
            type.IsEnabled = false;
            quantity.Text = Convert.ToString(equipment.Quantity);
        }

        private void Affirm_Click(object sender, RoutedEventArgs e)
        {
            String name1 = name.Text;
            int quantity1 = Convert.ToInt32(quantity.Text);
            bool isSucces =  RoomController.Instance.UpdateEquipment(currentRoom, new Equipment(currentEquip.EquipType, currentEquip.EquiptId, name1, quantity1));

            if (isSucces)
            {
                EquipmentWindow.Instance.refreshGrid(currentRoom);
                EquipmentWindow.Instance.Show();
                this.Hide();
            }
           
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.Show();

            this.Hide();
        }
    }
}
