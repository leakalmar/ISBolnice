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
using System.Windows.Threading;
using Controllers;
using Model;

namespace Hospital_IS
{
    
    public partial class EquipmentWindow : Window
    {

      
        private static EquipmentWindow instance = null;
        public static EquipmentWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentWindow();
                }
                return instance;
            }
        }
        private EquipmentWindow()
        {
            InitializeComponent();
            DataGridEquipment.DataContext = new ObservableCollection<Equipment>();
            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            DataGridEquipment.SelectedItem = null;
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();
        }


        public void refresh()
        {
          
            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            foreach(Room r in RoomController.Instance.GetAllRooms())
            {
                foreach(Transfer trans in TransferController.Instance.GetAllTransfers())
                {
                    if (trans.TransferEnd <= time && trans.isMade == false)
                    {
                       trans.isMade = true;
                       TransferController.Instance.ExecuteStaticTransfer(trans);       
                       MessageBox.Show("Uspjesan transfer");
                    }
                }
            }
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.Show();
            this.Hide();
        }

        public void refreshGrid(Room room)
        {
            if (room != null)
            {
                if (room.Equipment != null)
                {
                    DataGridEquipment.DataContext = new ObservableCollection<Equipment>(room.Equipment);
                }
                else
                {
                    DataGridEquipment.DataContext = new ObservableCollection<Equipment>();
                }
            }   
            else
            {
                DataGridEquipment.DataContext = new ObservableCollection<Equipment>();
            }
          

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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(SearchPanel.Visibility == Visibility.Collapsed)
            {
                SearchPanel.Visibility = Visibility.Visible;
            }
            else
            {
                SearchPanel.Visibility = Visibility.Collapsed;
            }
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
                       }catch(Exception e)
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
            if(ComboType.SelectedIndex == 0)
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

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGridEquipment.SelectedItem;
            Room currentRoom = (Room)Combo.SelectedItem;
            if (equipment == null)
            {
                MessageBox.Show("Izaberite opremu");

            }
            else
            {

                RoomController.Instance.RemoveEquipment(currentRoom, equipment);
                DataGridEquipment.SelectedItem = null;
                refreshGrid(currentRoom);
            }
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGridEquipment.SelectedItem;
            Room currentRoom = (Room)Combo.SelectedItem;
            if (equipment == null)
            {
                MessageBox.Show("Izaberite opremu");

            }
            else
            {
                EditEquipment edit = new EditEquipment(currentRoom, equipment);
                edit.Show();
                this.Hide();
                DataGridEquipment.SelectedItem = null;
            }
        }

        private void TransferEquipment_Click(object sender, RoutedEventArgs e)
        {
            Room currentRoom = (Room)Combo.SelectedItem;
            if (ComboTransfer.Text.Equals("Dinamicka"))
            {
                TransferDynamic transfer = new TransferDynamic(currentRoom);
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

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            Room currentRoom = (Room)Combo.SelectedItem;
            if (currentRoom.Type == RoomType.StorageRoom)
            {

                AddEquipment addEquipment = new AddEquipment(currentRoom);
                addEquipment.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Dodavanje moguce samo u magacine");
            }
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if(OptionsPanel.Visibility == Visibility.Collapsed)
            {
                OptionsPanel.Visibility = Visibility.Visible;
              
            }
            else
            {
                OptionsPanel.Visibility = Visibility.Collapsed;
            }
        }

       

    }
}
