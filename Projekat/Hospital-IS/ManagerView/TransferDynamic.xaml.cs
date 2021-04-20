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
using System.Linq;

namespace Hospital_IS
{
  
    public partial class TransferDynamic : Window
    {
        public ObservableCollection<Equipment> SourceEquip { get; set; }
        public ObservableCollection<Equipment> DestinationEquip { get; set; }

        public ObservableCollection<Room> SourceRoom { get; set; }

        public ObservableCollection<Room> DestinationRoom { get; set; }

        private static Room currentRoom;
        private static int currentIndex;
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

            bool isEmpty = false;
            if (QuantityBox.Text.Equals(""))
            {
                isEmpty = true;
            }
           



            bool isIntString = QuantityBox.Text.All(char.IsDigit);
            
            bool isNumberOverZero = true;
            if (!isIntString || isEmpty == true)
            { 
                MessageBox.Show("Unesite pozitivan broj broj");
                isNumberOverZero = false;
            }
            Room roomSource = (Room)ComboSource.SelectedItem;

            Equipment equip = (Equipment)DataGridSource.SelectedItem;


            if (isNumberOverZero)
            {
                int quantity = Convert.ToInt32(QuantityBox.Text);
                if (roomSource == null || equip == null)
                {
                    MessageBox.Show("Unesite sve podatke ispravno!");
                }
                else if (!Hospital.Instance.CheckQuantity(roomSource, equip, quantity))
                {
                    MessageBox.Show("Nedovoljna kolicina");
                    QuantityBox.Text = "";
                }
                else
                {
                    Hospital.Instance.TransferEquipment(roomSource, equip, quantity);
                    refreshGrid(roomSource);
                    MessageBox.Show("Uspjesan transfer");
                }
            }
           

        }

        private void refreshGrid(Room sourceRooom)
        {
           
            if (sourceRooom.Equipment != null)
            {
                SourceEquip.Clear();
                foreach (Equipment eq in sourceRooom.Equipment)
                {
                    if (eq.EquipType == EquiptType.Dynamic)
                    {
                        SourceEquip.Add(eq);
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
           
            EquipmentOption option = new EquipmentOption(currentRoom, currentIndex);
            option.Show();
            this.Close();
        }
    }
}
