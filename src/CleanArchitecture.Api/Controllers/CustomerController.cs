using CleanArchitecture.Domain.Commands.Requests.Customer;
using CleanArchitecture.Domain.Commands.Responses.Customer;
using CleanArchitecture.Domain.Queries.Requests.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);

            var prod = result.Data as CreateCustomerResponse;

            return CreatedAtAction(nameof(Get), new { id = prod.Id }, prod);
        }
    }
}
