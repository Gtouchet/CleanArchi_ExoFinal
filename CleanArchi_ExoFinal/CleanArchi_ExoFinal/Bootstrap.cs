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
                CreationDate = DateTimeOffset.Now.AddDays(-1),
                DueDate = DateTimeOffset.Now.AddHours(-8),
                State = State.Done,
                Subtasks = new List<TaskEntity>()
                {
                    new TaskEntity()
                    {
                        Description = "init repo for the project",
                        CreationDate = DateTimeOffset.Now.AddDays(-1),
                        State = State.Done,
                    },
                    new TaskEntity()
                    {
                        Description = "create solution and project",
                        CreationDate = DateTimeOffset.Now.AddDays(-1),
                        State = State.Done,
                    },
                    new TaskEntity()
                    {
                        Description = "create a small poc to test formats",
                        CreationDate = DateTimeOffset.Now.AddDays(-1),
                        State = State.Progress,
                    }
                }
            },
        };
        
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText("tasks.json", json);
    }
}
