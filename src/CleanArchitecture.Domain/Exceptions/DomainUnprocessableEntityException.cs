namespace CleanArchitecture.Domain.Exceptions
{
    public class DomainUnprocessableEntityException : DomainException
    {
        public DomainUnprocessableEntityException(string message)
        : base(message)
        {
        }
    }
}