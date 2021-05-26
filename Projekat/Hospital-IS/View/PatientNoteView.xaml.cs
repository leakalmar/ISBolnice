using Hospital_IS.View.PatientViewModels;
using System.Windows;

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
