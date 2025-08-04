namespace Blog.CrossCutting.NotificationHandler
{
    public interface INotificationHandler
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void AddNotification(string key, string message);
        void AddNotifications(IEnumerable<Notification> notifications);
    }
}
