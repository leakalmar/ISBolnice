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
using Model;

namespace Hospital_IS.View
{
    public partial class DoctorHomePage : Window
    {
        public Model.Doctor Doctor { get; set; }
        public DoctorHomePage()
        {
            InitializeComponent();
            List<Model.WorkDay> dani = new List<Model.WorkDay>
            {
                new Model.WorkDay("Pon", DateTime.Now, DateTime.Now)
            };
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Model.Doctor doc = new Model.Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "doktor@gmail.com", "doktor", "Brace Radica 30", 60000.0, DateTime.Now, dani, spec, new Room(RoomType.ConsultingRoom, true, true, 1, 1));
            this.Doctor = doc;
            this.DataContext = new ViewModel.HomePage();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                this.Close();
            }
         }

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void HomePage_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModel.HomePage();
        }
    }
}
