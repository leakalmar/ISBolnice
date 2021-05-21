﻿using Hospital_IS.ManagerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ManagerProfileOptionsView.xaml
    /// </summary>
    public partial class ManagerProfileOptionsView : Page
    {
        public ManagerProfileOptionsView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ManagerProfileOptionsVIewModel(this.NavigationService);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            window.Close();
        }
    }
}
