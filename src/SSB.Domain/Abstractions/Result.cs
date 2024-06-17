namespace SSB.Domain.Abstractions;

public record Result
{
    #region Properties
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    #endregion // Properties

    #region Constructor
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }
    #endregion // Constructor

    #region Methods
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    #endregion // Methods
}

public sealed record Result<T> : Result where T : class
{
    public T? Value { get; init; }

    public Result(bool isSuccess, Error error, T? value = default) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T? value) => new Result<T>(true, Error.None, value);
    public static Result<T> Failure(Error error, T? value = default) => new Result<T>(false, Error.None, null);

    public static implicit operator Result<T>(Error error) => Failure(error);
}