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
    /// Interaction logic for CalendarApponitmentView.xaml
    /// </summary>
    public partial class CalendarAppointmentView : UserControl
    {
        public CalendarAppointmentView()
        {
            InitializeComponent();
            this.DataContext = new CalendarAppointmentViewModel();
        }
    }
}
