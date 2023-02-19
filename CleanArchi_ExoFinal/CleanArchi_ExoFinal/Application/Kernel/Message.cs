namespace CleanArchi_ExoFinal.Application.Kernel;

public class UnknownMessageException : Exception
{
    public UnknownMessageException(Type messageType) : base($"Error, unknown message type '{messageType.Name}', check if your command inherits Message")
    {

    }
}

public abstract class Message
{

}
