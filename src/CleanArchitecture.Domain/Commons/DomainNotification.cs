using MediatR;

namespace CleanArchitecture.Domain.Commons
{
    public class DomainNotification : INotification
    {
        public DomainNotification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }
        public string Message { get; }
    }
}
