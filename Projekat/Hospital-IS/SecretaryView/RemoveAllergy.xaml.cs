using System.Windows;

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
            tbDeleteAllergen.Text = "Da li ste sigurni da želite da uklonite alregen \"" + allergy + "\"?";
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
