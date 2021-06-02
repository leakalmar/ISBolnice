using Hospital_IS.DTOs;
using Hospital_IS.Model;
using Hospital_IS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Controllers
{
    class FeedbackMessageController
    {
        private static FeedbackMessageController instance = null;
        public static FeedbackMessageController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackMessageController();
                }
                return instance;
            }
        }

        private FeedbackMessageController()
        {
        }

        public List<FeedbackMessage> GetAll()
        {
            return FeedbackMessageService.Instance.AllMessages;
        }

        public void AddFeedbackMessage(FeedbackMessageDTO message)
        {
            FeedbackMessage feedbackMessage = new FeedbackMessage(message.Text, message.DateSent);
            FeedbackMessageService.Instance.AddFeedbackMessage(feedbackMessage);
        }
    }
}
