using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for RecipientSelection.xaml
    /// </summary>
    public partial class RecipientSelection : Window
    {
        public ObservableCollection<PatientDTO> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        CreateNotification cn;

        public RecipientSelection(CreateNotification cn)
        {
            InitializeComponent();

            this.cn = cn;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());

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
    }

}
