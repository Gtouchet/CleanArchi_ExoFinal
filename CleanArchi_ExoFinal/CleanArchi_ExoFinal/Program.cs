using CleanArchi_ExoFinal;
using CleanArchi_ExoFinal.Application;
using CleanArchi_ExoFinal.Infrastructure.CLI;
using CleanArchi_ExoFinal.Infrastructure.Repositories;

Bootstrap.Seed();

Context context = new Context(new JsonRepository("tasks.json"));
HandlersProcessor handlersProcessor = new HandlersProcessor(context);

AgendaCommandParser agendaCommandParser = new AgendaCommandParser();
ConsoleEngine consoleEngine = new ConsoleEngine(
    agendaCommandParser,
    handlersProcessor);

consoleEngine.Run();
