using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events.Customer
{
    public class CreatedCustomerEvent : INotification
    {
        public string Customer { get; set; }
    }
}
