using Handcom.Services.Interfaces;

namespace Handcom.Services.Services.Notifications
{
    public class NotifierService : INotifierService
    {
        private readonly List<Notification> _notifications;

        public NotifierService() =>
            _notifications = new List<Notification>();

        public void Handle(Notification notification) =>
            _notifications.Add(notification);

        public void ClearErrors() =>
            _notifications.Clear();

        public IEnumerable<Notification> GetNotifications() =>
            _notifications;

        public bool HasNotifications() =>
            _notifications.Any();
    }
}
