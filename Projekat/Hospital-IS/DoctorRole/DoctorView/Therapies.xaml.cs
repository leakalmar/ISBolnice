using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Therapies : UserControl
    {
        public Therapies(TherapyViewModel therapyViewModel)
        {
            InitializeComponent();
            this.DataContext = therapyViewModel;

        }
    }
}
