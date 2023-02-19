namespace UnitTesting;

public class TestBase
{
    protected readonly HandlersProcessor HandlersProcessor;

    public TestBase()
	{
        Context context = new Context(new InMemoryRepository());
        this.HandlersProcessor = new HandlersProcessor(context, Logger.GetInstance());
    }
}
