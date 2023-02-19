using System.Text.RegularExpressions;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;
namespace CleanArchi_ExoFinal.Infrastructure.CLI;

public class AgendaCommandParser
{
    public AgendaCommand Parse(string command)
    {
        var result = new AgendaCommand();
        if (string.IsNullOrWhiteSpace(command) || command.Length < 7)
            throw new WrongParametersForCommandException(CommandErrorMessage.NotEnoughArguments);
        
        var updatedString = command.Remove(0, 7);
        
        var parts = updatedString.Split(' ').ToList();
        if (parts.Count == 0)
            throw new WrongParametersForCommandException(CommandErrorMessage.NotEnoughArguments);
        
        if (Enum.TryParse(parts[0], ignoreCase: true, out EAgendaCommand action))
            result.Command = action;
        else
            throw new WrongParametersForCommandException(CommandErrorMessage.CommandNotRecognized);

        if (result.Command == EAgendaCommand.ReadAll) return result;
        
        updatedString = updatedString.Remove(0, parts[0].Length);
        parts.RemoveAt(0);
        if (result.Command != EAgendaCommand.ReadAll && parts.Count < 1)
            throw new WrongParametersForCommandException(CommandErrorMessage.NotEnoughArguments);
        
        // si plus rien == Read or ReadAll
        if (parts.Count == 0) return result;
        
        return ParseOptions(result, updatedString);
    }
    
    private AgendaCommand ParseOptions(AgendaCommand agendaCommand, string input)
    {
        string patternDueDate = @"-d:\d{4}-\d{2}-\d{2}";
        string patternDescription = @"-c:""(.+?)""";
        string patternState = @"-s:\w+";
        string patternId = @"-id:[A-F0-9]{8}-([A-F0-9]{4}-){3}[A-F0-9]{12}";

        agendaCommand.DueDate = Regex.Match(input, patternDueDate).Value != "" ? DateTime.Parse(Regex.Match(input, patternDueDate).Value.Remove(0, 3)) : null;
        agendaCommand.Description = Regex.Match(input, patternDescription)?.Groups[1].Value;
        agendaCommand.State = Regex.Match(input, patternState)?.Value != "" ? (State)Enum.Parse(typeof(State), Regex.Match(input, patternState).Value.Remove(0, 3), ignoreCase: true) : default(State);

        Console.WriteLine(Regex.Match(input, patternId).Value.Remove(0, 4));
        
        agendaCommand.Id = Regex.Match(input, patternId)?.Value != "" ? Guid.Parse(Regex.Match(input, patternId).Value.Remove(0, 4)) : null;

        return agendaCommand;
    }
}