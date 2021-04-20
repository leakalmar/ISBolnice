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
using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for ExitMess.xaml
    /// </summary>
    public partial class ExitMess : Window
    {
        public ExitMess(string question)
        {
            InitializeComponent();
            lblQuestion.Text = question;
        }

        public void BtnClickPotvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void BtnClickOdustani(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
