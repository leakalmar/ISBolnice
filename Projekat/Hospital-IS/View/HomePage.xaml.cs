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
    public partial class HomePage : Window
    {
        public Model.Doctor Doctor { get; set; }
        public HomePage()
        {
            InitializeComponent();
            List<Model.WorkDay> dani = new List<Model.WorkDay>
            {
                new Model.WorkDay("Pon", DateTime.Now, DateTime.Now)
            };
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Model.Doctor doc = new Model.Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani, spec);
            this.Doctor = doc;
            docotrAppointments.DataContext = Doctor;
        }
    }
}
