using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        Notifiable<Notification> _notifications;

        protected bool IsValidOperation()
        {
            return (!_notifications.IsValid);
        }

        protected ActionResult<List<T>> ResponseGet<T>(List<T> result)
        {
            if (IsValidOperation())
            {
                if (result == null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            return BadRequest("teste");
        }
    }
}