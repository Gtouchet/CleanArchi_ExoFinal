using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Handlers.CommandHandlers;

public class CreateTaskCommand : Command
{
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public State State { get; set; }
}

public class CreateTaskCommandHandler : CommandBase, ICommandHandler<CreateTaskCommand>
{
    public CreateTaskCommandHandler(IParser<TaskEntity> parser) : base(parser) { }

    public void Handle(CreateTaskCommand command)
    {
        this.parser.Write(new TaskEntity()
        {
            Description = command.Description,
            DueDate = command.DueDate,
            State = command.State,
        });
    }
}
