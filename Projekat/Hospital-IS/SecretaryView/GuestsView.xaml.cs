﻿using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for GuestsView.xaml
    /// </summary>
    public partial class GuestsView : Window
    {
        public ObservableCollection<PatientDTO> Guests { get; set; }
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
            Guests = new ObservableCollection<PatientDTO>(SecretaryController.Instance.GetAllGuests());
            dataGridGuests.ItemsSource = Guests;
        }
        private void RegisterPatient(object sender, RoutedEventArgs e)
        {
            if ((PatientDTO)dataGridGuests.SelectedItem != null)
            {
                PatientDTO guest = (PatientDTO)dataGridGuests.SelectedItem;
                PatientRegistration pr = new PatientRegistration(ucp, this);
                pr.Show();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
