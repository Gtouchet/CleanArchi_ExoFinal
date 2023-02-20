namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public enum CliCommand
{
    Unknown,
    Agenda,
    Quit,
}

public class UnkownCommandException : Exception
{
    public UnkownCommandException(string message) : base(message)
    {

    }
}