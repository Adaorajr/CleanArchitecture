using System;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Api.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (DomainUnprocessableEntityException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}