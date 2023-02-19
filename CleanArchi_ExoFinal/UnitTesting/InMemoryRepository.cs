using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.Repositories;

namespace UnitTesting;

internal class InMemoryRepository : IRepository<TaskEntity>
{
    List<TaskEntity> tasks = new List<TaskEntity>();

    public bool Delete(Guid id)
    {
        int result = this.tasks.RemoveAll(t => t.Id.Equals(id));
        return result > 0;
    }

    public bool Exists(Guid id)
    {
        return this.tasks.Exists(t => t.Id.Equals(id));
    }

    public List<TaskEntity> Read()
    {
        return this.tasks;
    }

    public TaskEntity? Read(Guid id)
    {
        return this.tasks.FirstOrDefault(t => t.Id.Equals(id));
    }

    public void Update(Guid id, TaskEntity data)
    {
        TaskEntity? task = this.tasks.FirstOrDefault(t => t.Id.Equals(id));
        if (task is not null)
        {
            task.Description = data.Description;
            task.DueDate = data.DueDate;
            task.State = data.State;
            task.Subtasks = data.Subtasks;
        }
    }

    public Guid Write(TaskEntity data)
    {
        this.tasks.Add(data);
        return data.Id;
    }
}
