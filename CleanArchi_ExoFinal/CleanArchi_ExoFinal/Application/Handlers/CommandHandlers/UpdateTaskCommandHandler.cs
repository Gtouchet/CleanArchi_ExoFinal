using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.CommandHandlers;

public struct Void { }

public class UpdateTaskCommand : Command
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State? State { get; set; }
}

public class UpdateTaskCommandHandler : CommandHandlerBase, ICommandHandler<Void, UpdateTaskCommand>
{
    public UpdateTaskCommandHandler(Context context, Logger logger) : base(context, logger) { }

    public Void Handle(UpdateTaskCommand message)
    {
        this.Logger.Log($"{this.GetType().Name} called on task ID {message.Id}");

        TaskEntity? task = this.Context.Tasks.Read(message.Id);
        if (task != null)
        {
            task.Description = message.Description ?? task.Description;
            task.DueDate = message.DueDate ?? task.DueDate;
            task.State = message.State ?? task.State;
            this.Context.Tasks.Update(message.Id, task);
        }
        return default;
    }
}
