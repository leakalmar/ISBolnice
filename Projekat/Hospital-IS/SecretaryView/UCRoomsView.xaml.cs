using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCRoomsView.xaml
    /// </summary>
    public partial class UCRoomsView : UserControl
    {
        public ObservableCollection<RoomDTO> Rooms { get; set; }
        public UCRoomsView()
        {
            InitializeComponent();
            Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetAllRooms());

            this.DataContext = this;
        }

        private void ShowRoom(object sender, RoutedEventArgs e)
        {
            if ((RoomDTO)dataGridRooms.SelectedItem != null)
            {
                RoomDTO room = (RoomDTO)dataGridRooms.SelectedItem;
                RoomView rv = new RoomView(room);
                rv.Show();
            }
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = new CollectionViewSource { Source = Rooms }.View;
            view.Filter = delegate (object item)
            {
                RoomDTO room = item as RoomDTO;
                return CheckIfPatientMeetsSearchCriteria(room);
            };
            dataGridRooms.ItemsSource = view;
        }

        private bool CheckIfPatientMeetsSearchCriteria(RoomDTO room)
        {
            string[] search = txtSearch.Text.ToLower().Split(" ");
            bool found = false;
            if (txtSearch.Text.Equals("Pretraži..."))
                search[0] = string.Empty;

            if (search.Length <= 1)
                if ("magacin".Contains(search[0].ToLower()) && room.Type.Equals(RoomType.StorageRoom))
                    found = true;
                else if ("operaciona sala".Contains(search[0].ToLower()) && room.Type.Equals(RoomType.OperationRoom))
                    found = true;
                else if ("konsultacije".Contains(search[0].ToLower()) && room.Type.Equals(RoomType.ConsultingRoom))
                    found = true;
                else if ("za oporavak".Contains(search[0].ToLower()) && room.Type.Equals(RoomType.RecoveryRoom))
                    found = true;
                else if (room.RoomId.ToString().Contains(search[0]))
                    found = true;
                else
                    found = false;

            return found;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Equals("Pretraži..."))
            {
                txtSearch.Text = string.Empty;
                txtSearch.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray);
                txtSearch.Text = "Pretraži...";
            }
        }
    }
}
