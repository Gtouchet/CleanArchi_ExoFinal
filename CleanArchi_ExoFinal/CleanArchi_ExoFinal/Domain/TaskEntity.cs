using System.Text;

namespace CleanArchi_ExoFinal.Domain;

public enum State
{
    Todo,
    Pending,
    Progress,
    Done,
    Cancelled,
    Closed
}

public class TaskEntity : Entity
{
    public string Description { get; set; } = "No description";
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CloseDate { get; set; }
    public State State { get; set; } = default(State);
    public List<TaskEntity> Subtasks { get; set; } = new List<TaskEntity>();

    public override string ToString()
    {
        StringBuilder task = new StringBuilder(
            $"\n{this.Id}\n" +
            $"{this.Description} -> {this.State}\n" +
            $"Created {this.CreationDate.ToString("dd/mm/yyyy")} -> Due date {this.DueDate?.ToString("dd/mm/yyyy")} " +
            $"({(this.DueDate != null ? "Due in : " + (this.DueDate - this.CreationDate).Value.Days.ToString() + " days" : "No deadline")})\n" +
            $"Subtasks : ");

        if (this.Subtasks.Count > 0)
        {
            this.Subtasks.ForEach(t => task.AppendLine(t.ToString()));
        }
        else
        {
            task.AppendLine("none");
        }
        task.Append($"----------------------------------");
        
        return task.ToString();
    }
}
