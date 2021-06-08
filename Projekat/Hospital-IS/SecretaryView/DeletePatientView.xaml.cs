using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DeletePatientView.xaml
    /// </summary>
    public partial class DeletePatientView : Window
    {
        public PatientDTO Patient { get; set; }
        public UpdatePatientView upv;
        public DeletePatientView(PatientDTO patient, UpdatePatientView upv)
        {
            InitializeComponent();
            this.Patient = patient;
            this.upv = upv;

            this.DataContext = this;
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            SecretaryManagementController.Instance.DeletePatient(Patient);
            upv.ucp.RefreshGrid();
            this.Close();
            upv.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
