using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure;

namespace CleanArchi_ExoFinal.Handlers;

public abstract class CommandBase
{
    protected readonly IParser<TaskEntity> parser;

    public CommandBase(IParser<TaskEntity> parser)
	{
        this.parser = parser;
    }
}
