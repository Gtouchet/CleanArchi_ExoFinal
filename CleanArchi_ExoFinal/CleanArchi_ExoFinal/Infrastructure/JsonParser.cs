using CleanArchi_ExoFinal.Domain;
using System.Text.Json;

namespace CleanArchi_ExoFinal.Infrastructure;

public class JsonParser : IParser<TaskEntity>
{
    private readonly string filepath;

    public JsonParser(string filepath)
    {
        this.filepath = filepath;
    }

    public List<TaskEntity> Read()
    {
        string fileContent = File.ReadAllText(this.filepath);
        return JsonSerializer.Deserialize<List<TaskEntity>>(fileContent) ?? new List<TaskEntity>();
    }

    public void Write(TaskEntity data)
    {
        List<TaskEntity> tasks = this.Read();
        tasks.Add(data);
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText("tasks.json", json);
    }
}
