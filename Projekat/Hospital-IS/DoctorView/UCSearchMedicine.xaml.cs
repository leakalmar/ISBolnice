using Controllers;
using DoctorView;
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
        public Patient Patient { get; set; }
        public ObservableCollection<MedicineObject> medicineObjects { get; set; }
        public UCPatientChart PatientChart { get; set; }
        public UCSearchMedicine(UCPatientChart patientChart)
        {
            InitializeComponent();
            PatientChart = patientChart;
           // Patient = patientChart.Patient;
            medicines.DataContext = GenerateListOfMedicines();
            alergies.DataContext = Patient.Alergies;
            //medicines.SelectedItem = medicines.Items[0];
            //medInfo.DataContext = medicines.Items[0];
        }

        private object GenerateListOfMedicines()
        {
            /* medicineObjects = new ObservableCollection<MedicineObject>();
             foreach (Prescription p in PatientChart.ReportView.Prescriptions)
             {
                 medicineObjects.Add(new MedicineObject(p.Medicine, true, false));
             }
             foreach (Medicine med in MedicineController.Instance.GetAll())
             {
                 //odvojiti
                 bool found = false;
                 foreach (Prescription p in PatientChart.ReportView.Prescriptions)
                 {
                     if (med.Name.Equals(p.Medicine.Name))
                     {
                         found = true;
                     }
                 }
                 if (!found)
                 {
                     bool allergic = PatientController.Instance.CheckIfAllergicToMedicine(Patient, med);
                     medicineObjects.Add(new MedicineObject(med, false, allergic));
                 }
             }
             return medicineObjects;*/
            return null;
        }

        private void addMedicine_KeyDown(object sender, KeyEventArgs e)
        {
           /* MedicineObject med = (MedicineObject)medicines.SelectedItem;

            if (med.Allergic == true)
            {
                ExitMess mess = new ExitMess("Pacijent je alergican na izabrani lek!");
                mess.btnCancle.Visibility = Visibility.Collapsed;
                mess.btnOk.Content = "U redu";
                mess.ShowDialog();
                return;

            }

            if (e.Key.Equals(Key.Enter))
            {
                if (med.Check == false)
                {
                    PatientChart.ReportView.Prescriptions.Add(new Prescription(med.Medicine, PatientChart.Appointment.AppointmentStart));
                    med.Check = true;
                    medicines.Items.Refresh();
                    search.Focus();
                }
                else
                {
                    PatientChart.ReportView.Prescriptions.Remove(new Prescription(med.Medicine, PatientChart.Appointment.AppointmentStart));
                    med.Check = false;
                    medicines.Items.Refresh();
                    search.Focus();
                }

            }
            else if (e.Key == Key.Delete)
            {
                foreach (Prescription prescription in PatientChart.ReportView.Prescriptions)
                {
                    if (prescription.Medicine.Name.Equals(med.Medicine.Name))
                    {
                        PatientChart.ReportView.Prescriptions.Remove(prescription); med.Check = false;
                        medicines.Items.Refresh();
                        search.Focus();
                        return;
                    }
                }
            }*/
        }

        private void back_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                //DoctorHomePage.Instance.Home.Children.Remove(this);
                PatientChart.Visibility = Visibility.Visible;
            }
        }

        private void medicines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*MedicineObject med = (MedicineObject)medicines.SelectedItem;
            if (PatientController.Instance.CheckIfAllergicToComponent(PatientChart.Patient, med.Medicine))
            {
                allergieMess.Visibility = Visibility.Visible;
            }
            else
            {
                allergieMess.Visibility = Visibility.Collapsed;
            }
            medInfo.DataContext = med.Medicine;*/
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            MedicineObject med = (MedicineObject)medicines.SelectedItem;
            medInfo.DataContext = med.Medicine;
        }

        private void medicines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           /* MedicineObject med = (MedicineObject)medicines.SelectedItem;

            if (med.Allergic == true)
            {
                ExitMess mess = new ExitMess("Pacijent je alergican na izabrani lek!");
                mess.btnCancle.Visibility = Visibility.Collapsed;
                mess.btnOk.Content = "U redu";
                mess.ShowDialog();
                return;

            }

            if (med.Check == false)
            {
                PatientChart.ReportView.Prescriptions.Add(new Prescription(med.Medicine, PatientChart.Appointment.AppointmentStart));
                med.Check = true;
                medicines.Items.Refresh();
                search.Focus();
            }
            else
            {
                foreach (Prescription prescription in PatientChart.ReportView.Prescriptions)
                {
                    if (prescription.Medicine.Name.Equals(med.Medicine.Name))
                    {
                        PatientChart.ReportView.Prescriptions.Remove(prescription); med.Check = false;
                        medicines.Items.Refresh();
                        search.Focus();
                        return;
                    }
                }
                
                
            }*/
        }
    }
}
