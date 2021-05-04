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
using System.Windows.Threading;
using Controllers;
using Model;

namespace Hospital_IS
{
    
    public partial class EquipmentWindow : Window
    {

        public ObservableCollection<Room> TempRoom { get; set; }
        public ObservableCollection<Equipment> TempEquip { get; set; }
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

            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

        }


        public void refresh()
        {
          
            Combo.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;

            MessageBox.Show(Convert.ToString(TransferController.Instance.getAllTransfers().Count));
           
            foreach(Room r in RoomController.Instance.getAllRooms())
            {
                foreach(Transfer trans in TransferController.Instance.getAllTransfers())
                {
                   
                    if (trans.TransferEnd <= time && trans.isMade == false)
                    {
                       trans.isMade = true;
                       TransferController.Instance.TransferStaticEquipment(trans);
                        
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

        public void refreshGrid(Room room,int index)
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
            Combo.SelectedIndex = index;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentOption equipmentOpt = new EquipmentOption((Room)Combo.SelectedItem,Combo.SelectedIndex);
            equipmentOpt.Show();
            this.Hide();

        }



        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Room room = (Room)Combo.SelectedItem;
         
            if(room != null)
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
    }
}
