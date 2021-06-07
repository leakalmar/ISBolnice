using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public interface MyWindowFactory
    {
        void CreateHospitalEvaluation();
        void CreateAppointmentEvaluation(int appointmentId);
        void CreateNote(int appointmentId);
        void CreateYesNo(string message);
        void CreateFeedback();
    }
}
