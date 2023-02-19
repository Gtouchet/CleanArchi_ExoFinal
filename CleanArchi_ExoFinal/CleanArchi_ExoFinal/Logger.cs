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
    
    public void Log(string message)
    {
        File.AppendAllText("logs.txt", $"{DateTimeOffset.Now.ToString("[dd/mm/yyyy - hh:mm:ss]")} - {message}\n");
    }
}
