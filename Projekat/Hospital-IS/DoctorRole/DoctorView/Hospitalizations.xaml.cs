using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Hospitalizations : UserControl
    {
        public Hospitalizations(HospitalizationsViewModel hospitalizationsViewModel)
        {
            InitializeComponent();
            this.DataContext = hospitalizationsViewModel;

        }
    }
}
