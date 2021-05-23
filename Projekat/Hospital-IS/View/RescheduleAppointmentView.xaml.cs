using Model;
using Hospital_IS.View.PatientViewModels;
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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for RescheduleAppointmentView.xaml
    /// </summary>
    public partial class RescheduleAppointmentView : UserControl
    {
        public RescheduleAppointmentViewModel Res { get; set; }
        public RescheduleAppointmentView()
        {
            Res = new RescheduleAppointmentViewModel();
            this.DataContext = Res;
            InitializeComponent();
            Calendar.DisplayDateStart = Res.Date;
            Calendar.DisplayDateEnd = Res.Date.AddDays(3);
        }
    }
}
