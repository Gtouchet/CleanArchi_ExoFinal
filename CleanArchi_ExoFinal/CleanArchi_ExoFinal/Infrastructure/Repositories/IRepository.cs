using CleanArchi_ExoFinal.Domain;

namespace CleanArchi_ExoFinal.Infrastructure.Repositories;

public interface IRepository<T> where T : Entity
{
    public void Write(T data);
    public List<T> Read();
    public T? Read(Guid id);
    public void Update(Guid id, T data);
    public bool Delete(Guid id);

    public bool Exists(Guid id);
}
