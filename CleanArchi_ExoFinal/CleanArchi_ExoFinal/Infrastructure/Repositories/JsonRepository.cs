using CleanArchi_ExoFinal.Domain;
using System.Text.Json;

namespace CleanArchi_ExoFinal.Infrastructure.Repositories;

public class JsonRepository : IRepository<TaskEntity>
{
    private readonly string filepath;

    public JsonRepository(string filepath)
    {
        this.filepath = filepath;
    }
    
    public Guid Write(TaskEntity data)
    {
        List<TaskEntity> tasks = this.Read();
        tasks.Add(data);
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(filepath, json);
        return data.Id;
    }

    public List<TaskEntity> Read()
    {
        string fileContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<List<TaskEntity>>(fileContent) ?? new List<TaskEntity>();
    }

    public TaskEntity? Read(Guid id)
    {
        string fileContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<List<TaskEntity>>(fileContent)!.FirstOrDefault(t => t.Id.Equals(id));
    }

    public void Update(Guid id, TaskEntity data)
    {
        List<TaskEntity> tasks = this.Read();
        TaskEntity? task = tasks.FirstOrDefault(t => t.Id.Equals(id));
        if (task is not null)
        {
            task.Description = data.Description;
            task.DueDate = data.DueDate;
            task.State = data.State;
            task.Subtasks = data.Subtasks;
        }
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(filepath, json);
    }

    public bool Delete(Guid id)
    {
        TaskEntity? task = this.Read(id);
        if (task != null)
        {
            List<TaskEntity> tasks = this.Read();
            tasks.RemoveAll(t => t.Id.Equals(id));
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(filepath, json);
            return true;
        }
        return false;
    }

    public bool Exists(Guid id)
    {
        string fileContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<List<TaskEntity>>(fileContent)!.Exists(t => t.Id.Equals(id));
    }
}
