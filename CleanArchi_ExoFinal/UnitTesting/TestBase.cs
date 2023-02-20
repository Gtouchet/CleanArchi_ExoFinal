namespace UnitTesting;

public class TestBase
{
    private readonly InMemoryRepository repository;
    protected readonly HandlersProcessor HandlersProcessor;
    protected readonly ConsoleEngine ConsoleEngine;

    public TestBase()
	{
        this.repository = new InMemoryRepository();
        Context context = new Context(this.repository);
        this.HandlersProcessor = new HandlersProcessor(context, Logger.GetInstance());
        this.ConsoleEngine = new ConsoleEngine(new AgendaCommandParser(), this.HandlersProcessor);
    }

    public void ClearRepository()
    {
        this.repository.Clear();
    }
}
