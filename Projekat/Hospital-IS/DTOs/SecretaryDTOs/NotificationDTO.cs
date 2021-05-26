using System;
using System.Collections.Generic;

namespace Hospital_IS.DTOs.SecretaryDTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime LastChanged { get; set; }
        public List<int> Recipients { get; set; } = new List<int>();

        public NotificationDTO(int id, string title, string text, DateTime datePosted, DateTime lastChanged, List<int> recipients)
        {
            Id = id;
            Title = title;
            Text = text;
            DatePosted = datePosted;
            LastChanged = lastChanged;
            Recipients = recipients;
        }

        public NotificationDTO()
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
