using Blog.Domain.Errors;

namespace Blog.Domain.Common
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public ResultError Error { get; }

        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
            Error = null!;
        }

        private Result(ResultError error)
        {
            Value = default!;
            IsSuccess = false;
            Error = error;
        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(ResultError error) => new(error);

        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator Result<T>(ResultError error) => Failure(error);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public ResultError Error { get; }

        private Result(bool isSuccess, ResultError error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null!);
        public static Result Failure(ResultError error) => new(false, error);

        public static implicit operator Result(ResultError error) => Failure(error);
    }
}
