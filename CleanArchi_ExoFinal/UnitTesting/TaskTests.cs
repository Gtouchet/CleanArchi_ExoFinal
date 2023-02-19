

namespace UnitTesting;

public class TaskTests : TestBase
{
    [Fact(DisplayName = "Creating a new task, then reading all tasks, should return the task")]
    public void Test1()
    {
        this.HandlersProcessor.ExecuteCommand(new CreateTaskCommand());
        List<TaskEntity> tasks = (List<TaskEntity>)this.HandlersProcessor.ExecuteQuery(new ReadTasksQuery());
        tasks.Count.Should().Be(1);
    }

    [Fact(DisplayName = "Creating a new task, then reading a task by ID, should return the task")]
    public void Test2()
    {
        Guid id = (Guid)this.HandlersProcessor.ExecuteCommand(new CreateTaskCommand());
        TaskEntity task = (TaskEntity)this.HandlersProcessor.ExecuteQuery(new ReadTaskQuery()
        {
            Id = id,
        });
        task.Should().NotBeNull();
    }

    [Fact(DisplayName = "Creating a new task, then deleting it, then reading the task, should throw exception")]
    public void Test3()
    {
        Guid id = (Guid)this.HandlersProcessor.ExecuteCommand(new CreateTaskCommand());
        this.HandlersProcessor.ExecuteCommand(new DeleteTaskCommand()
        {
            Id = id,
        });
        try
        {
            TaskEntity task = (TaskEntity)this.HandlersProcessor.ExecuteQuery(new ReadTaskQuery()
            {
                Id = id,
            });
        }
        catch (HandlerException e)
        {
            e.Message.Should().Be($"Task id {id} not found");
        }
    }

    [Fact(DisplayName = "Creating a new task, then updating its description, then reading it, should update the description")]
    public void Test4()
    {
        Guid id = (Guid)this.HandlersProcessor.ExecuteCommand(new CreateTaskCommand()
        {
            Description = "Old description",
        });
        this.HandlersProcessor.ExecuteCommand(new UpdateTaskCommand()
        {
            Id = id,
            Description = "New description",
        });
        TaskEntity task = (TaskEntity)this.HandlersProcessor.ExecuteQuery(new ReadTaskQuery()
        {
            Id = id,
        });
        task.Description.Should().Be("New description");
    }

    [Fact(DisplayName = "Creating a new task, then adding a subtask to it, then reading it, should contain the subtask")]
    public void Test5()
    {
        Guid id = (Guid)this.HandlersProcessor.ExecuteCommand(new CreateTaskCommand()
        {
            Description = "Old description",
        });
        this.HandlersProcessor.ExecuteCommand(new AddSubTaskCommand()
        {
            Id = id,
        });
        TaskEntity task = (TaskEntity)this.HandlersProcessor.ExecuteQuery(new ReadTaskQuery()
        {
            Id = id,
        });
        task.Subtasks.Count.Should().Be(1);
    }
}