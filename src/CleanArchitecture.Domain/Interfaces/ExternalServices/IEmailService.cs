using CleanArchitecture.Domain.Commons;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.ExternalServices
{
    public interface IEmailService
    {
        Task Send(Email email);
    }
}
