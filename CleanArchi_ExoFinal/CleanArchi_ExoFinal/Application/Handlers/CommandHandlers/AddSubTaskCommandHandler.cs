using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.CommandHandlers;

internal class AddSubTaskCommand : Message
{
    public Guid TaskId { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State? State { get; set; }
}
internal class AddSubTaskCommandHandler : CommandBase, ICommandHandler<Void, AddSubTaskCommand>
{
    public AddSubTaskCommandHandler(Context context) : base(context) { }

    public Void Handle(AddSubTaskCommand message)
    {
        TaskEntity? task = this.Context.Tasks.Read(message.TaskId);
        if (task != null)
        {
            task.Subtasks.Add(new TaskEntity()
            {
                Description = message.Description ?? "No description",
                DueDate = message.DueDate,
                State = message.State ?? default,
            });
            this.Context.Tasks.Update(message.TaskId, task);
        }
        return default;
    }
}
