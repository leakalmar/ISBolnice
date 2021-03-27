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


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for AddNewRoom.xaml
    /// </summary>
    public partial class AddNewRoom : Window
    {
        public AddNewRoom()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int brojSobe = int.Parse(text1.Text);
            bool zauzeto = (bool)check1.IsChecked;
            RoomType tip;
            if (combo1.Text.Equals("Soba za odmor"))
            {
                tip = RoomType.RecoveryRoom;
            }
            else if(combo1.Text.Equals("Operaciona soba"))
            {
                tip = RoomType.OperationRoom;
            } else 
            {
                tip = RoomType.ConsultingRoom;
            }
        }
    }
}
