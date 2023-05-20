namespace CleanArchitectureNetCore.Api.Common;

public class Envelope<T>
{
    protected internal Envelope(T result, string errorMessage)
    {
        Result = result;
        ErrorMessage = errorMessage;
        TimeGenerated = DateTime.UtcNow;
    }

    public T Result { get; }
    public string ErrorMessage { get; }
    public DateTime TimeGenerated { get; }
}

public class Envelope : Envelope<string>
{
    protected Envelope(string errorMessage)
        : base(null, errorMessage)
    {
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }

    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    public static Envelope Error(string errorMessage)
    {
        return new Envelope(errorMessage);
    }
}