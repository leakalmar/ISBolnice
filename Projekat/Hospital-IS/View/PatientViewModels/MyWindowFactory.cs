using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public interface MyWindowFactory
    {
        void CreateHospitalEvaluationWindow();
        void CreateAppointmentEvaluationWindow(int appointmentId);
        void CreateNoteWindow(int appointmentId);
        void CreateYesNoDialog(string message);
    }
}
