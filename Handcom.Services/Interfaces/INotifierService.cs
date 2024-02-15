using Handcom.Services.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
