using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Application.Handlers.CommandHandlers;

public class AddSubTaskCommand : Command
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State? State { get; set; }
}

public class AddSubTaskCommandHandler : CommandHandlerBase, ICommandHandler<Void, AddSubTaskCommand>
{
    public AddSubTaskCommandHandler(Context context, Logger logger) : base(context, logger) { }

    public Void Handle(AddSubTaskCommand message)
    {
        this.Logger.Log($"{this.GetType().Name} called on task ID {message.Id}");

        TaskEntity ? task = this.Context.Tasks.Read(message.Id);
        if (task != null)
        {
            task.Subtasks.Add(new TaskEntity()
            {
                Description = message.Description ?? "No description",
                DueDate = message.DueDate,
                State = message.State ?? default,
            });
            this.Context.Tasks.Update(message.Id, task);
        }
        return default;
    }
}
