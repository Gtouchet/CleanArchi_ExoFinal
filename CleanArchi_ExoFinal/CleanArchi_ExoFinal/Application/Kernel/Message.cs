namespace CleanArchi_ExoFinal.Application.Kernel;

public abstract class Message { }
public abstract class Command : Message { }
public abstract class Query : Message { }

public class UnknownMessageException : Exception
{
    public UnknownMessageException(Type messageType) : base($"Error, unknown message type '{messageType.Name}', check if your command inherits Message") { }
}
