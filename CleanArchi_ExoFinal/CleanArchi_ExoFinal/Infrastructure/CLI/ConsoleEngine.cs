using CleanArchi_ExoFinal.Application;
using CleanArchi_ExoFinal.Application.Handlers.QueryHandlers;
using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Handlers.CommandHandlers;

namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public enum ECommand
{
    unknown,
    create,
    read,
    readall,
    // updatestate,
    delete,
    quit,
}

// TODO: séparateur espace
// description entre ""
public class ConsoleEngine : ConsoleManager
{
    private readonly HandlersProcessor handlersProcessor;

    public ConsoleEngine(HandlersProcessor handlersProcessor)
    {
        this.handlersProcessor = handlersProcessor;
    }

    public void Run()
    {
        this.WriteLine("bonjour TODO tuto comment ca marche ?");

        string userInput = this.GetUserInput();
        ECommand userCommand = Utils.ParseStringAs<ECommand>(userInput.Split(",")[0]);
        while (!userCommand.Equals(ECommand.quit))
        {
            string[] args = this.FormatUserInput(userInput);
            try
            {
                this.InterpretCommand(userCommand, args);
            }
            catch (Exception exception)
            {
                this.WriteLine(exception.Message);
            }
            userInput = this.GetUserInput();
            userCommand = Utils.ParseStringAs<ECommand>(userInput.Split(",")[0]);
        }
    }

    private string GetUserInput()
    {
        this.Write("> ");
        return this.Read() ?? string.Empty;
    }

    private string[] FormatUserInput(string userInput)
    {
        return userInput
            .Split(",")
            .Select(i => i.Trim())
            .ToArray();
    }

    private void InterpretCommand(ECommand command, string[] args)
    {
        switch (command)
        {
            case ECommand.create: // > create, description, 7, done
                if (args.Length != 4) throw new WrongParametersForCommandException(CommandErrorMessage.create);
                this.handlersProcessor.Execute(this.ParseUserInputAs<CreateTaskCommand>(args)!);
                break;
            case ECommand.read: // > read, guid
                if (args.Length != 2) throw new WrongParametersForCommandException(CommandErrorMessage.read);
                TaskEntity task = (TaskEntity)this.handlersProcessor.Execute(this.ParseUserInputAs<ReadTaskQuery>(args)!);
                this.WriteLine(task.ToString());
                break;
            case ECommand.readall: // > readall
                if (args.Length != 1) throw new WrongParametersForCommandException(CommandErrorMessage.readall);
                List<TaskEntity> tasks = (List<TaskEntity>)this.handlersProcessor.Execute(this.ParseUserInputAs<ReadTasksQuery>(args)!);
                tasks.ForEach(task => this.WriteLine(task.ToString()));
                break;
            case ECommand.delete: // > delete, guid
                if (args.Length != 2) throw new WrongParametersForCommandException(CommandErrorMessage.delete);
                this.handlersProcessor.Execute(this.ParseUserInputAs<DeleteTaskCommand>(args)!);
                break;
            default: throw new Exception(""); // TODO
        };
    }

    private T? ParseUserInputAs<T>(string[] args) where T : Message
    {
        Message? command = typeof(T) switch
        {
            Type type when type.Equals(typeof(CreateTaskCommand)) => new CreateTaskCommand()
            {
                Description = args[1],
                DueDate = this.ParseDate(args[2]),
                State = Utils.ParseStringAs<State>(args[3]),
            },
            Type type when type.Equals(typeof(ReadTaskQuery)) => new ReadTaskQuery()
            {
                Id = args[1],
            },
            Type type when type.Equals(typeof(ReadTasksQuery)) => new ReadTasksQuery(),
            Type type when type.Equals(typeof(DeleteTaskCommand)) => new DeleteTaskCommand()
            {
                Id = args[1],
            },
            _ => throw new Exception("C'est quoi ce message ?"), // TODO
        };
        return command as T;
    }

    private DateTimeOffset ParseDate(string date)
    {
        if (int.TryParse(date, out int dueDateInDays))
        {
            if (dueDateInDays <= 0)
            {
                throw new Exception(""); // TODO
            }
            return DateTimeOffset.Now.AddDays(dueDateInDays);
        }
        else
        {
            throw new Exception(""); // TODO
        }
    }
}
