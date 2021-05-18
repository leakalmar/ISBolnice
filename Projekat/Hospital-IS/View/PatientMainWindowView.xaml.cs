using Model;
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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindowView : Window
    {
        private static PatientMainWindowView instance = null;
        public static PatientMainWindowView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientMainWindowView();
                }
                return instance;
            }
        }

        public Patient Patient { get; set; }
        private PatientMainWindowView()
        {
            Patient = MainWindow.PatientUser;
            InitializeComponent();
        }

        private void LogoutPatient(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
