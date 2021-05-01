using Controllers;
using DoctorView;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView
{
    /// <summary>
    /// Interaction logic for ChooseRecipient.xaml
    /// </summary>
    
    public partial class ChooseRecipient : Window
    {

        private String nameClass;
        private String sideEffectClass;
        private String therapeuticClass;
        private List<ReplaceMedicineName> medicineNamesClass;
        private List<MedicineComponent> medicineComponentsClass;
        public ChooseRecipient(string name, string sideEffect,string therapeutic, List<ReplaceMedicineName> medicineNames,List<MedicineComponent> medicineComponents )
        {
            nameClass = name;
            sideEffectClass = sideEffect;
            therapeuticClass = therapeutic;
            medicineComponentsClass = medicineComponents;
            medicineNamesClass = medicineNames;
            InitializeComponent();
            DataGridDoctors.DataContext = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

            List<int> doctorsIds = new List<int>();

            foreach (var doctor in DataGridDoctors.SelectedItems)
            {
                Doctor doc = (Doctor)doctor;
                doctorsIds.Add(doc.Id);
            }


            MedicineNotificationController.Instance.CreateNotification(nameClass, sideEffectClass, therapeuticClass, medicineNamesClass, medicineComponentsClass, doctorsIds);
            this.Close();
        }
    }
}
