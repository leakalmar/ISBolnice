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
using System.Collections.ObjectModel;


namespace Hospital_IS
{
  
    public partial class TransferDynamic : Window
    {
        public ObservableCollection<Equipment> SourceEquip { get; set; }
        public ObservableCollection<Equipment> DestinationEquip { get; set; }

        public ObservableCollection<Room> SourceRoom { get; set; }

        public ObservableCollection<Room> DestinationRoom { get; set; }

        private Room currentRoom;
        private int currentIndex;
        public TransferDynamic(Room room, int index)
        {
            InitializeComponent();
            currentIndex = index;
            currentRoom = room;
            this.DataContext = this;
            SourceRoom = new ObservableCollection<Room>();
            DestinationRoom = new ObservableCollection<Room>();
            SourceEquip = new ObservableCollection<Equipment>();
            DestinationEquip = new ObservableCollection<Equipment>();

            foreach(Room r in Hospital.Room)
            {
                if (r.Type == RoomType.StorageRoom)
                SourceRoom.Add(r);
           
            }
            

           
           
        }

        private void ComboSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Room room = (Room)ComboSource.SelectedItem;
            if(room.Equipment != null)
            {
                SourceEquip.Clear();
                foreach(Equipment eq in room.Equipment)
                {
                    if(eq.EquipType == EquiptType.Dynamic)
                    {
                        SourceEquip.Add(eq);
                    }
                   
                }
               
            }
            else
            {
                SourceEquip = new ObservableCollection<Equipment>();
            }     
        }

     

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Convert.ToInt32(QuantityBox.Text);
            if (quantity <= 0)
            {
                MessageBox.Show("Unesite ispravnu kolicinu");
                QuantityBox.Text = "";
            }

            Room roomSource = (Room)ComboSource.SelectedItem;

            Equipment equip = (Equipment)DataGridSource.SelectedItem;

            bool isSucces = Hospital.Instance.TransferEquipment(roomSource, equip, quantity);
            if (!isSucces)
            {
                MessageBox.Show("Nedovoljna kolicina");
            }
            else
            {
                MessageBox.Show("Uspjesno izvrseno");
                refreshGrid(roomSource);
            }
           
        }

        private void refreshGrid(Room sourceRooom)
        {
           
            if (sourceRooom.Equipment != null)
            {
                SourceEquip.Clear();
                foreach (Equipment eq in sourceRooom.Equipment)
                {


                    SourceEquip.Add(eq);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            EquipmentOption option = new EquipmentOption(currentRoom, currentIndex);
            option.Show();
            this.Hide();
           
           
        }
    }
}
