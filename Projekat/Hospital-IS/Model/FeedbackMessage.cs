using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Model
{
    class FeedbackMessage
    {
        public String Text { get; set; }
        public DateTime DateSent { get; set; }

        public FeedbackMessage(string text, DateTime dateSent)
        {
            Text = text;
            DateSent = dateSent;
        }
    }
}
