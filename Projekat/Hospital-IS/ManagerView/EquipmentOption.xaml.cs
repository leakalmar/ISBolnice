using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


           
            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
            Combo.SelectedIndex = index;

            currentRoom = room;
            if (currentRoom == null)
            {
                currentRoom = new Room();
            }
            if(currentRoom.Equipment != null)
            {
                DataGridEquipment.DataContext = new ObservableCollection<Equipment>(currentRoom.Equipment);
            }
        }

        public EquipmentOption()
        {
            InitializeComponent();
            this.DataContext = this;
            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
            DataGridEquipment.DataContext = new ObservableCollection<Equipment>();
          
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Room room = (Room)Combo.SelectedItem;

           
           
            if (room != null)
            {
                if (room.Equipment != null)
                {
                    ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                    view.Filter = null;
                    DataGridEquipment.DataContext = view;
                }
                else
                {
                    room.Equipment = new List<Equipment>();
                    ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                    view.Filter = null;
                    DataGridEquipment.DataContext = view;
                }
            }
            else
            {

                ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                view.Filter = null;
                DataGridEquipment.DataContext = view;
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchPanel.Visibility == Visibility.Collapsed)
            {
                SearchPanel.Visibility = Visibility.Visible;
            }
            else
            {
                SearchPanel.Visibility = Visibility.Collapsed;
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

        private void SeacrhDateGrid_Click(object sender, RoutedEventArgs e)
        {
            String text = SearchBox.Text.ToLower();
            String[] textSplit = text.Split(" ");


            Room room = (Room)Combo.SelectedItem;
            if (text.Length != 0)
            {
                if (room != null)
                {
                    ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                    view.Filter = null;
                    view.Filter = delegate (object item)
                    {
                        String name = ((Equipment)item).Name.ToLower();
                        int quantity = 0;
                        try
                        {
                            quantity = Convert.ToInt32(textSplit[1]);
                        }
                        catch (Exception e)
                        {
                            quantity = 0;
                        }
                        return name.Contains(textSplit[0]) && ((Equipment)item).Quantity >= quantity;
                    };
                    DataGridEquipment.DataContext = view;
                }
            }
            else
            {
                if (room != null)
                {
                    ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                    view.Filter = null;
                    DataGridEquipment.DataContext = view;
                }
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)Combo.SelectedItem;
            EquiptType type;
            if (ComboType.SelectedIndex == 0)
            {
                type = EquiptType.Dynamic;
            }
            else
            {
                type = EquiptType.Stationary;

            }

            if (room != null)
            {
                ICollectionView view = new CollectionViewSource { Source = room.Equipment }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    return ((Equipment)item).EquipType == type;
                };
                DataGridEquipment.DataContext = view;
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }
    }
}
