using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ManagerProfileOptionsView.xaml
    /// </summary>
    public partial class ManagerProfileOptionsView : Page
    {
        public ManagerProfileOptionsView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ManagerProfileOptionsVIewModel.Instance;
            ManagerProfileOptionsVIewModel.Instance.NavService = this.NavigationService;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            window.Close();
        }
    }
}
