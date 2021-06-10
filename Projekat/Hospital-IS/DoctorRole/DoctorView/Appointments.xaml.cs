using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Appointments : UserControl
    {
        public Appointments()
        {
            InitializeComponent();
            DoctorInsideNavigationController.Instance.NavigationService = this.frame.NavigationService;
            this.DataContext = AppointmentsViewModel.Instance;
        }

        private void DataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(grid.SelectedItem != null)
            {
                grid.ScrollIntoView(grid.SelectedItem);
            }
        }
    }
}
