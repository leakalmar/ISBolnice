using Controllers;
using DTOs;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class DoctorMainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        private PatientChartViewModel previousChart { get; set; } = new PatientChartViewModel();

        public DoctorMainWindow(DoctorDTO doctor)
        {
            InitializeComponent();
            CommandBinding removeBinding = null;
            foreach (CommandBinding cb in this.Home.CommandBindings)
            {
                if (cb.Command == NavigationCommands.Refresh)
                {
                    removeBinding = cb;
                    break;
                }
                if (removeBinding != null)
                {
                    this.Home.CommandBindings.Remove(removeBinding);
                }

            }
            DoctorNavigationController.Instance.NavigationService = this.Home.NavigationService;
            DoctorMainWindowModel.Instance.SetDoctor(doctor);
            this.DataContext = DoctorMainWindowModel.Instance;
        }
        //private void Execute_MinimizeCommand(object obj)
        //{
        //    WindowControls.DoMinimize(this);
        //}

        //private void Execute_MaximizeCommand(object obj)
        //{
        //    WindowControls.DoMaximize(this, this.full);
        //}
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                DoctorMainWindowModel.Instance.DemoRunning = false;
                this.Home.BorderBrush = Brushes.Transparent;
                this.demoLabel.Visibility = Visibility.Collapsed;
                dispatcherTimer.Stop();
                PatientChartViewModel.Instance.Started = false;
                PatientChartViewModel.Instance.SelectedAppointment = null;
                PatientChartViewModel.Instance.SearchMedicineViewModel = null;
                PatientChartViewModel.Instance.ReportViewModel = null;
                if (previousChart.SelectedAppointment != null)
                {
                    PatientChartViewModel.Instance.SelectedAppointment = previousChart.SelectedAppointment;
                    PatientChartViewModel.Instance.Started = previousChart.Started;
                }
                
                DoctorNavigationController.Instance.NavigateToHomeCommand();
                PatientDTO patient = new PatientDTO();
                patient.Id = -1;
                SecretaryManagementController.Instance.DeletePatient(patient);


                new ExitMess("Demo uspešno prekinut.", "info").ShowDialog();

                return;
            }

            if (e.Key == Key.F11)
            {
                bool dialog = (bool)new ExitMess("Da li želite da pokrenete demo zakazivanja pregleda?", "yesNo").ShowDialog();
                if (dialog)
                {
                    if(PatientChartViewModel.Instance.SelectedAppointment != null)
                    {
                        previousChart.SelectedAppointment = PatientChartViewModel.Instance.SelectedAppointment;
                        previousChart.Started = PatientChartViewModel.Instance.Started;
                        previousChart.SearchMedicineViewModel = PatientChartViewModel.Instance.SearchMedicineViewModel;
                        previousChart.ReportViewModel = PatientChartViewModel.Instance.ReportViewModel;
                    }
                    DoctorMainWindowModel.Instance.DemoRunning = true;
                    this.demoLabel.Visibility = Visibility.Visible;
                    this.Home.BorderBrush = new SolidColorBrush(Color.FromArgb(0x7F, 0x5B, 0x31, 0x7E));
                    Demo();
                }
            }
        }

        private void Demo()
        {
            List<AppointmentRowDTO> list = new List<AppointmentRowDTO>();
            List<String> alergies = new List<string>();
            alergies.Add("Polen");

            PatientDTO patient1 = new PatientDTO(-1, "Petar", "Perić", "Muško", new DateTime(1990, 05, 12), "069/457-66-77", "petar@gmail.com", EducationCategory.College, "U braku", "Addiko banka", "petar", "Zelengorska 31", alergies, false, false);
            PatientDTO patient2 = new PatientDTO(-1, "Marina", "Tanasić", "Žensko", new DateTime(1995, 11, 21), "063/466-56-18", "marina@gmail.com", EducationCategory.College, "U braku", "Adeko", "marina", "Braće Radića 5", alergies, false, false);

            SecretaryManagementController.Instance.AddPatient(patient1);

            AppointmentRowDTO appointmentRowDTO1 = new AppointmentRowDTO(new DoctorAppointmentDTO(true, "Pregled nakon operacije.", DateTime.Now.AddHours(1), DateTime.Now.AddHours(1).AddMinutes(30), AppointmentType.CheckUp, DoctorMainWindowModel.Instance.Doctor.PrimaryRoom, -1, false, patient1, DoctorMainWindowModel.Instance.Doctor, false));
            AppointmentRowDTO appointmentRowDTO2 = new AppointmentRowDTO(new DoctorAppointmentDTO(true, "Bolovi u stomaku", DateTime.Now.AddMinutes(30), DateTime.Now.AddHours(1), AppointmentType.CheckUp, DoctorMainWindowModel.Instance.Doctor.PrimaryRoom, -1, false, patient2, DoctorMainWindowModel.Instance.Doctor, false));

            list.Add(appointmentRowDTO1);
            list.Add(appointmentRowDTO2);

            HomePage homePage = new HomePage();
            ICollectionView collectionView = new CollectionViewSource { Source = list }.View;
            homePage._ViewModel.AppointmentsView = collectionView;

            AppDetail appDetail = new AppDetail();
            appDetail._ViewModel.AppointmentsView = collectionView;

            list[0].Appointment.IsFinished = false;
            list[0].IsStarted = true;

            ScheduledApp scheduledApp = null;
            ScheduleAppointmentViewModel scheduleAppointmentViewModel = null;

            NewApp newApp = null;

            List<AppointmentRowDTO> newAppointments = new List<AppointmentRowDTO>();

            AppointmentRowDTO appointmentRowDTO3 = new AppointmentRowDTO(new DoctorAppointmentDTO(false, "", DateTime.Now.Date.AddHours(19).AddMinutes(30), DateTime.Now.Date.AddHours(20), AppointmentType.CheckUp, DoctorMainWindowModel.Instance.Doctor.PrimaryRoom, -1, false, patient1, DoctorMainWindowModel.Instance.Doctor, false));
            AppointmentRowDTO appointmentRowDTO4 = new AppointmentRowDTO(new DoctorAppointmentDTO(false, "", DateTime.Now.Date.AddHours(19), DateTime.Now.Date.AddHours(19).AddMinutes(30), AppointmentType.CheckUp, DoctorMainWindowModel.Instance.Doctor.PrimaryRoom, -1, false, patient1, DoctorMainWindowModel.Instance.Doctor, false));

            appointmentRowDTO3.IsEnabled = true;
            appointmentRowDTO4.IsEnabled = true;

            newAppointments.Add(appointmentRowDTO3);
            newAppointments.Add(appointmentRowDTO4);
            ICollectionView newCollectionView = new CollectionViewSource { Source = newAppointments }.View;

            IssueInstruction issueInstruction = new IssueInstruction();
            issueInstruction._ViewModel.SelectedAppointment = appointmentRowDTO3;

            int demoNumber = 0;
            var enumerator = collectionView.GetEnumerator();
            enumerator.MoveNext();
            var newCollectionEnumerator = newCollectionView.GetEnumerator();
            newCollectionEnumerator.MoveNext();
            dispatcherTimer.Start();
            dispatcherTimer.Tick += (sender, args) =>
            {

                switch (demoNumber)
                {
                    case 0:
                        Keyboard.DefaultRestoreFocusMode = RestoreFocusMode.Auto;
                        Keyboard.Focus(homePage.docotrAppointments);
                        homePage.docotrAppointments.Focus();
                        DoctorNavigationController.Instance.NavigationService.Navigate(homePage);
                        homePage._ViewModel.SelectedAppointment = null;
                        break;
                    case 1:
                        homePage._ViewModel.SelectedAppointment = (AppointmentRowDTO)enumerator.Current;
                        enumerator.MoveNext();
                        break;
                    case 2:
                        homePage._ViewModel.SelectedAppointment = (AppointmentRowDTO)enumerator.Current;
                        break;
                    case 3:
                        enumerator.Reset();
                        enumerator.MoveNext();
                        homePage._ViewModel.SelectedAppointment = (AppointmentRowDTO)enumerator.Current;
                        break;
                    case 4:
                        DataGridRow dataGridRow = homePage.docotrAppointments.ItemContainerGenerator.ContainerFromIndex(homePage.docotrAppointments.SelectedIndex) as DataGridRow;
                        if (dataGridRow != null)
                        {
                            dataGridRow.Background = Brushes.White;
                            dataGridRow.Foreground = Brushes.Black;
                        }
                        homePage._ViewModel.SelectedAppointment = null;
                        dataGridRow.UpdateLayout();
                        break;
                    case 5:
                        DataGridRow dataGridRow3 = homePage.docotrAppointments.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
                        if (dataGridRow3 != null)
                        {
                            dataGridRow3.Background = Brushes.Transparent;
                            dataGridRow3.Foreground = Brushes.White;
                        }
                        appDetail._ViewModel.SelectedAppointment = list[0];
                        DoctorNavigationController.Instance.NavigationService.Navigate(appDetail);
                        break;
                    case 6:
                        appDetail.start.Focus();
                        break;
                    case 7:
                        DoctorNavigationController.Instance.NavigateToChartCommand();
                        PatientChartViewModel.Instance.ReportViewModel = new ReportViewModel();
                        PatientChartViewModel.Instance.SelectedAppointment = list[0];
                        PatientChartViewModel.Instance.Started = true;
                        break;
                    case 8:
                        PatientChartViewModel.Instance.ChangeCommand.Execute("3");
                        scheduleAppointmentViewModel = new ScheduleAppointmentViewModel();
                        scheduleAppointmentViewModel.Started = true;
                        scheduledApp = new ScheduledApp(scheduleAppointmentViewModel);
                        DoctorInsideNavigationController.Instance.NavigationService.Navigate(scheduledApp);
                        break;
                    case 9:
                        scheduledApp.schedule.Focus();
                        break;
                    case 10:
                        newApp = new NewApp();
                        newApp._ViewModel.Appointments = null;
                        newApp._ViewModel.EmergencyAppointments = null;
                        newApp.fromDate.SelectedDate = new DateTime(2021, 06, 07);
                        newApp.toDate.SelectedDate = new DateTime(2021, 06, 08);
                        DoctorNavigationController.Instance.NavigationService.Navigate(newApp);
                        break;
                    case 11:
                        newApp.fromDate.IsDropDownOpen = true;
                        break;
                    case 12:
                        newApp.fromDate.SelectedDate = new DateTime(2021, 05, 07);
                        newApp.fromDate.IsDropDownOpen = false;
                        newApp.toDate.SelectedDate = new DateTime(2021, 05, 08);
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 13:
                        newApp.toDate.Focus();
                        break;
                    case 14:
                        newApp.specialty.Focus();
                        newApp.specialty.SelectedIndex = 0;
                        newApp._ViewModel.Appointments = null;
                        break;
                    case 15:
                        foreach (Specialty s in newApp.specialty.Items)
                        {
                            if (s.Name.Equals(DoctorMainWindowModel.Instance.Doctor.Specialty))
                            {
                                newApp.specialty.SelectedItem = s;
                            }
                        }
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 16:
                        newApp.doctors.Focus();
                        newApp.doctors.SelectedIndex = 1;
                        newApp._ViewModel.Appointments = null;
                        break;
                    case 17:
                        foreach (Doctor d in newApp.doctors.Items)
                        {
                            if (d.Id.Equals(DoctorMainWindowModel.Instance.Doctor.Id))
                            {
                                newApp.doctors.SelectedItem = d;
                            }
                        }
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 18:
                        newApp.rooms.Focus();
                        newApp.rooms.SelectedIndex = 1;
                        newApp._ViewModel.Appointments = null;
                        break;
                    case 19:
                        foreach (Room r in newApp.rooms.Items)
                        {
                            if (r.Id.Equals(DoctorMainWindowModel.Instance.Doctor.PrimaryRoom))
                            {
                                newApp.rooms.SelectedItem = r;
                            }
                        }
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 20:
                        newApp.types.Focus();
                        newApp.types.SelectedIndex = 1;
                        newApp._ViewModel.Appointments = null;
                        break;
                    case 21:
                        newApp.types.SelectedIndex = 0;
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 22:
                        newApp.emergency.Focus();
                        newApp._ViewModel.Emergency = true;
                        newApp._ViewModel.Appointments = null;
                        break;
                    case 23:
                        newApp.emergency.Focus();
                        newApp._ViewModel.Emergency = false;
                        newApp._ViewModel.Appointments = newCollectionView;
                        break;
                    case 24:
                        newApp.classicAppointments.Focus();
                        newApp._ViewModel.SelectedAppointment = (AppointmentRowDTO)newCollectionEnumerator.Current;
                        break;
                    case 25:
                        DoctorNavigationController.Instance.NavigationService.Navigate(issueInstruction);
                        break;
                    case 26:
                        issueInstruction.cause.Text = "Drugi pregled nakon operacije.";
                        break;
                    case 27:
                        issueInstruction.issueButton.Focus();
                        break;
                    case 28:
                        scheduleAppointmentViewModel.ScheduledAppointments.Add(appointmentRowDTO3.Appointment);
                        DoctorNavigationController.Instance.NavigateToChartCommand();
                        DoctorInsideNavigationController.Instance.NavigationService.Navigate(new ScheduledApp(scheduleAppointmentViewModel));
                        break;
                    case 29:
                        DoctorMainWindowModel.Instance.DemoRunning = false;
                        this.Home.BorderBrush = Brushes.Transparent;
                        this.demoLabel.Visibility = Visibility.Collapsed;
                        this.Home.BorderThickness = new Thickness(0);
                        PatientChartViewModel.Instance.Started = false;
                        PatientChartViewModel.Instance.SelectedAppointment = null;
                        if (previousChart.SelectedAppointment != null)
                        {
                            PatientChartViewModel.Instance.SelectedAppointment = previousChart.SelectedAppointment;
                            PatientChartViewModel.Instance.Started = previousChart.Started;
                            PatientChartViewModel.Instance.SearchMedicineViewModel = previousChart.SearchMedicineViewModel;
                            PatientChartViewModel.Instance.ReportViewModel = previousChart.ReportViewModel;
                        }
                        DoctorNavigationController.Instance.NavigateToHomeCommand();
                        SecretaryManagementController.Instance.DeletePatient(patient1);
                        dispatcherTimer.Stop();
                        this.Focus();
                        new ExitMess("Demo zakazivanja pregleda je završen.", "info").ShowDialog();
                        demoNumber = 0;
                        return;
                };
                demoNumber++;
            };

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PatientChartViewModel.Instance.Started)
            {
                new ExitMess("Termin nije završen! Molimo vas završite termin pre odjave.", "info").ShowDialog();
                return;
            }
            else
            {
                bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?", "yesNo").ShowDialog();
                if (dialog)
                {
                    MainWindow login = new MainWindow();
                    login.Show();
                    this.Hide();
                }
            }
        }
    }
}
