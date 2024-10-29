namespace Service.Helper
{
    public class ExceptionManager
    {
        public int? Code { get; set; }

        public string? Message { get; set; }

        public ICollection<ErrorValidation>? Errors { get; set; }

        public static ExceptionManager RequestError(int code, string message)
        {
            return new ExceptionManager
            {
                Code = code,
                Message = message
            };
        }

        public static ExceptionManager<T> RequestError<T>(int code, string message)
        {
            return new ExceptionManager<T>
            {
                Code = code,
                Message = message,
            };
        }

        public static ExceptionManager<T> NotFound<T>(string message) => new() { Code = 404, Message = message };
        public static ExceptionManager<T> BadRequest<T>(string message) => new() { Code = 400, Message = message };
        public static ExceptionManager<T> Unauthorized<T>(string message) => new() { Code = 401, Message = message };
        public static ExceptionManager<T> Forbidden<T>(string message) => new() { Code = 403, Message = message };
        public static ExceptionManager<T> NotAcceptable<T>(string message) => new() { Code = 406, Message = message };
        public static ExceptionManager<T> Ok<T>(T Data) => new() { Code = 200, Data = Data };
        public static ExceptionManager<T> Created<T>(T Data) => new() { Code = 201, Data = Data };
    }
    public class ExceptionManager<T> : ExceptionManager
    {
        public T? Data { get; set; }
    }
}
