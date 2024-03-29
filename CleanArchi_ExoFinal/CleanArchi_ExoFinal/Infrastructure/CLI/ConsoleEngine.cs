using CleanArchi_ExoFinal.Application;
using CleanArchi_ExoFinal.Application.Handlers.CommandHandlers;
using CleanArchi_ExoFinal.Application.Handlers.QueryHandlers;
using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Handlers.CommandHandlers;
using CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;

namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public class ConsoleEngine : ConsoleManager
{
    private readonly AgendaCommandParser agendaCommandParser;
    private readonly HandlersProcessor handlersProcessor;

    public ConsoleEngine(
        AgendaCommandParser agendaCommandParser,
        HandlersProcessor handlersProcessor)
    {
        this.handlersProcessor = handlersProcessor;
        this.agendaCommandParser = agendaCommandParser;
    }

    public void Run()
    {
        this.WriteLine("Use 'agenda help' to get a list of commands");

        (CliCommand, string?) userInput = this.GetUserCommand();
        while (!userInput.Item1.Equals(CliCommand.Quit))
        {
            try
            {
                this.ProcessCommand(userInput.Item1, userInput.Item2);
            }
            catch (Exception exception)
            {
                this.WriteLine(exception.Message);
            }
            userInput = this.GetUserCommand();
        }
    }

    private (CliCommand, string?) GetUserCommand()
    {
        this.Write("> ");
        string? input = this.Read();
        if (input != null)
        {
            Enum.TryParse(input.Split(" ")[0], ignoreCase: true, out CliCommand domain);
            return (domain, input);
        }
        else
        {
            return (default, null);
        }
    }

    private void ProcessCommand(CliCommand domain, string? userCommand)
    {
        switch (domain)
        {
           case CliCommand.Agenda: this.InterpretAgendaCommand(userCommand!); break;
           // other commands
           default: throw new UnkownCommandException($"Error, unknown domain");
        }
    }

    private void InterpretAgendaCommand(string userCommand)
    {
        AgendaCommand agendaCommand = this.agendaCommandParser.Parse(userCommand);
        switch (agendaCommand.Command)
        {
            case EAgendaCommand.Help:
                this.WriteLine("list of agenda commands (the arguments order does not matter) :\n" +
                    "create  > agenda create -c:\"description\" -d:yyyy-mm-dd -s:todo\n" +
                    "read    > agenda read -id:guid\n" +
                    "readall > agenda readall\n" +
                    "update  > agenda update -id:guid -c:\"description\" -d:yyyy-mm-dd -s:todo\n" +
                    "delete  > agenda delete -id:guid\n" +
                    "add     > agenda add -id:guid -c:\"description\" -d:yyyy-mm-dd -s:todo\n");
                break;
            case EAgendaCommand.Create:
                Guid id = (Guid)this.handlersProcessor.ExecuteCommand(this.ParseAgendaCommandAs<CreateTaskCommand>(agendaCommand)!);
                this.WriteLine(id.ToString());
                break;
            case EAgendaCommand.Read:
                TaskEntity task = (TaskEntity)this.handlersProcessor.ExecuteQuery(this.ParseAgendaCommandAs<ReadTaskQuery>(agendaCommand)!);
                this.WriteLine(task.ToString());
                break;
            case EAgendaCommand.ReadAll:
                List<TaskEntity> tasks = (List<TaskEntity>)this.handlersProcessor.ExecuteQuery(this.ParseAgendaCommandAs<ReadTasksQuery>(agendaCommand)!);
                tasks.ForEach(task => this.WriteLine(task.ToString()));
                break;
            case EAgendaCommand.Update:
                this.handlersProcessor.ExecuteCommand(this.ParseAgendaCommandAs<UpdateTaskCommand>(agendaCommand)!);
                break;
            case EAgendaCommand.Delete:
                this.handlersProcessor.ExecuteCommand(this.ParseAgendaCommandAs<DeleteTaskCommand>(agendaCommand)!);
                break;
            case EAgendaCommand.Add:
                this.handlersProcessor.ExecuteCommand(this.ParseAgendaCommandAs<AddSubTaskCommand>(agendaCommand)!);
                break;
            default: throw new WrongParametersForCommandException(CommandErrorMessage.CommandNotRecognized);
        }
    }

    /**
     * other commands interpreter methods
     */
    
    private T? ParseAgendaCommandAs<T>(AgendaCommand agendaCommand) where T : Message
    {
        Message? command = typeof(T) switch
        {
            Type type when type.Equals(typeof(CreateTaskCommand)) => new CreateTaskCommand()
            {
                Description = agendaCommand.Description,
                DueDate = agendaCommand.DueDate,
                State = agendaCommand.State,
            },
            Type type when type.Equals(typeof(ReadTaskQuery)) => new ReadTaskQuery()
            {
                Id = agendaCommand.Id!.Value,
            },
            Type type when type.Equals(typeof(ReadTasksQuery)) => new ReadTasksQuery(),
            Type type when type.Equals(typeof(UpdateTaskCommand)) => new UpdateTaskCommand()
            {
                Id = agendaCommand.Id!.Value,
                Description = agendaCommand.Description,
                DueDate = agendaCommand.DueDate,
                State = agendaCommand.State,
            },
            Type type when type.Equals(typeof(DeleteTaskCommand)) => new DeleteTaskCommand()
            {
                Id = agendaCommand.Id!.Value,
            },
            Type type when type.Equals(typeof(AddSubTaskCommand)) => new AddSubTaskCommand()
            {
                Id = agendaCommand.Id!.Value,
                Description = agendaCommand.Description,
                DueDate = agendaCommand.DueDate,
                State = agendaCommand.State,
            },
            _ => throw new UnknownMessageException(typeof(T)),
        };
        return command as T;
    }

    /**
     * other commands parser methods
     */
}
