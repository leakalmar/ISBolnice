using System.Windows;
using System.Windows.Input;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (email.Text == "upravnik@gmail.com" && password.Password.ToString() == "upravnik")
            {
            }
            else if (email.Text == "doktor@gmail.com" && password.Password.ToString() == "doktor")
            {
            }
            else if (email.Text == "sekretar@gmail.com" && password.Password.ToString() == "sekretar")
            {
                SecretaryMainWindow.Instance.Show();
                this.Close();
            }
            else if (email.Text == "pacijent@gmail.com" && password.Password.ToString() == "pacijent")
            {
            }
        }
    }
}
