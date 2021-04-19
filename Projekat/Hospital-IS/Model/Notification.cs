using System;

namespace Model
{
    public class Notification
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime LastChanged { get; set; }

        public Notification(string title, string text, DateTime datePosted, DateTime lastChanged)
        {
            Title = title;
            Text = text;
            DatePosted = datePosted;
            LastChanged = lastChanged;
        }
    }
}
