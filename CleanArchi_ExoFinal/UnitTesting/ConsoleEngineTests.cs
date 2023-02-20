using System.Reflection;

namespace UnitTesting;

public class ConsoleEngineTests : TestBase
{
    private readonly MethodInfo sendConsoleCommand = typeof(ConsoleEngine).GetMethod("ProcessCommand", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public ConsoleEngineTests()
    {
        this.ClearRepository();
    }

    [Fact(DisplayName = "Writing an agenda create command should create a new task")]
    public void Test1()
    {
        this.sendConsoleCommand.Invoke(this.ConsoleEngine, new object[] { CliCommand.Agenda, "agenda create -c:\"my task\"" });
        List<TaskEntity> tasks = (List<TaskEntity>)this.HandlersProcessor.ExecuteQuery(new ReadTasksQuery());
        tasks.Count.Should().Be(1);
    }

    [Fact(DisplayName = "Writing an agenda create command, without arguments, should throw an exception")]
    public void Test2()
    {
        try
        {
            this.sendConsoleCommand.Invoke(this.ConsoleEngine, new object[] { CliCommand.Agenda, "agenda create" });
        }
        catch (TargetInvocationException e)
        {
            e.InnerException!.Message.Should().Be("Error, not enough arguments");
        }
    }

    [Fact(DisplayName = "Writing an agenda create command should display the new task's ID in the console")]
    public void Test3()
    {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        this.sendConsoleCommand.Invoke(this.ConsoleEngine, new object[] { CliCommand.Agenda, "agenda create -c:\"my task\"" });
        string id = sw.ToString();
        Guid.TryParse(id, out Guid _).Should().BeTrue();
    }

    [Fact(DisplayName = "Writing an agenda create command should create a new task with the description")]
    public void Test4()
    {
        this.sendConsoleCommand.Invoke(this.ConsoleEngine, new object[] { CliCommand.Agenda, "agenda create -c:\"my task's description\"" });
        List<TaskEntity> tasks = (List<TaskEntity>)this.HandlersProcessor.ExecuteQuery(new ReadTasksQuery());
        tasks[0].Description.Should().Be("my task's description");
    }
}
