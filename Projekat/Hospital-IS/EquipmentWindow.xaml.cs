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
using Model;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for EquipmentWindow.xaml
    /// </summary>
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
           
           
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.Show();
            this.Hide();
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)Combo.SelectedItem;
            MessageBox.Show(Convert.ToString(room.Equipment.Count));
            TempEquip.Clear();
            foreach(Equipment eq in room.Equipment)
            {
                
                TempEquip.Add(eq);
            }   
        }

        private void DataGridEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentOption equipmentOpt = new EquipmentOption((Room)Combo.SelectedItem,Combo.SelectedIndex);
            equipmentOpt.Show();
            this.Hide();

        }


    }
}
