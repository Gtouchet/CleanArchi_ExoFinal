using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Handlers.CommandHandlers;

public class CreateTaskCommand : Command
{
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State? State { get; set; }
}

public class CreateTaskCommandHandler : CommandBase, ICommandHandler<Guid, CreateTaskCommand>
{
    public CreateTaskCommandHandler(Context context) : base(context) { }
    
    public Guid Handle(CreateTaskCommand message)
    {
        return this.Context.Tasks.Write(new TaskEntity()
        {
            Description = message.Description ?? "No description",
            DueDate = message.DueDate,
            State = message.State ?? default,
        });
    }
}
