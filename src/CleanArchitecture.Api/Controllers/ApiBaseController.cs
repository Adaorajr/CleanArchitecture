using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Handlers.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        public ApiBaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }
        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected ActionResult ResponsePutPatch()
        {
            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult ResponseDelete()
        {
            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult ResponseDelete<T>(T item)
        {
            if (IsValidOperation())
            {
                if (item == null)
                    return NoContent();

                return Ok(item);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult ResponsePost<T>(string action, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, route, result);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult ResponsePost<T>(string action, string controller, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, controller, route, result);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
        {
            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        protected ActionResult ResponseError()
        {
            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }
    }
}