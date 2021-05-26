using Hospital_IS.DTOs;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorDTO Doctor { get; set; }
        public DoctorView(DoctorDTO doctor)
        {
            InitializeComponent();
            Doctor = doctor;
            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
