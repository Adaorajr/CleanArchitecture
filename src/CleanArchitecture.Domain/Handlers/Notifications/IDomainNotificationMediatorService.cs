using CleanArchitecture.Domain.Commons;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Notifications
{
    public interface IDomainNotificationMediatorService
    {
        void Notify(DomainNotification notify);
    }
}
