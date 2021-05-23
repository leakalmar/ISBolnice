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
using System.Windows.Shapes;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientNoteView.xaml
    /// </summary>
    public partial class PatientNoteView : Window
    {
        public PatientNoteViewModel AppointmentNoteViewModel { get; set; }
               
        public PatientNoteView(int appointmentId)
        {
            AppointmentNoteViewModel = new PatientNoteViewModel(appointmentId);
            this.DataContext = AppointmentNoteViewModel;
            InitializeComponent();
        }
    }
}
