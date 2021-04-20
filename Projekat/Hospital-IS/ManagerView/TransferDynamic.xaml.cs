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
            SourceEquip = new ObservableCollection<Equipment>();
            DestinationEquip = new ObservableCollection<Equipment>();


            SourceRoom = Hospital.Instance.getRoomByType(RoomType.StorageRoom);
           
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
           bool isSucess = Validate();
            if (isSucess)
            {
                Room roomSource = (Room)ComboSource.SelectedItem;

                Equipment equip = (Equipment)DataGridSource.SelectedItem;
                int quantity = Convert.ToInt32(QuantityBox.Text);
                Hospital.Instance.TransferEquipment(roomSource, equip, quantity);
                refreshGrid(roomSource);
                MessageBox.Show("Uspjesan transfer");
            }
            else
            {
                MessageBox.Show("Provjerite unesene podatke i odabranu opremu");
            }
        }

        private bool Validate()
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
                
                isNumberOverZero = false;
            }
            Room roomSource = (Room)ComboSource.SelectedItem;

            Equipment equip = (Equipment)DataGridSource.SelectedItem;

            bool isCorrect = false;
            if (isNumberOverZero)
            {
                int quantity = Convert.ToInt32(QuantityBox.Text);
                if (roomSource == null || equip == null)
                {
                   
                }
                else if (!Hospital.Instance.CheckQuantity(roomSource, equip, quantity))
                {
                   
                    QuantityBox.Text = "";
                }
                else
                {
                    isCorrect = true;
                }
            }

            return isNumberOverZero && isCorrect;
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
