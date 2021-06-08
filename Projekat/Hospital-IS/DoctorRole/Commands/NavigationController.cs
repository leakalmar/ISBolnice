using Hospital_IS.DoctorViewModel;
using Model;

namespace Hospital_IS.DoctorRole.Commands
{
    public abstract class NavigationController
    {
        public abstract void NavigateToHomeCommand();

        public abstract void NavigateToAppointmentsCommand();
        public abstract void NavigateToPatientsCommand();
        public abstract void NavigateToChartCommand();
        public abstract void NavigateToMedicinesCommand();
        public abstract void NavigateToViewMedicineCommand(MedicineNotification medicineNotification);
        public abstract void NavigateToUpdateMedicineCommand(Medicine medicine);
        public abstract void NavigateToRoomsCommand();
        public abstract void NavigateToEquipmentCommand();
        public abstract void NavigateToPrescriptionsCommand();
        public abstract void NavigateToApprovemedicineCommand();
        public abstract void NavigateToNotificationsCommand();
        public abstract void NavigateBackCommand();
        public abstract void NavigateBackWithoutCheckCommand();
        public abstract void NavigateToSettingsCommand();
        public abstract void NavigateToAppDetailCommand(HomePageViewModel homePageViewModel);
        public abstract void NavigateToInstructionCommand(NewAppViewModel newAppViewModel);
        public abstract void NavigateToSearchMedicineCommand(ReportViewModel reportViewModel);
        public abstract void NavigateToNewAppointment();
    }
}
