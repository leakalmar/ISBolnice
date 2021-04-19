using Model;
using System.Windows;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Window
    {
        public Notification Notification { get; set; }
        public NotificationView(Notification notification)
        {
            InitializeComponent();
            Notification = notification;
            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
