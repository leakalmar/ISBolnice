using Enums;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public PatientDTO Patient { get; set; }
        public PatientView(PatientDTO patient)
        {
            InitializeComponent();
            Patient = patient;
            this.DataContext = this;
            if (patient.Education.Equals(EducationCategory.NA))
                eduTxt.Text = " ";
            else if (patient.Education.Equals(EducationCategory.GradeSchool))
                eduTxt.Text = "Osnovna škola";
            else if (patient.Education.Equals(EducationCategory.HighSchool))
                eduTxt.Text = "Srednja škola";
            else if (patient.Education.Equals(EducationCategory.College))
                eduTxt.Text = "Fakultet";
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
