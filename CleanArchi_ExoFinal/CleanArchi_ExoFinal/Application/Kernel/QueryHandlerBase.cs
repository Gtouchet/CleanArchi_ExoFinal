using CleanArchi_ExoFinal.Infrastructure.Repositories;

namespace CleanArchi_ExoFinal.Application.Kernel;

public abstract class QueryHandlerBase
{
    protected readonly Context Context;
    
    public QueryHandlerBase(Context context)
    {
        this.Context = context;
    }
}
