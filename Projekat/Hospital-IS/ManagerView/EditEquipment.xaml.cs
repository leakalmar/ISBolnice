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
using Model;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {
        private Equipment currentEquip;
        private int currentIndex;
        private Room currentRoom;

        public EditEquipment(Room room, Equipment equipment,int index)
        {
            currentEquip = equipment;
            currentIndex = index;
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
            EquiptType type1;
            if (type.Text.Equals("dinamicka"))
            {
                type1 = EquiptType.Dynamic;
            }
            else
            {
                type1 = EquiptType.Stationary;
            }
            int quantity1 = Convert.ToInt32(quantity.Text);
            int id = Convert.ToInt32(currentRoom.Equipment.Count);
            if (quantity1 != 0)
            {
                currentRoom.UpdateEquipment(new Equipment(type1, currentEquip.EquiptId, name1, quantity1));
            }
            else
            {
                currentRoom.RemoveEquipment(currentEquip);
            }
            EquipmentOption equipmentOption = new EquipmentOption(currentRoom, currentIndex);
            equipmentOption.Show();
            this.Hide();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
