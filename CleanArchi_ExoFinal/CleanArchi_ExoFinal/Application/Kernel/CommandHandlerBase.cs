using CleanArchi_ExoFinal.Infrastructure.Repositories;

namespace CleanArchi_ExoFinal.Application.Kernel;

public abstract class CommandHandlerBase
{
    protected readonly Context Context;
    protected readonly Logger Logger;

    public CommandHandlerBase(
        Context context,
        Logger logger)
    {
        this.Context = context;
        this.Logger = logger;
    }
}
