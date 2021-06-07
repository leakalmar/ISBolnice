using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for UpdateEquipmentView.xaml
    /// </summary>
    public partial class UpdateEquipmentView : Page
    {
        public UpdateEquipmentView()
        {
            InitializeComponent();
            this.DataContext = UpdateEquipmentViewModel.Instance;
        
            
        }
    }
}
