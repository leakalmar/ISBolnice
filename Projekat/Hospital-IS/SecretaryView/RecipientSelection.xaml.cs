using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for RecipientSelection.xaml
    /// </summary>
    public partial class RecipientSelection : Window
    {
        public ObservableCollection<PatientDTO> Patients { get; set; }
        public ObservableCollection<DoctorDTO> Doctors { get; set; }

        CreateNotification cn;

        public RecipientSelection(CreateNotification cn)
        {
            InitializeComponent();

            this.cn = cn;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());

            this.DataContext = this;
        }



        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            foreach (var patient in lbPatients.SelectedItems)
            {
                for (int i = 0; i < lbPatients.Items.Count; i++)
                {
                    if (patient.Equals(lbPatients.Items[i]))
                        cn.Ids.Add(Patients[i].Id);
                }
            }

            foreach (var doctor in lbDoctors.SelectedItems)
            {
                for (int i = 0; i < lbDoctors.Items.Count; i++)
                {
                    if (doctor.Equals(lbDoctors.Items[i]))
                        cn.Ids.Add(Doctors[i].Id);
                }
            }

            cn.rbSelectAll.IsChecked = false;
            cn.rbSelectSome.IsChecked = true;

            this.Close();
        }

        private void SearchButtonPatoents_Click(object sender, RoutedEventArgs e)
        {
            string[] search = txtSearchPatients.Text.ToLower().Split(" ");
            ICollectionView view = new CollectionViewSource { Source = Patients }.View;
            view.Filter = delegate (object item)
            {
                PatientDTO patient = item as PatientDTO;
                return CheckIfPersonMeetsSearchCriteria(patient.Name, patient.Surname, patient.BirthDate, search);
            };
            lbPatients.ItemsSource = view;
        }

        private void SearchButtonDoctors_Click(object sender, RoutedEventArgs e)
        {
            string[] search = txtSearchEmployees.Text.ToLower().Split(" ");
            ICollectionView view2 = new CollectionViewSource { Source = Doctors }.View;
            view2.Filter = delegate (object item)
            {
                DoctorDTO doctor = item as DoctorDTO;
                return CheckIfPersonMeetsSearchCriteria(doctor.Name, doctor.Surname, doctor.BirthDate, search);
            };
            lbDoctors.ItemsSource = view2;
        }

        private bool CheckIfPersonMeetsSearchCriteria(string name, string surname, DateTime bday, string[] search)
        {


            if (search.Length <= 1)
                return name.ToLower().Contains(search[0]) | surname.ToLower().Contains(search[0]) | bday.ToString("dd.MM.yyyy.").Contains(search[0]);
            else
            {
                bool FirstName = true;
                bool LastName = true;
                bool BirthDate = true;
                int cnt = 0;

                for (int i = 0; i < search.Length; i++)
                {
                    if (name.ToLower().Contains(search[i]) && FirstName)
                    {
                        FirstName = false;
                        cnt++;
                        continue;
                    }
                    if (surname.ToLower().Contains(search[i]) && LastName)
                    {
                        LastName = false;
                        cnt++;
                        continue;
                    }
                    if (bday.ToString("dd.MM.yyyy.").Contains(search[i]) && BirthDate)
                    {
                        BirthDate = false;
                        cnt++;
                        continue;
                    }
                }

                return cnt == search.Length;
            }

        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name.ToString().Equals("btnSelectAllPatients"))
            {
                lbPatients.SelectAll();
            }
            else
            {
                lbDoctors.SelectAll();
            }
        }

        private void DeselectAll(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name.ToString().Equals("btnDeselectAllPatients"))
            {
                lbPatients.UnselectAll();
            }
            else
            {
                lbDoctors.UnselectAll();
            }
        }
    }

}
