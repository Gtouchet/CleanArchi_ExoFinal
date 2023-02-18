namespace CleanArchi_ExoFinal;

public sealed class Logger
{
    private static Logger? logger;

    public static Logger GetInstance()
    {
        if (logger == null)
        {
            logger = new Logger();
        }
        return logger;
    }

    // TODO: écrire dans un logs.txt
    public void Log(string message)
    {
        Console.WriteLine($"{DateTimeOffset.Now.ToString("[dd/mm/yyyy - hh:mm:ss]")} - {message}");
    }
}
