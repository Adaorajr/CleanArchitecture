using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.Api.Filters.Interface
{
    public interface IExceptionFilter : IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}