namespace CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;

public enum EDomain
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