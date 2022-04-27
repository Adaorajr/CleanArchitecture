namespace CleanArchitecture.Domain.Commons
{
    public class GenericCommandResult
    {
        public GenericCommandResult()
        {
        }
        public GenericCommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }

    public class GenericCommandResult<TResponse> : GenericCommandResult
    {
        public TResponse Data { get; private set; }
        public GenericCommandResult(bool success, string message, TResponse data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}