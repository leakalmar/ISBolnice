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
    /// Interaction logic for AddEquipment.xaml
    /// </summary>
    public partial class AddEquipment : Window
    {
        private Room currentRoom;
        private int currentIndex;
        public AddEquipment(Room room, int index)
        {
            currentRoom = room;
            currentIndex = index;
            InitializeComponent();
        }

        private void Affirm_Click(object sender, RoutedEventArgs e)
        {
            String name1 = name.Text;
            EquiptType type1;
            if (type.Text.Equals("dinamicka"))
            {
                type1 = EquiptType.Dynamic;
            }else
            {
                type1 = EquiptType.Stationary;
            }
            int quantity1 = Convert.ToInt32(quantity.Text);
            int id = Convert.ToInt32(currentRoom.Equipment.Count);

            currentRoom.AddEquipment(new Equipment(type1, ++id, name1, quantity1));


            
            EquipmentOption equipmentOption = new EquipmentOption(currentRoom, currentIndex);
            equipmentOption.Show();
            this.Hide();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            EquipmentOption equipmentOption = new EquipmentOption(currentRoom, currentIndex);
            equipmentOption.Show();
            this.Close();
            

        }
    }
}
