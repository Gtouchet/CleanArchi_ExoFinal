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

#pragma warning disable CS8602, CS8603, CS8597
public class HandlersProcessor
{
    private readonly List<CommandHandlerBase> commands;
    private readonly List<QueryHandlerBase> queries;

    public HandlersProcessor(
        Context context,
        Logger logger)
    {
        this.commands = this.InitializeCommands(context, logger);
        this.queries = this.InitializeQueries(context);
    }

    /// <summary>
    /// Execute a command
    /// </summary>
    /// <typeparam name="C">The command message type to process</typeparam>
    /// <param name="command">The command to execute</param>
    /// <returns>An object, which type depends on the command to execute</returns>
    /// <exception cref="UnknownHandlerException"></exception>
    public object ExecuteCommand<C>(C command) where C : Command
    {
        CommandHandlerBase? commandHandler = commands
            .FirstOrDefault(c => c.GetType()
                .GetInterfaces()
                .Any(i => i.GetGenericArguments()
                    .Any(a => a == typeof(C))));
        try
        {
            if (commandHandler != null)
            {
                return commandHandler.GetType().GetMethod("Handle").Invoke(commandHandler, new object[] { command });
            }
            else
            {
                throw new UnknownHandlerException(typeof(C));
            }
        }
        catch (TargetInvocationException exception)
        {
            throw exception.InnerException;
        }
    }

    /// <summary>
    /// Execute a query
    /// </summary>
    /// <typeparam name="Q">The query message type to process</typeparam>
    /// <param name="query">The query to execute</param>
    /// <returns>An object, which type depends on the query to execute</returns>
    /// <exception cref="UnknownHandlerException"></exception>
    public object ExecuteQuery<Q>(Q query) where Q : Query
    {
        QueryHandlerBase? queryHandler = queries
            .FirstOrDefault(c => c.GetType()
                .GetInterfaces()
                .Any(i => i.GetGenericArguments()
                    .Any(a => a == typeof(Q))));
        try
        {
            if (queryHandler != null)
            {
                return queryHandler.GetType().GetMethod("Handle").Invoke(queryHandler, new object[] { query });
            }
            else
            {
                throw new UnknownHandlerException(typeof(Q));
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
    private List<CommandHandlerBase> InitializeCommands(Context context, Logger logger)
    {
        List<CommandHandlerBase> commands = new List<CommandHandlerBase>();
        Assembly.GetAssembly(typeof(CommandHandlerBase))!
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(CommandHandlerBase)))
            .ToList()
            .ForEach(t => commands.Add((CommandHandlerBase)Activator.CreateInstance(t, context, logger)!));
        return commands;
    }

    /// <summary>
    /// Initialize the queries container with all the queries handlers found in code
    /// </summary>
    /// <param name="repository">The repository used to retrieve or write the data</param>
    /// <returns>The list of initialized handlers</returns>
    private List<QueryHandlerBase> InitializeQueries(Context context)
    {
        List<QueryHandlerBase> queries = new List<QueryHandlerBase>();
        Assembly.GetAssembly(typeof(QueryHandlerBase))!
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(QueryHandlerBase)))
            .ToList()
            .ForEach(t => queries.Add((QueryHandlerBase)Activator.CreateInstance(t, context)!));
        return queries;
    }
}
