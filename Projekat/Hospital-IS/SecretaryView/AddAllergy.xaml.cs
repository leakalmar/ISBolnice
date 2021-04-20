using System.Windows;

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
            if (!string.IsNullOrEmpty(txtAllergies.Text))
            {
                string Allergies = txtAllergies.Text;
                string[] Allergy = Allergies.Split(',');
                for (int i = 0; i < Allergy.Length; i++)
                {
                    upv.Allergies.Add(Allergy[i].Trim());
                    upv.Patient.Alergies.Add(Allergy[i].Trim());
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
