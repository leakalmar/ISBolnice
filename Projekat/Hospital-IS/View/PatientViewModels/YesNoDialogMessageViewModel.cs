using System;

namespace Hospital_IS.View.PatientViewModels
{
    public class YesNoDialogMessageViewModel : BindableBase
    {
        private string message;
        public MyICommand PositiveAnswer { get; set; }
        public MyICommand NegativeAnswer { get; set; }
        public EventHandler OnRequestClose;
        private readonly MyWindowFactory windowFactory;
        public YesNoDialogMessageViewModel(string message)
        {
            Message = message;
            PositiveAnswer = new MyICommand(PosAnswer);
            NegativeAnswer = new MyICommand(NegAnswer);
            windowFactory = new WindowProductionFactory();
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
            windowFactory.CreateHospitalEvaluation();
            OnRequestClose(this, new EventArgs());
        }
    }
}
