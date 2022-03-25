using System;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
                var problemDetails = new ProblemDetails
                {
                    Instance = context.Request.HttpContext.Request.Path,
                    Title = "The request is invalid!",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                };

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                await context.Response.WriteAsync(json);
            }
            catch (DomainUnprocessableEntityException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                var tst = context.Response.StatusCode;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}