using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class History : UserControl
    {
        public History(HistoryViewModel historyViewModel)
        {
            InitializeComponent();
            this.DataContext = historyViewModel;
        }

    }
}
