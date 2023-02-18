using CleanArchi_ExoFinal.Infrastructure.Repositories;

namespace CleanArchi_ExoFinal.Application.Kernel;

public abstract class CommandBase
{
    protected readonly Context Context;

    public CommandBase(Context context)
    {
        this.Context = context;
    }
}
