using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.QueryHandlers;

public class ReadTaskQuery : Message
{
    public Guid Id { get; set; }
}

public class ReadTaskQueryHandler : QueryBase, IQueryHandler<TaskEntity, ReadTaskQuery>
{
    public ReadTaskQueryHandler(Context context) : base(context) { }

    public TaskEntity Handle(ReadTaskQuery message)
    {
        if (!this.Context.Tasks.Exists(message.Id))
        {
            throw new HandlerException($"Task id {message.Id} not found");
        }
        return this.Context.Tasks.Read(message.Id)!;
    }
}
