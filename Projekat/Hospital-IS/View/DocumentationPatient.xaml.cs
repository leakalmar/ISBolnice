﻿using System;
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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for DocumentationPatient.xaml
    /// </summary>
    public partial class DocumentationPatient : Window
    {
        public DocumentationPatient()
        {
            InitializeComponent();
        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient.Instance.Show();
            this.Close();
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Close();
        }

        private void showAll(object sender, RoutedEventArgs e)
        {
            AllAppointments allApp = new AllAppointments();
            allApp.Show();
            this.Close();
        }
    }
}