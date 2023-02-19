using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.QueryHandlers;

public class ReadTasksQuery : Query
{
    
}

public class ReadTasksQueryHandler : QueryBase, IQueryHandler<List<TaskEntity>, ReadTasksQuery>
{
    public ReadTasksQueryHandler(Context context) : base(context) { }

    public List<TaskEntity> Handle(ReadTasksQuery message)
    {
        return this.Context.Tasks.Read();
    }
}
