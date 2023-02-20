using CleanArchi_ExoFinal.Domain;
using System.Text.Json;

namespace CleanArchi_ExoFinal;

public static class Bootstrap
{
    public static void Seed()
    {
        List<TaskEntity> tasks = new List<TaskEntity>()
        {
            new TaskEntity()
            {
                Description = "init a project to create an app for my tasks",
                DueDate = DateTimeOffset.Now.AddDays(2),
                State = State.Done,
                Subtasks = new List<TaskEntity>()
                {
                    new TaskEntity()
                    {
                        Description = "init repo for the project",
                        DueDate = DateTimeOffset.Now.AddDays(2),
                        State = State.Done,
                    },
                }
            },
            new TaskEntity()
            {
                Description = "do a clean archi",
                DueDate = DateTimeOffset.Now.AddDays(5),
                State = State.Todo,
            },
        };
        
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText("tasks.json", json);
    }
}
