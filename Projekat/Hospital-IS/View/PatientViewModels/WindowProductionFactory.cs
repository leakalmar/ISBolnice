using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class WindowProductionFactory : MyWindowFactory
    {
        public void CreateAppointmentEvaluationWindow(int appointmentId)
        {
            PatientAppointmentEvaluationWindow appointmentEvaluation = new PatientAppointmentEvaluationWindow(appointmentId);
            appointmentEvaluation.AppointmentEvaluation.OnRequestClose += (s, e) => appointmentEvaluation.Close();
            appointmentEvaluation.Show();
        }

        public void CreateHospitalEvaluationWindow()
        {
            PatientHospitalEvaluationWindow hospitalEvaluation = new PatientHospitalEvaluationWindow();
            hospitalEvaluation.HospitalEvaluationViewModel.OnRequestClose += (s, e) => hospitalEvaluation.Close();
            hospitalEvaluation.Show();
        }

        public void CreateNoteWindow(int appointmentId)
        {
            PatientNoteView appointmentNote = new PatientNoteView(appointmentId);
            appointmentNote.AppointmentNoteViewModel.OnRequestClose += (s, e) => appointmentNote.Close();
            appointmentNote.Show();
        }

        public void CreateYesNoDialog(string message)
        {
            YesNoDialogMessage yesNoDialog = new YesNoDialogMessage(message);
            yesNoDialog.DialogMessageViewModel.OnRequestClose += (s, e) => yesNoDialog.Close();
            yesNoDialog.Show();
        }

        public void CreateFeedbackWindow()
        {
            FeedbackView feedbackView = new FeedbackView();
            feedbackView.FeedbackViewModel.OnRequestClose += (s, e) => feedbackView.Close();
            feedbackView.Show();
        }
    }
}
