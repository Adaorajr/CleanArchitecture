using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Handlers.Notifications;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Pipelines
{
    public class ValidateCommand<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TResponse : GenericCommandResult
            where TRequest : IRequest<TResponse>
    {
        private readonly IDomainNotificationMediatorService _domainNotification;
        public ValidateCommand(IDomainNotificationMediatorService domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is Validatable validatable)
            {
                validatable.Validate();
                if (!validatable.IsValid)
                {
                    GenericCommandResult validations = new GenericCommandResult(false, "Invalid Request!");
                    foreach (Notification notification in validatable.Notifications)
                        _domainNotification.Notify(new DomainNotification(notification.Key, notification.Message));

                    var response = validations as TResponse;
                    return response;
                }
            }

            TResponse result = await next();
            return result;
        }
    }
}
