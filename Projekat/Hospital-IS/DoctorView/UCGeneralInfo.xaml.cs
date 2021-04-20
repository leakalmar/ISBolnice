﻿using Model;
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

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCGeneralInfo.xaml
    /// </summary>
    public partial class UCGeneralInfo : UserControl
    {
        public bool Started { get; set; }
        public UCGeneralInfo(Patient p)
        {
            InitializeComponent();
            info.DataContext = p;

            if (!Started)
            {
                change.Visibility = Visibility.Collapsed;
            }
            else
            {
                change.Visibility = Visibility.Visible;
            }
        }
    }
}
