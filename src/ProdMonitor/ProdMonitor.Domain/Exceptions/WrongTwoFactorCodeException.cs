namespace ProdMonitor.Domain.Exceptions;

public class WrongTwoFactorCodeException: Exception
{
    public WrongTwoFactorCodeException()
    {
    }
    public WrongTwoFactorCodeException(string? message) : base(message)
    {
    }

    public WrongTwoFactorCodeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}