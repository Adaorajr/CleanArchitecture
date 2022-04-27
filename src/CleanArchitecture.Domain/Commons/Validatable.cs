using Flunt.Notifications;
using MediatR;

namespace CleanArchitecture.Domain.Commons
{
    public abstract class Validatable : Notifiable<Notification>, INotification
    {
        public abstract void Validate();
    }
}
