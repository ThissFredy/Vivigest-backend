using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Vivigest_backend.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public Error Error { get; set; }
    

        public Result(bool isSuccess, Error error, T? value = default)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value)
            => new Result<T>(true, Error.None, value);

        public static Result<T> Failure(Error error)
            => new Result<T>(false, error);
    }
}

