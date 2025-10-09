namespace DynamicQueries.BusinessObjects.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T SuccessValue { get; private set; }
        public string ErrorMessage { get; private set; }
        private Result(bool isSuccess) => IsSuccess = isSuccess;
        public static Result<T> Ok(T result) => new(true) { SuccessValue = result };
        public static Result<T> Fail(string errorMessage) => new(false) { ErrorMessage = errorMessage };
    }
}
