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

        public Notification(string title, string text, DateTime datePosted)
        {
            Title = title;
            Text = text;
            DatePosted = datePosted;
            LastChanged = datePosted;
            generateId();
        }
        public Notification(string title, string text, DateTime datePosted, DateTime lastChanged) : this(title, text, datePosted)
        {
            LastChanged = lastChanged;
            generateId();
        }

        public Notification()
        {
            generateId();
        }

        private void generateId() 
        {
            Random rand = new Random();
            this.Id = rand.Next(1, 100000);
        }

    }
}
