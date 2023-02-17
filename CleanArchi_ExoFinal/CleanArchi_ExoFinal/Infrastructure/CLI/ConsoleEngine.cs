using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Handlers;
using CleanArchi_ExoFinal.Handlers.CommandHandlers;

namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public enum ECommand
{
    unknown,
    quit,
    create,
}

public class ConsoleEngine : ConsoleManager
{
    public void Run()
    {
        this.WriteLine("bonjour TODO tuto comment ca marche ?");
        this.Write("> ");
        string userInput = this.Read() ?? string.Empty;

        ECommand userCommand = Utils.ParseStringAs<ECommand>(userInput.Split(",")[0]);
        while (!userCommand.Equals(ECommand.quit))
        {
            string[] args = userInput.Split(",");

            switch (userCommand)
            {
                case ECommand.create: // create, myTask, 7, done
                    if (args.Length != 4) continue;
                    new CreateTaskCommandHandler(new JsonParser("tasks.json")).Handle(this.ParseUserInputAs<CreateTaskCommand>(args)!);
                    break;
                default: this.WriteLine("dats a nono"); break;
            };

            this.Write("> ");
            userInput = this.Read() ?? string.Empty;
        }
    }

    private T? ParseUserInputAs<T>(string[] args) where T : Command
    {
        Command? command = typeof(T) switch
        {
            Type type when type.Equals(typeof(CreateTaskCommand)) => new CreateTaskCommand()
            {
                Description = args[1],
                DueDate = this.ParseDate(args[2]),
                State = Utils.ParseStringAs<State>(args[3]),
            },
            _ => null,
        };
        return command as T;
    }

    private DateTimeOffset ParseDate(string date)
    {
        return DateTimeOffset.Now; // TODO
    }
}
