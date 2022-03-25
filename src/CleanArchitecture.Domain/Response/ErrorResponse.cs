using System.Collections.Generic;

namespace CleanArchitecture.Domain.Response
{
    public class ErrorResponse
    {
        public ErrorResponse(string traceId, int status, string title, string type, List<string> errors)
        {
            TraceId = traceId;
            Status = status;
            Title = title;
            Type = type;
            Errors = errors;
        }

        public string TraceId { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public List<string> Errors { get; set; }
    }
}