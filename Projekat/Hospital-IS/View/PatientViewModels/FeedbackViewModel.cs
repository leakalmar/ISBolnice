using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class FeedbackViewModel : BindableBase
    {
        private string comment;

        public MyICommand SendFeedback { get; set; }
        public MyICommand CloseFeedback { get; set; }
        public EventHandler OnRequestClose;
        private readonly MyWindowFactory windowFactory;

        public FeedbackViewModel()
        {
            SendFeedback = new MyICommand(Send);
            CloseFeedback = new MyICommand(Close);
            windowFactory = new WindowProductionFactory();
            Comment = "";
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private void Send()
        {
            FeedbackMessageDTO feedback = new FeedbackMessageDTO(Comment, DateTime.Today.Date);
            FeedbackMessageController.Instance.AddFeedbackMessage(feedback);
            OnRequestClose(this, new EventArgs());
        }

        private void Close()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
