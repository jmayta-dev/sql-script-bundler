namespace SSB.Shared.Abstractions;

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

    public static implicit operator Result(Error error) => Failure(error);
    #endregion // Methods
}

public sealed record Result<T> : Result
{
    public T? Value { get; init; }

    #region Constructor
    public Result(bool isSuccess, Error error, T? value) : base(isSuccess, error)
    {
        if (isSuccess && value == null || !isSuccess && value != null)
            throw new ArgumentException("Invalid value assigned", nameof(value));

        Value = value;
    }
    #endregion

    #region Methods
    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static Result<T> Failure(Error error, T? value = default) => new(false, error, value);

    public static implicit operator Result<T>(Error error) => Failure(error);
    #endregion
}