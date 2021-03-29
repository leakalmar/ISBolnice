using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Window
    {
        public Model.Patient Patient { get; set; } = new Model.Patient();
        public ObservableCollection<Model.Patient> Patients { get; set; } = new ObservableCollection<Model.Patient>();


        public PatientRegistration(ObservableCollection<Model.Patient> patients)
        {
            InitializeComponent();
            this.DataContext = this;
            Patients = patients;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
                Patient.IsGuest = true;

            if (comboBox.SelectedIndex == 0)
                Patient.Gender = "Žensko";
            else
                Patient.Gender = "Muško";


            Patient.Password = passwordBox.ToString();
            Patients.Add(Patient);

            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.SavePatient(Patient);

            this.Close();
        }
    }
}
