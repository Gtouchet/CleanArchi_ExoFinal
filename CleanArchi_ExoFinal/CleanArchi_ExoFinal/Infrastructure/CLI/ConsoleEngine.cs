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

        ECommand userCommand = Utils.ParseStringAs<ECommand>(userInput.Split(" ")[0]);
        while (!userCommand.Equals(ECommand.quit))
        {
            Command? command = userCommand switch
            {
                ECommand.create => this.ParseUserInputAs<CreateTaskCommand>(userInput),
                _ => null,
            };
        }
    }

    private Command ParseUserInputAs<T>(string userInput) where T : Command
    {
        // TODO
        return null;
    }
}
