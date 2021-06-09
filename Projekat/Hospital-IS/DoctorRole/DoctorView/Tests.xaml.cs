using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Tests : UserControl
    {
        public Tests(TestsViewModel testsViewModel)
        {
            InitializeComponent();
            this.DataContext = testsViewModel;

        }
    }
}
