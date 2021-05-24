using Controllers;
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

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for GuestsView.xaml
    /// </summary>
    public partial class GuestsView : Window
    {
        public ObservableCollection<Patient> Guests { get; set; }
        public UCPatientsView ucp;
        public GuestsView(UCPatientsView ucp)
        {
            InitializeComponent();
            this.ucp = ucp;

            RefreshGrid();

            this.DataContext = this;
        }

        public void RefreshGrid()
        {
            if (Guests != null)
                Guests.Clear();

            PatientController.Instance.ReloadPatients();
            Guests = new ObservableCollection<Patient>(PatientController.Instance.GetAllGuests());
            dataGridGuests.ItemsSource = Guests;
        }
        private void RegisterPatient(object sender, RoutedEventArgs e)
        {
            if ((Patient)dataGridGuests.SelectedItem != null)
            {
                Patient guest = (Patient)dataGridGuests.SelectedItem;
                PatientRegistrationView pr = new PatientRegistrationView(ucp, this);
                pr.Show();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
