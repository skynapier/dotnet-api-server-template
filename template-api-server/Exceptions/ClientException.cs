public class ClientException : Exception
{
    public ClientErrorType ErrorType { get; }

    public ClientException(ClientErrorType errorType) 
        : base(GetDefaultMessage(errorType))
    {
        ErrorType = errorType;
    }

    public ClientException(ClientErrorType errorType, string message) 
        : base(message)
    {
        ErrorType = errorType;
    }

    public ClientException(ClientErrorType errorType, string message, Exception inner) 
        : base(message, inner)
    {
        ErrorType = errorType;
    }

    private static string GetDefaultMessage(ClientErrorType errorType)
    {
        switch (errorType)
        {
            case ClientErrorType.NotFound:
                return "Client not found.";
            case ClientErrorType.Duplicate:
                return "Client already exists.";
            case ClientErrorType.Validation:
                return "Client validation failed.";
            default:
                return "An error occurred.";
        }
    }
}
