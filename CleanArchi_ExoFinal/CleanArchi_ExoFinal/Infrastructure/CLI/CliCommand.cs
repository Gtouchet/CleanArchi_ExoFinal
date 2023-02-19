namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public enum CliCommand
{
    Unknown,
    Agenda,
    Quit,
}

public class UnkownDomainException : Exception
{
    public UnkownDomainException(string message) : base(message)
    {

    }
}