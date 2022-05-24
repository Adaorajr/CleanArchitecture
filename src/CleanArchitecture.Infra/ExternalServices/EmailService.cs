using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Interfaces.ExternalServices;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.ExternalServices
{
    public class EmailService : IEmailService
    {
        public async Task Send(Email email)
        {
            await Task.CompletedTask;
        }
    }
}
