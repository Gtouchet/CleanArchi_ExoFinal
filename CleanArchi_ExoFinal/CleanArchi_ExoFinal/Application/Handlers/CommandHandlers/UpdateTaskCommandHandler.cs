using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.CommandHandlers;

public struct Void { }

internal class UpdateTaskCommand : Message
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State? State { get; set; }
}

internal class UpdateTaskCommandHandler : CommandBase, ICommandHandler<Void, UpdateTaskCommand>
{
    public UpdateTaskCommandHandler(Context context) : base(context) { }

    public Void Handle(UpdateTaskCommand message)
    {
        TaskEntity? task = this.Context.Tasks.Read(message.Id);
        if (task is not null)
        {
            task.Description = message.Description ?? task.Description;
            task.DueDate = message.DueDate ?? task.DueDate;
            task.State = message.State ?? task.State;
            this.Context.Tasks.Update(message.Id, task);
        }
        return default;
    }
}
