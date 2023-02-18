using CleanArchi_ExoFinal.Application.Kernel;
using CleanArchi_ExoFinal.Infrastructure.Repositories;
using System.Reflection;

namespace CleanArchi_ExoFinal.Application;

public class UnknownHandlerException : Exception
{
    public UnknownHandlerException(Type type) : base($"Error, unknown handler type {type.Name}") { }
}

public class HandlerException : Exception
{
    public HandlerException(string message) : base(message) { }
}

public class HandlersProcessor
{
    private readonly List<CommandBase> commands;
    private readonly List<QueryBase> queries;

    public HandlersProcessor(Context context)
    {
        this.commands = this.InitializeCommands(context);
        this.queries = this.InitializeQueries(context);
    }

    /// <summary>
    /// Execute a specific handler (command or query) depending on the type of message to execute
    /// </summary>
    /// <typeparam name="T">The type of the message that the container has to execute</typeparam>
    /// <param name="message">The message to execute</param>
#pragma warning disable CS8602, CS8603, CS8597
    public object Execute<T>(T message) where T : Message
    {
        CommandBase? commandHandler = commands
            .FirstOrDefault(c => c.GetType()
                .GetInterfaces()
                .Any(i => i.GetGenericArguments()
                    .Any(a => a == typeof(T))));

        QueryBase? queryHandler = queries
            .FirstOrDefault(c => c.GetType()
                .GetInterfaces()
                .Any(i => i.GetGenericArguments()
                    .Any(a => a == typeof(T))));

        try
        {
            if (commandHandler != null)
            {
                return commandHandler.GetType().GetMethod("Handle").Invoke(commandHandler, new object[] { message });
            }
            else if (queryHandler != null)
            {
                return queryHandler.GetType().GetMethod("Handle").Invoke(queryHandler, new object[] { message });
            }
            else
            {
                throw new UnknownHandlerException(typeof(T));
            }
        }
        catch (TargetInvocationException exception)
        {
            throw exception.InnerException;
        }
    }

    /// <summary>
    /// Initialize the commands container with all the commands handlers found in code
    /// </summary>
    /// <param name="repository">The repository used to retrieve or write the data</param>
    /// <returns>The list of initialized handlers</returns>
    private List<CommandBase> InitializeCommands(Context context)
    {
        List<CommandBase> commands = new List<CommandBase>();
        Assembly.GetAssembly(typeof(CommandBase))!
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(CommandBase)))
            .ToList()
            .ForEach(t => commands.Add((CommandBase)Activator.CreateInstance(t, context)!));
        return commands;
    }

    /// <summary>
    /// Initialize the queries container with all the queries handlers found in code
    /// </summary>
    /// <param name="repository">The repository used to retrieve or write the data</param>
    /// <returns>The list of initialized handlers</returns>
    private List<QueryBase> InitializeQueries(Context context)
    {
        List<QueryBase> queries = new List<QueryBase>();
        Assembly.GetAssembly(typeof(QueryBase))!
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(QueryBase)))
            .ToList()
            .ForEach(t => queries.Add((QueryBase)Activator.CreateInstance(t, context)!));
        return queries;
    }
}
