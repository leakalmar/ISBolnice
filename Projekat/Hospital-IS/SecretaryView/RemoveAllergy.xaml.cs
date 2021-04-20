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
    /// Interaction logic for RemoveAllergy.xaml
    /// </summary>
    public partial class RemoveAllergy : Window
    {
        private UpdatePatientView upv;
        string allergy;
        public RemoveAllergy(UpdatePatientView upv)
        {
            InitializeComponent();
            this.upv = upv;
            allergy = (string)upv.dataGridAllergies.SelectedItem;
            txtConfirmation.Text = "Da li ste sigurni da želite da uklonite alregen \"" + allergy + "\"?";
        }

        private void DeleteAllergy(object sender, RoutedEventArgs e)
        {
            upv.Allergies.Remove(allergy);
            upv.Patient.Alergies.Remove(allergy);
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
