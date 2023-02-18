using CleanArchi_ExoFinal.Application.Kernel;

namespace CleanArchi_ExoFinal.Kernel;

public interface IQueryHandler<R, C> where C : Message
{
    public R Handle(C message);
}
