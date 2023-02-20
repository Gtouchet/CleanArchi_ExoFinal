using CleanArchi_ExoFinal.Domain;

namespace CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;

public struct AgendaCommand
{
    public EAgendaCommand Command;
    public Guid? Id;
    public string? Description;
    public DateTime? DueDate;
    public State? State;
}

public enum EAgendaCommand
{
    Unknown,
    Help,
    Create,
    Read,
    ReadAll,
    Delete,
    Update,
    Add,
}