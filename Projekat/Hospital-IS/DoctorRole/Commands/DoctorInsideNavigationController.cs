using DTOs;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorRole.Commands
{
    public class DoctorInsideNavigationController : InsideNavigationController
    {
        private static DoctorInsideNavigationController instance;
        private NavigationService navigaitonService;

        public static DoctorInsideNavigationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorInsideNavigationController();
                }
                return instance;
            }
        }

        public NavigationService NavigationService
        {
            set { 
                navigaitonService = value; 
            }
            get { return navigaitonService; }
        }

        public DoctorInsideNavigationController() { }

        public override void NavigateToChangeAppointment(AppointmentsViewModel appointmentsViewModel)
        {
            ChangeApp changeApp = new ChangeApp();
            changeApp._ViewModel.OldAppointment = appointmentsViewModel.SelectedAppointment;
            appointmentsViewModel.ShowChangePanel = true;
            this.NavigationService.Navigate(changeApp);
        }

        public override void NavigateToReportCommand(ReportViewModel reportViewModel)
        {
            this.NavigationService.Navigate(new ReportView(reportViewModel));
        }

        public override void NavigateToGeneralCommand(GeneralInfoViewModel generalInfoViewModel)
        {
            this.NavigationService.Navigate(new GeneralInfo(generalInfoViewModel));
        }

        public override void NavigateToHistoryCommand(HistoryViewModel historyViewModel)
        {
            this.NavigationService.Navigate(new History(historyViewModel));
        }

        public override void NavigateToSheduledAppointmentsCommand(ScheduleAppointmentViewModel scheduleAppointmentViewModel)
        {
            this.NavigationService.Navigate(new ScheduledApp(scheduleAppointmentViewModel));
        }
        public override void NavigateToTherapiesCommand(TherapyViewModel therapyViewModel)
        {
            this.NavigationService.Navigate(new Therapies(therapyViewModel));
        }
        public override void NavigateToTestsCommand(TestsViewModel testsViewModel)
        {
            this.NavigationService.Navigate(new Tests(testsViewModel));
        }
        public override void NavigateToHospitalizationsCommand(HospitalizationsViewModel hospitalizationsViewModel)
        {
            this.NavigationService.Navigate(new Hospitalizations(hospitalizationsViewModel));
        }

        public override void NavigateToSearchMedicineCommand(SearchMedicineViewModel searchMedicineViewModel)
        {
            this.NavigationService.Navigate(new SearchMedicine(searchMedicineViewModel));
        }

        public override void NavigateToGeneralInfoChangeCommand()
        {
            this.NavigationService.Navigate(new GeneralInfoChange());
        }

        public override void NavigateToOldReportCommand(ReportDTO report)
        {
            OldReportViewModel oldReportViewModel = new OldReportViewModel();
            oldReportViewModel.Report = report;
            this.NavigationService.Navigate(new OldReport(oldReportViewModel));
        }
        public override void NavigateToTherapyNewCommand()
        {
            this.NavigationService.Navigate(new TherapyNew());
        }
    }
}
