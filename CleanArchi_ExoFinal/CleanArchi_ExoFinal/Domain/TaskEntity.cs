namespace CleanArchi_ExoFinal.Domain;

public enum State
{
    todo,
    pending,
    progress,
    done,
    cancelled,
    closed
}

public class TaskEntity : Entity
{
    public string? Description { get; set; } = "No description";
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CloseDate { get; set; }
    public State State { get; set; } = default(State);
    public List<TaskEntity> Subtasks { get; set; } = new List<TaskEntity>();
}
