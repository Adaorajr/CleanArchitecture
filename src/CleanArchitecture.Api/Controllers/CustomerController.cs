using CleanArchitecture.Domain.Commands.Requests.Customer;
using CleanArchitecture.Domain.Commands.Responses.Customer;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.DTO.Customer;
using CleanArchitecture.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerQueries _customerQueries;
        private readonly IMediator _mediator;
        public CustomerController(
            ICustomerQueries customerQueries
            , IMediator mediator
            , INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _customerQueries = customerQueries;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            return ResponseGet(await _customerQueries.GetAllCustomers());
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCustomerResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateCustomerResponse>> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return ResponseError();
            }

            var response = result as GenericCommandResult<CreateCustomerResponse>;
            return ResponsePost(nameof(Get), new { id = response.Data.Id }, response.Data);
        }
    }
}
