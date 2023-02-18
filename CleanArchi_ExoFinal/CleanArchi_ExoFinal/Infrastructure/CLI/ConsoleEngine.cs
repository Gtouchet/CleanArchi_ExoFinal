using CleanArchi_ExoFinal.Application;
using CleanArchi_ExoFinal.Application.Handlers.QueryHandlers;
using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Handlers.CommandHandlers;
using CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;

namespace CleanArchi_ExoFinal.Infrastructure.CLI;


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
        EDomain userDomain = Utils.ParseStringAs<EDomain>(userInput.Split(" ")[0]);
        while (!userDomain.Equals(EDomain.quit))
        {
            try
            {
                this.InterpretCommand(userDomain, userInput);
            }
            catch (Exception exception)
            {
                this.WriteLine(exception.Message);
            }
            userInput = this.GetUserInput();
            userDomain = Utils.ParseStringAs<EDomain>(userInput.Split(" ")[0]);
        }
    }

    private string GetUserInput()
    {
        this.Write("> ");
        return this.Read() ?? string.Empty;
    }

    private void InterpretCommand(EDomain domain, string args)
    {
        switch (domain)
        {
           case EDomain.agenda:
               AgendaCommand agendaCommand = AgendaCommandParser.Parse(args);
               switch (agendaCommand.Command)
               {
                   case ECommand.create: // > create, description, 7, done
                       this.handlersProcessor.Execute(this.ParseUserInputAs<CreateTaskCommand>(agendaCommand)!);
                       break;
                   case ECommand.read: // > read, guid
                       TaskEntity task = (TaskEntity)this.handlersProcessor.Execute(this.ParseUserInputAs<ReadTaskQuery>(agendaCommand)!);
                       this.WriteLine(task.ToString());
                       break;
                   case ECommand.readall: // > readall
                       List<TaskEntity> tasks = (List<TaskEntity>)this.handlersProcessor.Execute(this.ParseUserInputAs<ReadTasksQuery>(agendaCommand)!);
                       tasks.ForEach(task => this.WriteLine(task.ToString()));
                       break;
                   case ECommand.delete: // > delete, guid
                       this.handlersProcessor.Execute(this.ParseUserInputAs<DeleteTaskCommand>(agendaCommand)!);
                       break;
                   case ECommand.add:
                      // TODO add subtask
                       break;
                   case ECommand.update:
                       // TODO update task
                       break;
                   default: throw new Exception(""); // TODO
               }
               break;
           default: throw new Exception(""); // TODO
        }
;
    }

    private T? ParseUserInputAs<T>(AgendaCommand agendaCommand) where T : Message
    {
        Message? command = typeof(T) switch
        {
            Type type when type.Equals(typeof(CreateTaskCommand)) => new CreateTaskCommand()
            {
                Description = agendaCommand.Content,
                DueDate = agendaCommand.DueDate,
                State = agendaCommand.Status,
            },
            Type type when type.Equals(typeof(ReadTaskQuery)) => new ReadTaskQuery()
            {
                Id = agendaCommand.Id.ToString(),
            },
            Type type when type.Equals(typeof(ReadTasksQuery)) => new ReadTasksQuery(),
            Type type when type.Equals(typeof(DeleteTaskCommand)) => new DeleteTaskCommand()
            {
                Id = agendaCommand.Id.ToString(),
            },
            _ => throw new Exception("C'est quoi ce message ?"), // TODO
        };
        return command as T;
    }
}
