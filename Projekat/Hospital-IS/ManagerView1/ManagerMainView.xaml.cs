using System.Threading;
using System.Windows;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ManagerMainView.xaml
    /// </summary>
    public partial class ManagerMainView : Window
    {
        public ManagerMainView(MainWindow mainWindow)
        {
            mainWindow.Close();
            InitializeComponent();          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
