using System.Windows;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UpdateAllergy.xaml
    /// </summary>
    public partial class UpdateAllergy : Window
    {
        private UpdatePatientView upv;
        public UpdateAllergy(UpdatePatientView upv)
        {
            InitializeComponent();
            this.upv = upv;
            txtOldAllergy.Text = (string)upv.dataGridAllergies.SelectedItem;
        }

        private void ChangeAllergy(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewAllergy.Text))
            {
                string allergy = (string)upv.dataGridAllergies.SelectedItem;
                upv.Allergies[upv.dataGridAllergies.SelectedIndex] = txtNewAllergy.Text;
                for (int i = 0; i < upv.Patient.Alergies.Count; i++)
                {
                    if (upv.Patient.Alergies[i].Equals(allergy))
                    {
                        upv.Patient.Alergies.Remove(allergy);
                        upv.Patient.Alergies.Insert(i, txtNewAllergy.Text);
                    }
                }
            }
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
