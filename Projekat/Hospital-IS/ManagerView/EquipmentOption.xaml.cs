using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        public static Room currentRoom { get; set; }

        public static int currentIndex { get; set; }
        public EquipmentOption(Room room,int index)
        {

           
            InitializeComponent();
            this.DataContext = this;
            TempRoom = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            TempEquip = new ObservableCollection<Equipment>();
           
            Combo.SelectedIndex = index;

            currentRoom = room;
            if (currentRoom == null)
            {
                currentRoom = new Room();
            }
            if(currentRoom.Equipment != null)
            {
                foreach (Equipment eq in currentRoom.Equipment)
                {
                    TempEquip.Add(eq);
                }
            }
        }

        public EquipmentOption()
        {
            InitializeComponent();
            this.DataContext = this;
            TempRoom = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            TempEquip = new ObservableCollection<Equipment>();
          
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            currentRoom = (Room)Combo.SelectedItem;
            
            TempEquip.Clear();
            if(currentRoom.Equipment != null)
            {
                foreach (Equipment eq in currentRoom.Equipment)
                {

                    TempEquip.Add(eq);
                }
            }
           
        }



        private void TransferEquipment_Click(object sender, RoutedEventArgs e)
        {
           
            if (ComboTransfer.Text.Equals("Dinamicka"))
            {
                TransferDynamic transfer = new TransferDynamic(currentRoom, currentIndex);
                transfer.Show();
                this.Hide();
            }
            else
            {
                TransferStatic transfer = new TransferStatic();
                transfer.Show();
                this.Hide();
            }
          
        }



        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGridEquipment.SelectedItem;
            if (equipment == null)
            {
                MessageBox.Show("Izaberite opremu");

            }
            else
            { 
                TempEquip.Remove(equipment);
                currentRoom.RemoveEquipment(equipment);
            }
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGridEquipment.SelectedItem;
            if (equipment == null)
            {
                MessageBox.Show("Izaberite opremu");

            }
            else
            {
                EditEquipment edit = new EditEquipment(currentRoom, equipment, currentIndex);
                edit.Show();
                this.Hide();
            }
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            if(currentRoom.Type == RoomType.StorageRoom)
            {
              
                AddEquipment addEquipment = new AddEquipment(currentRoom, Combo.SelectedIndex);
                addEquipment.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Dodavanje moguce samo u magacine");
            }
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.refreshGrid(currentRoom,Combo.SelectedIndex);
            EquipmentWindow.Instance.Show();

            this.Hide();
           
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void myCombo_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ControlTemplate ct = this.ComboTransfer.Template;
            Popup pop = ct.FindName("PART_Popup", this.ComboTransfer) as Popup;
            pop.Placement = PlacementMode.Top;
        }

        private void DataGridEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
