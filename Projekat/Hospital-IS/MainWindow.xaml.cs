﻿using Hospital_IS.View;
using Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
        public static Patient User { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            List<Patient> patients = pfs.GetAll();

            foreach (Patient p in patients)
            {
                if (email.Text == p.Email && password.Password.ToString() == p.Password)
                {
                    User = p;
                    HomePatient.Instance.Show();
                    this.Close();
                }
            }

            if (email.Text == "upravnik@gmail.com" && password.Password.ToString() == "upravnik")
            {
                Window1 win = new Window1();
                win.Show();
            }
            else if (email.Text == "doktor@gmail.com" && password.Password.ToString() == "doktor")
            {
            }
            else if (email.Text == "sekretar@gmail.com" && password.Password.ToString() == "sekretar")
            {
                SecretaryMainWindow smw = new SecretaryMainWindow();
                smw.Show();
                this.Close();
            }
            else if (email.Text == "pacijent@gmail.com" && password.Password.ToString() == "pacijent")
            {
                
            }
        }
    }
}
