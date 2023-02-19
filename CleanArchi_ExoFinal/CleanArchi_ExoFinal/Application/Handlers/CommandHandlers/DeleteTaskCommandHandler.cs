using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using CleanArchi_ExoFinal.Kernel;

namespace CleanArchi_ExoFinal.Handlers.CommandHandlers;

public class DeleteTaskCommand : Command
{
    public string? Id { get; set; }
}

public class DeleteTaskCommandHandler : CommandBase, ICommandHandler<bool, DeleteTaskCommand>
{
    public DeleteTaskCommandHandler(Context context) : base(context) { }

    public bool Handle(DeleteTaskCommand message)
    {
        return this.Context.Tasks.Delete(Guid.Parse(message.Id!));
    }
}
