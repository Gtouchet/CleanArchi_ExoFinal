using CleanArchi_ExoFinal.Domain;
namespace CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;

public struct AgendaCommand
{
    public ECommand Command;
    public int? Id;
    public string? Content;
    public DateTime? DueDate;
    public State Status;
}
