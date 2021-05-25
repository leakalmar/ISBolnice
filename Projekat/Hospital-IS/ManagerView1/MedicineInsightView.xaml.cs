using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for MedicineInsightView.xaml
    /// </summary>
    public partial class MedicineInsightView : Page
    {
        public MedicineInsightView()
        {
            InitializeComponent();
            this.DataContext = MedicineViewModel.Instance;
            Back.DataContext = MedicineViewModel.Instance;

        }

        
    }
}
