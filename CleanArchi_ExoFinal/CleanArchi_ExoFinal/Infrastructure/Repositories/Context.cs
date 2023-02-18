using CleanArchi_ExoFinal.Domain;

namespace CleanArchi_ExoFinal.Infrastructure.Repositories;

public class Context
{
    public IRepository<TaskEntity> Tasks { get; }

    public Context(IRepository<TaskEntity> tasks)
    {
        this.Tasks = tasks;
    }
}
