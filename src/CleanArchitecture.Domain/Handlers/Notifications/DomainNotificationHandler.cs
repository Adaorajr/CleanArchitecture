using CleanArchitecture.Domain.Commons;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly List<Notification> _notifications;

        public DomainNotificationHandler()
        {
            this._notifications = new List<Notification>();
        }
        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            this._notifications.Add(new Notification(notification.Key, notification.Message));
            return Task.CompletedTask;
        }

        public virtual List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public virtual Dictionary<string, string[]> GetNotificationsByKey()
        {
            var strings = this._notifications.Select(s => s.Key).Distinct();
            var dictionary = new Dictionary<string, string[]>();
            foreach (var str in strings)
            {
                var key = str;
                dictionary[key] = this._notifications.Where(w => w.Key.Equals(key, StringComparison.Ordinal)).Select(s => s.Message).ToArray();
            }
            return dictionary;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }
    }
}
