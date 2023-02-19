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
    private readonly List<CommandBase> commands;
    private readonly List<QueryBase> queries;

    public HandlersProcessor(Context context)
    {
        this.commands = this.InitializeCommands(context);
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
        CommandBase? commandHandler = commands
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
        QueryBase? queryHandler = queries
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
