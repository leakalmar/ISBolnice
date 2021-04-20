using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCSearchMedicine.xaml
    /// </summary>
    public partial class UCSearchMedicine : UserControl
    {
        public MedicineFileStorage mfs = new MedicineFileStorage();
        public DoctorAppointment Appointment { get; set; }

        public Dictionary<Medicine,bool> added { get; set; } = new Dictionary<Medicine, bool>();
        public UCSearchMedicine(DoctorAppointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            foreach (Prescription p in appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
            {
                added.Add(p.Medicine, true);
            }
            foreach(Medicine med in mfs.GetAll())
            {
                bool found = false;
                foreach (Prescription p in appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (med.Name.Equals(p.Medicine.Name))
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    added.Add(med, false);
                }
            }

            
            medicines.DataContext = added;
            alergies.DataContext = appointment.Patient.Alergies;
            
            
        }

        private void addMedicine_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Enter))
            {
                KeyValuePair<Medicine, bool> med = (KeyValuePair<Medicine, bool>)medicines.SelectedItem;
                Appointment.Patient.MedicalHistory.AddPrescription(new Prescription(med.Key,Appointment.AppointmentStart));
                added[med.Key] = true;
                medicines.Items.Refresh();
                search.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                KeyValuePair<Medicine, bool> med = (KeyValuePair<Medicine, bool>)medicines.SelectedItem;
                foreach(Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (p.Medicine.Name.Equals(med.Key.Name))
                    {
                        Appointment.Patient.MedicalHistory.RemovePrescription(p);
                    }
                }
                added[med.Key] = false;
                medicines.Items.Refresh();
                search.Focus();
            }
        }

        private void back_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {

                DoctorHomePage.Instance.Home.Children.Remove(this);
                DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment,true));
            }
        }

        private void medicines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyValuePair<Medicine, bool> med = (KeyValuePair<Medicine, bool>)medicines.SelectedItem;
            medInfo.DataContext = med.Key;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            KeyValuePair<Medicine, bool> med = (KeyValuePair<Medicine, bool>)medicines.SelectedItem;
            medInfo.DataContext = med.Key;
        }

        private void medicines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            KeyValuePair<Medicine, bool> med = (KeyValuePair<Medicine, bool>)medicines.SelectedItem;

            if (!med.Value)
            {
                
                Appointment.Patient.MedicalHistory.AddPrescription(new Prescription(med.Key, Appointment.AppointmentStart));
                added[med.Key] = true;
                medicines.Items.Refresh();
                search.Focus();
            }
            else
            { 
                foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (p.Medicine.Name.Equals(med.Key.Name))
                    {
                        Appointment.Patient.MedicalHistory.RemovePrescription(p);
                    }
                }
                added[med.Key] = false;
                medicines.Items.Refresh();
                search.Focus();
            }
        }

        private void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
