namespace FiapAcademyAdmin.Application.Models
{
    public class ResultViewModel(bool isSuccess = true, string message = "")
    {
        public bool IsSuccess { get; private set; } = isSuccess;
        public string Message { get; private set; } = message;
    }

    public class ResultViewModel<T>(T? data, bool isSuccess = true, string message = "") : ResultViewModel(isSuccess, message)
    {
        public T? Data { get; private set; } = data;

        public static ResultViewModel<T> Success(T data)
            => new(data);

        public static ResultViewModel<T> Success(T data, string message)
            => new(data, true, message);

        public static ResultViewModel<T> Error(string message)
            => new(default, false, message);
    }
} 