using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCSearchMedicine.xaml
    /// </summary>
    public partial class UCSearchMedicine : UserControl
    {
        public MedicineFileStorage mfs = new MedicineFileStorage();
        public DoctorAppointment Appointment { get; set; }

        public ObservableCollection<MedicineObject> medicineObjects { get; set; }
        public UCSearchMedicine(DoctorAppointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            
            medicines.DataContext = GenerateListOfMedicines();
            alergies.DataContext = appointment.Patient.Alergies;
            
            
        }

        private bool CheckAlergies(Medicine med)
        {
            foreach(String allergie in Appointment.Patient.Alergies)
            {
                if (med.Name.ToLower().Contains(allergie.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private object GenerateListOfMedicines()
        {
            medicineObjects = new ObservableCollection<MedicineObject>();
            foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
            {
                medicineObjects.Add(new MedicineObject(p.Medicine,true,false));
            }
            foreach (Medicine med in mfs.GetAll())
            {
                bool found = false;
                foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (med.Name.Equals(p.Medicine.Name))
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    bool allergic = CheckAlergies(med);
                    medicineObjects.Add(new MedicineObject(med,false,allergic));
                }
            }
            return medicineObjects;
        }

        private void addMedicine_KeyDown(object sender, KeyEventArgs e)
        {
            MedicineObject med = (MedicineObject)medicines.SelectedItem;

            if(med.Allergic == true)
            {
                ExitMess mess= new ExitMess("Pacijent je alergican na izabrani lek!");
                mess.btnCancle.Visibility = Visibility.Collapsed;
                mess.btnOk.Content = "U redu";
                mess.ShowDialog();
                return;
                
            }

            if (e.Key.Equals(Key.Enter))
            {
                Appointment.Patient.MedicalHistory.AddPrescription(new Prescription(med.Medicine,Appointment.AppointmentStart));
                med.Check = true;
                medicines.Items.Refresh();
                search.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (p.Medicine.Name.Equals(med.Medicine.Name))
                    {
                        Appointment.Patient.MedicalHistory.RemovePrescription(p);
                    }
                }
                med.Check = false;
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
            MedicineObject med = (MedicineObject)medicines.SelectedItem;
            if (checkMedicationComposition(med.Medicine))
            {
                allergieMess.Visibility = Visibility.Visible;
            }
            else
            {
                allergieMess.Visibility = Visibility.Collapsed;
            }
            medInfo.DataContext = med.Medicine;
        }

        private bool checkMedicationComposition(Medicine medicine)
        {
            String[] components = medicine.Composition.Split(",");
            foreach(String c in components)
            {
                foreach (String allergie in Appointment.Patient.Alergies)
                {
                    Medicine med = findMedicine(allergie);
                    if(med != null)
                    {
                        String[] allergieComponents = med.Composition.Split(",");
                        foreach(String allergieComp in allergieComponents)
                        {
                            if (c.ToLower().Equals(allergieComp.ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                    
                }
            }
            
            return false;
        }

        private Medicine findMedicine(String allergie)
        {
            foreach(Medicine med in mfs.GetAll())
            {
                if (med.Name.ToLower().Contains(allergie.ToLower()))
                {
                    return med;
                }
            }
            return null;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            MedicineObject med = (MedicineObject)medicines.SelectedItem;
            medInfo.DataContext = med.Medicine;
        }

        private void medicines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MedicineObject med = (MedicineObject)medicines.SelectedItem;

            if (med.Allergic == true)
            {
                ExitMess mess = new ExitMess("Pacijent je alergican na izabrani lek!");
                mess.btnCancle.Visibility = Visibility.Collapsed;
                mess.btnOk.Content = "U redu";
                mess.ShowDialog();
                return;

            }

            if (!med.Check)
            {
                Appointment.Patient.MedicalHistory.AddPrescription(new Prescription(med.Medicine, Appointment.AppointmentStart));
                med.Check = true;
                medicines.Items.Refresh();
                search.Focus();
            }
            else
            {
                foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (p.Medicine.Name.Equals(med.Medicine.Name))
                    {
                        Appointment.Patient.MedicalHistory.RemovePrescription(p);
                    }
                }
                med.Check = false;
                medicines.Items.Refresh();
                search.Focus();
            }
        }

        private void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
