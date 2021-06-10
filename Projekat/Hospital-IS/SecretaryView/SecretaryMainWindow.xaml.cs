using Controllers;
using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView.Localization;
using Hospital_IS.SecretaryView.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        public IDoctorAppointmentTarget target = new DoctorAppointmentAdapter();
        UCPatientsView ucp = new UCPatientsView();
        UCNotificationsView ucn = new UCNotificationsView();
        UCDoctorsView ucd = new UCDoctorsView();
        UCRoomsView ucr = new UCRoomsView();
        UCSettings ucs = new UCSettings();
        public UCAppointmentsView uca;

        private static SecretaryMainWindow instance = null;

        public static SecretaryMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryMainWindow();
                }
                return instance;
            }
        }

        private SecretaryMainWindowViewModel viewModel;

        public SecretaryMainWindowViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }

        public SecretaryMainWindow()
        {
            InitializeComponent();
            this.uca = new UCAppointmentsView(target);

            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

            miScheduling.IsEnabled = false;
            HomePage.Content = ucp;

            SetSearchField("sr");
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
        }

        

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            SecretaryManagementController.Instance.ReloadPatients();
            ucp.RefreshGrid();

            miShow.IsEnabled = true;
            miUpdate.IsEnabled = true;
            miRegistration.IsEnabled = true;
            miScheduling.IsEnabled = false;
            HomePage.Content = ucp;
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            miShow.IsEnabled = false;
            miUpdate.IsEnabled = false;
            miRegistration.IsEnabled = false;
            miScheduling.IsEnabled = false;
            HomePage.Content = ucn;
        }

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentController.Instance.ReloadDoctorAppointments();
            uca.RefreshGrid();

            miShow.IsEnabled = true;
            miUpdate.IsEnabled = true;
            miRegistration.IsEnabled = false;
            miScheduling.IsEnabled = true;
            HomePage.Content = uca;
        }

        private void btnDoctors_Click(object sender, RoutedEventArgs e)
        {
            SecretaryManagementController.Instance.ReloadDoctors();
            ucd.RefreshGrid();

            miShow.IsEnabled = true;
            miUpdate.IsEnabled = true;
            miRegistration.IsEnabled = false;
            miScheduling.IsEnabled = false;
            HomePage.Content = ucd;
        }

        private void btnRooms_Click(object sender, RoutedEventArgs e)
        {
            miShow.IsEnabled = true;
            miUpdate.IsEnabled = false;
            miRegistration.IsEnabled = false;
            miScheduling.IsEnabled = false;
            HomePage.Content = ucr;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Content = ucs;
        }

        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            if (HomePage.Content == ucp)
            {
                if (ucp.dataGridPatients.SelectedItem != null)
                {
                    PatientView pv = new PatientView((PatientDTO)ucp.dataGridPatients.SelectedItem);
                    pv.Show();
                }
                else
                    MessageBox.Show("Odaberite pacijenta!");
            }
            else if (HomePage.Content == uca)
            {
                if (uca.dataGridAppointments.SelectedItem != null)
                {
                    AppointmentView av = new AppointmentView((DoctorAppointmentDTO)uca.dataGridAppointments.SelectedItem);
                    av.Show();
                }
                else
                    MessageBox.Show("Odaberite termin!");
            }
            else if (HomePage.Content == ucd)
            {
                if (ucd.dataGridDoctors.SelectedItem != null)
                {
                    DoctorView dv = new DoctorView((DoctorDTO)ucd.dataGridDoctors.SelectedItem);
                    dv.Show();
                }
                else
                    MessageBox.Show("Odaberite doktora!");
            }
            else if (HomePage.Content == ucr)
            {
                if (ucr.dataGridRooms.SelectedItem != null)
                {
                    RoomView rv = new RoomView((RoomDTO)ucr.dataGridRooms.SelectedItem);
                    rv.Show();
                }
                else
                    MessageBox.Show("Odaberite prostoriju!");
            }
        }

        private void miUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (HomePage.Content == ucp)
            {
                if (ucp.dataGridPatients.SelectedItem != null)
                {
                    UpdatePatientView upv = new UpdatePatientView((PatientDTO)ucp.dataGridPatients.SelectedItem, ucp);
                    upv.Show();
                }
                else
                    MessageBox.Show("Odaberite pacijenta!");
            }
            else if (HomePage.Content == uca)
            {
                if (uca.dataGridAppointments.SelectedItem != null)
                {
                    UpdateAppointment ua = new UpdateAppointment((DoctorAppointmentDTO)uca.dataGridAppointments.SelectedItem, uca);
                    ua.Show();
                }
                else
                    MessageBox.Show("Odaberite termin!");
            }
            else if (HomePage.Content == ucd)
            {
                if (ucd.dataGridDoctors.SelectedItem != null)
                {
                    UpdateDoctorView udv = new UpdateDoctorView((DoctorDTO)ucd.dataGridDoctors.SelectedItem, ucd);
                    udv.Show();
                }
                else
                    MessageBox.Show("Odaberite doktora!");
            }
        }

        private void miRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (HomePage.Content == ucp)
            {
                PatientRegistration pr = new PatientRegistration(ucp, null);
                pr.Show();
            }
            else if (HomePage.Content == ucd)
            {
                DoctorRegistration dr = new DoctorRegistration(ucd);
                dr.Show();
            }

        }

        private void miScheduling_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(uca);
            sa.Show();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;

            MenuItem mi = (MenuItem)sender;
            if (mi.Name.ToString().Equals("miLight"))
            {
                app.ChangeTheme(new Uri("SecretaryView/Themes/LightTheme.xaml", UriKind.Relative));
                miLight.IsChecked = true;
                miDark.IsChecked = false;
                ucs.cbTheme.SelectedIndex = 0;
            }
            else 
            {
                app.ChangeTheme(new Uri("SecretaryView/Themes/DarkTheme.xaml", UriKind.Relative));
                miLight.IsChecked = false;
                miDark.IsChecked = true;
                ucs.cbTheme.SelectedIndex = 1;
            }
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            if (mi.Name.ToString().Equals("miSerbian"))
            {
                LocalizedStrings.Instance.SetCulture("sr-LATN-CS");
                miSerbian.IsChecked = true;
                miEnglish.IsChecked = false;
                ucs.cbLanguage.SelectedIndex = 0;
                SetSearchField("sr");
            }
            else
            {
                LocalizedStrings.Instance.SetCulture("en-US");
                miSerbian.IsChecked = false;
                miEnglish.IsChecked = true;
                ucs.cbLanguage.SelectedIndex = 1;
                SetSearchField("en");
            }
        }

        public void SetSearchField(string lang)
        {
            if (lang.Equals("sr"))
            {
                ucp.txtSearch.Text = "Pretraži...";
                uca.txtSearch.Text = "Pretraži...";
                ucd.txtSearch.Text = "Pretraži...";
                ucr.txtSearch.Text = "Pretraži...";
            }
            else 
            {
                ucp.txtSearch.Text = "Search...";
                uca.txtSearch.Text = "Search...";
                ucd.txtSearch.Text = "Search...";
                ucr.txtSearch.Text = "Search...";
            }
        }
    }
}