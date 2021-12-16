namespace CleanArchitecture.Domain.Exceptions
{
    public class DomainNotFoundException : DomainException
    {
        public DomainNotFoundException(string message)
        : base(message)
        {

        }
    }
}