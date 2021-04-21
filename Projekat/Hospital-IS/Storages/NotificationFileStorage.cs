using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Hospital_IS.Storages
{
    class NotificationFileStorage
    {
        private string fileLocation;

        public NotificationFileStorage()
        {
            this.fileLocation = "../../../FileStorage/notifications.json";
        }

        public List<Notification> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Notification> notifications = JsonConvert.DeserializeObject<List<Notification>>(text);

            return notifications;
        }

        public List<Notification> GetAllByUser(Patient patient)
        {
            List<Notification> notifications = GetAll();
            List<Notification> userNotifications = new List<Notification>();

            foreach (Notification notif in notifications)
            {
                if (patient.Notifications != null)
                    foreach (int id in patient.Notifications)
                    {
                        if (notif.Id == id)
                            userNotifications.Add(notif);
                    }
            }

            return userNotifications;
        }

        public void SaveNotification(Notification notification)
        {
            List<Notification> notifications = GetAll();
            notifications.Insert(0, notification);

            var file = JsonConvert.SerializeObject(notifications, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public Boolean UpdateNotification(Notification notification)
        {
            List<Notification> notifications = GetAll();

            for (int i = 0; i < notifications.Count; i++)
            {
                if (notification.Id.Equals(notifications[i].Id))
                {
                    notifications.Remove(notifications[i]);
                    notifications.Insert(i, notification);

                    var file = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.fileLocation))
                    {
                        writer.Write(file);
                    }

                    return true;
                }
            }
            return false;
        }

        public Boolean DeleteNotification(Notification notification)
        {
            List<Notification> notifications = GetAll();

            for (int i = 0; i < notifications.Count; i++)
            {
                if (notification.Id.Equals(notifications[i].Id))
                {
                    notifications.Remove(notifications[i]);

                    var file = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.fileLocation))
                    {
                        writer.Write(file);
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
