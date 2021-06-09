using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class GeneralInfo : UserControl
    {
        public GeneralInfo(GeneralInfoViewModel generalInfoViewModel)
        {
            InitializeComponent();
            this.DataContext = generalInfoViewModel;
        }

    }
}
