using CleanArchi_ExoFinal;
using CleanArchi_ExoFinal.Application;
using CleanArchi_ExoFinal.Infrastructure.CLI;
using CleanArchi_ExoFinal.Infrastructure.Repositories;

Bootstrap.Seed();

Context context = new Context(new JsonRepository("tasks.json"));
HandlersProcessor handlersProcessor = new HandlersProcessor(context);

new ConsoleEngine(handlersProcessor).Run();
