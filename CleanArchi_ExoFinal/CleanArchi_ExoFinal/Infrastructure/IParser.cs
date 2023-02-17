using CleanArchi_ExoFinal.Domain;

namespace CleanArchi_ExoFinal.Infrastructure;

public interface IParser<T> where T : Entity
{
    public List<T> Read();
    public void Write(T data);
}
