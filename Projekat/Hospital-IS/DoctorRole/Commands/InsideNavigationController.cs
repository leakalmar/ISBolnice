using DTOs;
using Hospital_IS.DoctorViewModel;

namespace Hospital_IS.DoctorRole.Commands
{
    public abstract class InsideNavigationController
    {
        public abstract void NavigateToChangeAppointment(AppointmentsViewModel appointmentsViewModel);
        public abstract void NavigateToReportCommand(ReportViewModel reportViewModel);
        public abstract void NavigateToGeneralCommand(GeneralInfoViewModel generalInfoViewModel); 
        public abstract void NavigateToHistoryCommand(HistoryViewModel historyViewModel); 
        public abstract void NavigateToSheduledAppointmentsCommand(ScheduleAppointmentViewModel scheduleAppointmentViewModel); 
        public abstract void NavigateToTherapiesCommand(TherapyViewModel therapyViewModel);
        public abstract void NavigateToTestsCommand(TestsViewModel testsViewModel);
        public abstract void NavigateToHospitalizationsCommand(HospitalizationsViewModel hospitalizationsViewModel);
        public abstract void NavigateToSearchMedicineCommand(SearchMedicineViewModel searchMedicineViewModel);
        public abstract void NavigateToGeneralInfoChangeCommand();
        public abstract void NavigateToOldReportCommand(ReportDTO report);
        public abstract void NavigateToTherapyNewCommand();

    }
}
