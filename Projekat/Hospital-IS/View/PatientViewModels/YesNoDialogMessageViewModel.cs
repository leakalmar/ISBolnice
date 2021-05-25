using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class YesNoDialogMessageViewModel : BindableBase
    {
        private string message;
        public MyICommand PositiveAnswer { get; set; }
        public MyICommand NegativeAnswer { get; set; }
        public EventHandler OnRequestClose;

        public YesNoDialogMessageViewModel(string message)
        {
            Message = message;
            PositiveAnswer = new MyICommand(PosAnswer);
            NegativeAnswer = new MyICommand(NegAnswer);
        }

        public string Message
        {
            get { return message; }
            set
            {
                if(message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        private void NegAnswer()
        {
            OnRequestClose(this, new EventArgs());
        }

        private void PosAnswer()
        {
            PatientHospitalEvaluationWindow hospitalEvaluation = new PatientHospitalEvaluationWindow();
            hospitalEvaluation.HospitalEvaluationViewModel.OnRequestClose += (s, e) => hospitalEvaluation.Close();
            hospitalEvaluation.Show();
            OnRequestClose(this, new EventArgs());
        }
    }
}
