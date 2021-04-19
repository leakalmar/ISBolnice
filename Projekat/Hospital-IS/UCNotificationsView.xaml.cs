using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UCNotificationsView.xaml
    /// </summary>
    public partial class UCNotificationsView : UserControl
    {
        List<Notification> notifications = new List<Notification>();

        public UCNotificationsView()
        {
            InitializeComponent();
            Notification n1 = new Notification("Obavestenje 1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce dictum turpis sem, eu hendrerit arcu pharetra vitae. " +
                "Quisque in elit ullamcorper, tempus sapien vitae, lobortis elit. Fusce rutrum, ipsum eu sodales sagittis, neque est tincidunt nunc, et facilisis massa arcu non " +
                "turpis. Suspendisse potenti. Cras mollis lorem eget pharetra rutrum. Sed vel eros rhoncus, commodo enim dignissim, vulputate leo. Phasellus ut sem ut metus " +
                "vulputate congue. Curabitur nibh eros, tincidunt nec ultricies nec, finibus tincidunt massa. Nullam vitae porttitor massa, sed dignissim erat. Nulla eu varius " +
                "nisl. Vivamus sollicitudin dui in diam lobortis tincidunt. Morbi egestas sapien ultricies venenatis maximus. Mauris dignissim eget ex ac commodo. Donec massa sem, " +
                "pellentesque et augue ac, egestas sodales urna.", DateTime.Now, DateTime.Now);
            n1.Id = 1;
            Notification n2 = new Notification("Obavestenje 2", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 2;
            Notification n3 = new Notification("Obavestenje 3", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 3;
            Notification n4 = new Notification("Obavestenje 4", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 4;
            Notification n5 = new Notification("Obavestenje 5", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 5;
            Notification n6 = new Notification("Obavestenje 6", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 6;
            Notification n7 = new Notification("Obavestenje 7", "Tekst", DateTime.Now, DateTime.Now);
            n1.Id = 7;
            notifications.Add(n1);
            notifications.Add(n2);
            notifications.Add(n3);
            notifications.Add(n4);
            notifications.Add(n5);
            notifications.Add(n6);
            notifications.Add(n7);
         
            if (notifications.Count > 0)
                ListViewNotifications.ItemsSource = notifications;

        }

        private void ShowNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification n = findNotification(Int32.Parse(button.Tag.ToString()));
            NotificationView nv = new NotificationView(n);
            nv.Show();

        }

        private Notification findNotification(int id)
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                if (id == notifications[i].Id)
                    return notifications[i];
            }
            return null;
        }
    }
}
