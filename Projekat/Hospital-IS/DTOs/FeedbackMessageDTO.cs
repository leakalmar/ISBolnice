using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class FeedbackMessageDTO
    {
        public String Text { get; set; }
        public DateTime DateSent { get; set; }

        public FeedbackMessageDTO(string text, DateTime dateSent)
        {
            Text = text;
            DateSent = dateSent;
        }

        public FeedbackMessageDTO()
        { 
        }
    }
}
