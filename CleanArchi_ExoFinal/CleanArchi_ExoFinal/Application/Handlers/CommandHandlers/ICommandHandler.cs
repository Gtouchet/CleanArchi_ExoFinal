using CleanArchi_ExoFinal.Handlers;

namespace CleanArchi_ExoFinal.Kernel;

public interface ICommandHandler<T> where T : Command
{
    public void Handle(T command);
}
