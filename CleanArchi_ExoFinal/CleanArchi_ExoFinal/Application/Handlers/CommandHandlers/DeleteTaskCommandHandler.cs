using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Handlers.CommandHandlers;

public class DeleteTaskCommand : Command
{
    public Guid Id { get; set; }
}

public class DeleteTaskCommandHandler : CommandHandlerBase, ICommandHandler<bool, DeleteTaskCommand>
{
    public DeleteTaskCommandHandler(Context context, Logger logger) : base(context, logger) { }

    public bool Handle(DeleteTaskCommand message)
    {
        this.Logger.Log($"{this.GetType().Name} called on task ID {message.Id}");

        return this.Context.Tasks.Delete(message.Id);
    }
}
