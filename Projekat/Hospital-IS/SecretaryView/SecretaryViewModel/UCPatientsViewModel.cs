using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Hospital_IS.SecretaryView.SecretaryViewModel
{
    public class UCPatientsViewModel : BindableBase
    {
        private ObservableCollection<Patient> patients { get; set; }
        public ObservableCollection<Patient> Patients 
        { 
            get { return patients; }
            set
            {
                if (patients != value)
                {
                    patients = value;
                    OnPropertyChanged("Patients");
                }
            }
        }

        private Patient selectedPatient;

        public MyICommand ShowPatient { get; set; }
        public MyICommand UpdatePatient { get; set; }
        public MyICommand DeletePatient { get; set; }
        public MyICommand RegisterPatient { get; set; }
        public MyICommand ShowAllGuests { get; set; }
        public UCPatientsViewModel()
        {
            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAllRegisteredPatients());

            ShowPatient = new MyICommand(ShowSelectedPatient);
            UpdatePatient = new MyICommand(UpdateSelectedPatient);
            DeletePatient = new MyICommand(RemovePatient);
            RegisterPatient = new MyICommand(AddNewPatient);
            ShowAllGuests = new MyICommand(ShowGuests);
        }


        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (selectedPatient != value)
                {
                    selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }

        public void AddNewPatient()
        {
            PatientRegistrationView prv = new PatientRegistrationView();
            prv.Show();
        }

        public void ShowSelectedPatient()
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Izaberite pacijenta!");
            }
            else
            {
                PatientView pv = new PatientView(SelectedPatient.Id);
                pv.Show();
            }
        }

        public void UpdateSelectedPatient()
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Izaberite pacijenta!");
            }
            else
            {
                UpdatePatientView upv = new UpdatePatientView(SelectedPatient, null);
                upv.Show();
            }
        }

        public void RemovePatient()
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Izaberite pacijenta!");
            }
            else
            {
                PatientController.Instance.DeletePatient(SelectedPatient);
                Patients.Remove(SelectedPatient);
            }
        }

        public void ShowGuests()
        {
            GuestsView gv = new GuestsView(null);
            gv.Show();
        }

    }


}
