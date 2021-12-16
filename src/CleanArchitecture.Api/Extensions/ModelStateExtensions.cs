using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArchitecture.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorsMessages(this ModelStateDictionary modelState)
        {
            return modelState
            .SelectMany(m => m.Value.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        }
    }
}