using Hospital_IS.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Controllers
{
    class NotificationController
    {
        private static NotificationController instance = null;
        public static NotificationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationController();
                }
                return instance;
            }
        }

        private NotificationController()
        {
        }

        public List<Notification> GetAll()
        {
            return NotificationService.Instance.AllNotifications;
        }

        public List<Notification> GetAllByUser(int userId)
        {
            return NotificationService.Instance.GetAllByUser(userId);
        }

        public void AddNotification(Notification notification)
        {
            NotificationService.Instance.AddNotification(notification);
        }

        public void ReloadNotifications()
        {
            NotificationService.Instance.ReloadNotifications();
        }

        public void UpdateNotification(Notification notification)
        {
            NotificationService.Instance.UpdateNotification(notification);
        }

        public void DeleteNotification(Notification notification)
        {
            NotificationService.Instance.DeleteNotification(notification);
        }

    }
}
