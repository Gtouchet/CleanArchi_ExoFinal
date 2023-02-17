namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public abstract class ConsoleManager
{
    protected void WriteLine(string message) => Console.WriteLine(message);
    protected void Write(string message) => Console.Write(message);
    protected string? Read() => Console.ReadLine();
}
