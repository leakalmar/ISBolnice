using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for EquipmentStaticTransferView.xaml
    /// </summary>
    public partial class EquipmentStaticTransferView : Page
    {
        public EquipmentStaticTransferView()
        {
            InitializeComponent();
            this.DataContext = EquipmentTransferStaticViewModel.Instance;
        }
    }
}
