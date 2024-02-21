using Handcom.Services.Services.Notifications;

namespace Handcom.Services.Interfaces
{
    public interface INotifierService
    {
        bool HasNotifications();
        IEnumerable<Notification> GetNotifications();
        void Handle(Notification notification);
        void ClearErrors();
    }
}
