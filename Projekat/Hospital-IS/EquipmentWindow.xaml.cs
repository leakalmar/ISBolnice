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
            
            this.DataContext = this;
           
            TempRoom = Hospital.Room;
            TempEquip = new ObservableCollection<Equipment>();
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;

            foreach(Room r in Hospital.Room)
            {
                foreach(Transfer trans in r.TransferList)
                {
                    if(trans.TransferEnd <= time && trans.isMade == false)
                    {
                        trans.isMade = true;
                        Hospital.Instance.TransferStaticEquipment(trans.SourceRoomId, trans.DestinationRoomId, trans.Equip, trans.Quantity);
                        MessageBox.Show("Desio se transfer");
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
                TempEquip.Clear();
                foreach (Equipment eq in room.Equipment)
                {

                    TempEquip.Add(eq);
                }
            }
            else
            {
                TempEquip = new ObservableCollection<Equipment>();
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
            TempEquip.Clear();
            foreach (Equipment eq in room.Equipment)
            {

                TempEquip.Add(eq);
            }
        }
    }
}
