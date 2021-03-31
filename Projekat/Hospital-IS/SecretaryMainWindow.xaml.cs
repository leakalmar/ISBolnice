using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        public ObservableCollection<Model.Patient> Patients { get; set; }
        Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();

        private static SecretaryMainWindow instance = null;

        public static SecretaryMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryMainWindow();
                }
                return instance;
            }
        }

        private SecretaryMainWindow()
        {
            InitializeComponent();

            RefreshGrid();

            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

            this.DataContext = this;

            /*ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Patient p1 = new Model.Patient(1111, "Marko", "Marković", new DateTime(1998, 12, 18), "markoa@gmail.com", "sifra", "Cara Dušana 4", DateTime.Now, "", alergije);
            Model.Patient p2 = new Model.Patient(1212, "Nikolina", "Perić", new DateTime(1972, 10, 12), "nikolina@gmail.com", "sifra", "Bulevar Evrope 11", DateTime.Now, "", null);
            Model.Patient p3 = new Model.Patient(2314, "Ana", "Zorić", new DateTime(1995, 11, 12), "ana@gmail.com", "sifra", "Bulevar Evrope 21", DateTime.Now, "", null);
            Model.Patient p4 = new Model.Patient(1234, "Petar", "Petrović", new DateTime(2000, 12, 9), "petar@gmail.com", "sifra", "Šekspirova 18", DateTime.Now, "", null);
            Patients.Add(p1);
            Patients.Add(p2);
            Patients.Add(p3);
            Patients.Add(p4);*/

        }

        public void RefreshGrid()
        {
            if (Patients != null)
                Patients.Clear();
            List<Model.Patient> patients = pfs.GetAll();
            Patients = new ObservableCollection<Model.Patient>(patients);
            dataGridPatients.ItemsSource = Patients;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
        }

        private void AddNewPatient(object sender, RoutedEventArgs e)
        {
            PatientRegistration pr = new PatientRegistration();
            pr.Show();
        }

        private void ShowPatient(object sender, RoutedEventArgs e)
        {
            Model.Patient patient = (Model.Patient) dataGridPatients.SelectedItem;
            PatientView pv = new PatientView(patient);
            pv.Show();
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            Model.Patient patient = (Model.Patient)dataGridPatients.SelectedItem;
            UpdatePatientView upv = new UpdatePatientView(patient);
            upv.Show();
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            Model.Patient patient = (Model.Patient)dataGridPatients.SelectedItem;
            Patients.Remove(patient);
            pfs.DeletePatient(patient);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Visibility = Visibility.Hidden;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is DateTime && (DateTime)value < new DateTime(2, 1, 1))
            {
                return "";
            }
            else
                return value;
        }
    }
}