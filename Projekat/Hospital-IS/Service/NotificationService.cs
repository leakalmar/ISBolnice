using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Service
{
    class NotificationService
    {
        private NotificationFileStorage nfs = new NotificationFileStorage();

        public List<Notification> allNotifications { get; set; }

        private static NotificationService instance = null;
        public static NotificationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationService();
                }
                return instance;
            }
        }

        private NotificationService()
        {
            allNotifications = nfs.GetAll();
        }

        public List<Notification> GetAllByUser(int userId)
        {
            List<Notification> userNotifications = new List<Notification>();

            foreach (Notification notif in allNotifications)
            {
                foreach (int id in notif.Recipients)
                {
                    if (userId == id)
                        userNotifications.Add(notif);
                }
            }

            return userNotifications;
        }

        public void AddNotification(Notification notification)
        {
            allNotifications.Add(notification);

            nfs.SaveNotifications(allNotifications);
        }

        public void UpdateNotification(Notification notification)
        {

            for (int i = 0; i < allNotifications.Count; i++)
            {
                if (notification.Id.Equals(allNotifications[i].Id))
                {
                    allNotifications.Remove(allNotifications[i]);
                    allNotifications.Insert(i, notification);
                }
            }

            nfs.SaveNotifications(allNotifications);
        }

        public void DeleteNotification(Notification notification)
        {
            for (int i = 0; i < allNotifications.Count; i++)
            {
                if (notification.Id.Equals(allNotifications[i].Id))
                {
                    allNotifications.Remove(allNotifications[i]);
                }
            }

            nfs.SaveNotifications(allNotifications);
        }


    }
}
