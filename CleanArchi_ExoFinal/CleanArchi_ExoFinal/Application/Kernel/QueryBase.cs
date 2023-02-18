using CleanArchi_ExoFinal.Infrastructure.Repositories;

namespace CleanArchi_ExoFinal.Application.Kernel;

public abstract class QueryBase
{
    protected readonly Context Context;

    public QueryBase(Context context)
    {
        this.Context = context;
    }
}
