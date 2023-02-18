using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Handlers.CommandHandlers;

public struct Void { }

public class CreateTaskCommand : Message
{
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State State { get; set; }
}

public class CreateTaskCommandHandler : CommandBase, ICommandHandler<Void, CreateTaskCommand>
{
    public CreateTaskCommandHandler(Context context) : base(context) { }
    
    public Void Handle(CreateTaskCommand message)
    {
        this.Context.Tasks.Write(new TaskEntity()
        {
            Description = message.Description,
            DueDate = message.DueDate,
            State = message.State,
        });
        return default;
    }
}
