using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for AddAllergy.xaml
    /// </summary>
    public partial class AddAllergy : Window
    {
        UpdatePatientView upv;
        public AddAllergy(UpdatePatientView upv)
        {
            InitializeComponent();
            this.upv = upv;
        }

        private void AddAllergies(object sender, RoutedEventArgs e)
        {
            string Allergies = txtAllergies.Text;
            string[] Allergy = Allergies.Split(',');
            for (int i = 0; i < Allergy.Length; i++)
            {
                upv.Allergies.Add(Allergy[i].Trim());
                upv.Patient.Alergies.Add(Allergy[i].Trim());
            }
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
