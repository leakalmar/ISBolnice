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

    public partial class TransferStatic : Window
    {
        public ObservableCollection<Equipment> SourceEquip { get; set; }
        public ObservableCollection<Equipment> DestinationEquip { get; set; }

        public ObservableCollection<Room> SourceRoom { get; set; }

        public ObservableCollection<Room> DestinationRoom { get; set; }

        
        public TransferStatic()
        {
            InitializeComponent();
            this.DataContext = this;
            SourceRoom = new ObservableCollection<Room>();
            DestinationRoom = new ObservableCollection<Room>();
            SourceEquip = new ObservableCollection<Equipment>();
            DestinationEquip = new ObservableCollection<Equipment>();

            DataGridDestination.IsEnabled = false;
            foreach (Room r in Hospital.Room)
            {
                SourceRoom.Add(r);
                DestinationRoom.Add(r);
            }




        }

        private void ComboSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Room room = (Room)ComboSource.SelectedItem;
            Room destiantionRoom = (Room)ComboDestionation.SelectedItem;

            
            if(room != null && destiantionRoom != null)
            {
                if (room.RoomId == destiantionRoom.RoomId)
                {
                    MessageBox.Show("Nije dozvoljeno birati iste sobe");
                    ComboSource.SelectedIndex = -1;
                    room = (Room)ComboSource.SelectedItem;
                    SourceEquip.Clear();
                }
            }

            if(room != null)
            {
                if (room.Equipment != null)
                {
                    SourceEquip.Clear();
                    foreach (Equipment eq in room.Equipment)
                    {
                        if (eq.EquipType == EquiptType.Stationary)
                            SourceEquip.Add(eq);
                    }

                }
                else
                {
                    SourceEquip = new ObservableCollection<Equipment>();
                }
            }
            
            
        }

        private void ComboDestionation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            Room room = (Room)ComboDestionation.SelectedItem;
            Room sourceRoom = (Room)ComboSource.SelectedItem;

            if (room != null && sourceRoom != null)
            {
                
                if (room.RoomId == sourceRoom.RoomId)
                {
                    MessageBox.Show("Nije dozvoljeno birati iste sobe");
                    ComboDestionation.SelectedIndex = -1;
                    room = (Room)ComboDestionation.SelectedItem;
                    DestinationEquip.Clear();
                }
            }
            if(room != null)
            {
                if (room.Equipment != null)
                {
                    DestinationEquip.Clear();
                    foreach (Equipment eq in room.Equipment)
                    {
                        if (eq.EquipType == EquiptType.Stationary)
                            DestinationEquip.Add(eq);
                    }
                }
                else
                {
                    DestinationEquip = new ObservableCollection<Equipment>();
                }
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
            Room roomDestination = (Room)ComboDestionation.SelectedItem;

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
                    TransferAppointment transferAppointment = new TransferAppointment(roomSource, roomDestination, equip, quantity);
                    transferAppointment.Show();
                    this.Hide();
                }
            }
        }

        private void refreshGrid(Room sourceRooom, Room destinationRoom)
        {
            if (destinationRoom.Equipment != null)
            {
                DestinationEquip.Clear();
                foreach (Equipment eq in destinationRoom.Equipment)
                {


                    DestinationEquip.Add(eq);
                }
            }


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
            EquipmentOption option = new EquipmentOption();
            option.Show();
            this.Hide();
        }
    }
}
