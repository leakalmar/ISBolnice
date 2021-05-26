using Hospital_IS.ManagerViewModel;
using System.Windows;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ChooseReciepientForNotification.xaml
    /// </summary>
    public partial class ChooseReciepientForNotification : Window
    {
        public ChooseReciepientForNotification()
        {
            InitializeComponent();
            this.DataContext = RecipientViewModel.Instance;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
