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

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public Model.Patient Patient { get; set; } 
        public PatientView(Model.Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            this.DataContext = this;
            if (patient.Education.Equals(Model.EducationCategory.NA))
                eduTxt.Text = " ";
            else if (patient.Education.Equals(Model.EducationCategory.GradeSchool))
                eduTxt.Text = "Osnovna škola";
            else if (patient.Education.Equals(Model.EducationCategory.HighSchool))
                eduTxt.Text = "Srednja škola";
            else if (patient.Education.Equals(Model.EducationCategory.College))
                eduTxt.Text = "Fakultet";
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
