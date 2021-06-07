using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class WindowProductionFactory : MyWindowFactory
    {
        public void CreateAppointmentEvaluation(int appointmentId)
        {
            PatientAppointmentEvaluationWindow appointmentEvaluation = new PatientAppointmentEvaluationWindow(appointmentId);
            appointmentEvaluation.AppointmentEvaluation.OnRequestClose += (s, e) => appointmentEvaluation.Close();
            appointmentEvaluation.Show();
        }

        public void CreateHospitalEvaluation()
        {
            PatientHospitalEvaluationWindow hospitalEvaluation = new PatientHospitalEvaluationWindow();
            hospitalEvaluation.HospitalEvaluationViewModel.OnRequestClose += (s, e) => hospitalEvaluation.Close();
            hospitalEvaluation.Show();
        }

        public void CreateNote(int appointmentId)
        {
            PatientNoteView appointmentNote = new PatientNoteView(appointmentId);
            appointmentNote.AppointmentNoteViewModel.OnRequestClose += (s, e) => appointmentNote.Close();
            appointmentNote.Show();
        }

        public void CreateYesNo(string message)
        {
            YesNoDialogMessage yesNoDialog = new YesNoDialogMessage(message);
            yesNoDialog.DialogMessageViewModel.OnRequestClose += (s, e) => yesNoDialog.Close();
            yesNoDialog.Show();
        }

        public void CreateFeedback()
        {
            FeedbackView feedbackView = new FeedbackView();
            feedbackView.FeedbackViewModel.OnRequestClose += (s, e) => feedbackView.Close();
            feedbackView.Show();
        }
    }
}
