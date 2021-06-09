using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
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

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DeleteDoctorView.xaml
    /// </summary>
    public partial class DeleteDoctorView : Window
    {
        public DoctorDTO Doctor { get; set; }
        public UpdateDoctorView ud;
        public DeleteDoctorView(DoctorDTO doctor, UpdateDoctorView ud)
        {
            InitializeComponent();
            this.Doctor = doctor;
            this.ud = ud;

            this.DataContext = this;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            DoctorController.Instance.DeleteDoctor(Doctor);
            ud.udv.RefreshGrid();
            this.Close();
            ud.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
