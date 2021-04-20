using Model;
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

namespace Hospital_IS
{
   
    public partial class TransferAppointment : Window
    {
        public ObservableCollection<Appointment> AllAppointments { get; set; }

        private Room sourceRoom;
        private Room destinationRoom;
        private Equipment equip;
        private int quantity;
        public TransferAppointment(Room sourceRoom, Room destinationRoom,Equipment equip,int quantity)
        {
            
          
            

            this.sourceRoom = sourceRoom;
            this.destinationRoom = destinationRoom;
            this.equip = equip;
            this.quantity = quantity;

            InitializeComponent();
            
            AllAppointments = Hospital.Instance.getAllAppByTwoRoom(destinationRoom.RoomId,sourceRoom.RoomId);
            int velicina = AllAppointments.Count;
            MessageBox.Show(Convert.ToString(velicina));
            this.DataContext = this;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TransferStatic transferStatic = new TransferStatic();
            this.Hide();
            transferStatic.Show();
        }

        private void AffirmTransfer_Click(object sender, RoutedEventArgs e)
        {
            String startDateAndTime = TransferStart.Text;
            DateTime dateTimeStart = Convert.ToDateTime(startDateAndTime);

            String endDateAndTime = TransferEnd.Text;
            DateTime dateTimeEnd = Convert.ToDateTime(endDateAndTime);


            if(dateTimeEnd < DateTime.Now || dateTimeStart < DateTime.Now || dateTimeStart >= dateTimeEnd)
            {
                MessageBox.Show("Izaberite odgovarajuci datum i vrijeme");
            }

           bool isSucces = Hospital.Instance.TransferEquipmentStatic(sourceRoom, destinationRoom, equip, quantity, dateTimeStart, dateTimeEnd, "dobar termin");

            if (!isSucces)
            {
                MessageBox.Show("Niste uspjeli da zakazete termin");
            }
            else
            {
                MessageBox.Show("Uspjesno zakazivanje termina");
                this.Hide();
                TransferStatic transferStatic = new TransferStatic();
                transferStatic.Show();
            }
           
        }

        private void TransferStart_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
