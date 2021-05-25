using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for EquipmentTransferDynamicVIew.xaml
    /// </summary>
    public partial class EquipmentTransferDynamicVIew : Page
    {
        public EquipmentTransferDynamicVIew()
        {
            InitializeComponent();
            this.DataContext = EquipmentTransferViewModel.Instance;
        }

       
    }
}
