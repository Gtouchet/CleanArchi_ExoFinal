using System.Text.RegularExpressions;
using CleanArchi_ExoFinal.Domain;
using CleanArchi_ExoFinal.Infrastructure.CLI.jeSaisPasOuLesMettre;
namespace CleanArchi_ExoFinal.Infrastructure.CLI;

// TODO elle est static est je suis pas focément convaincu du concepte
public static class AgendaCommandParser
{
    public static AgendaCommand Parse(string command)
    {
        var result = new AgendaCommand();
        if (string.IsNullOrWhiteSpace(command) || command.Length < 7)
            throw new ArgumentException("Invalid command string : " + command);
        
        var updatedString = command.Remove(0, 7);
        
        var parts = updatedString.Split(' ').ToList();
        if (parts.Count == 0)
            throw new ArgumentException("Invalid command string : " + command);
        
        if (Enum.TryParse<ECommand>(parts[0], true, out var action))
            result.Command = action;
        else
            throw new ArgumentException("Invalid command command : " + command);

        if ( result.Command == ECommand.readall) return result;
        
        updatedString = updatedString.Remove(0, parts[0].Length);
        parts.RemoveAt(0);
        if (result.Command != ECommand.readall && parts.Count < 1)
            throw new ArgumentException("Invalid command arguments : " + command);

        if (int.TryParse(parts[0], out int id))
        {
            result.Id = id;
            updatedString = updatedString.Remove(0, parts[0].Length);
            parts.RemoveAt(0);
        }
        
        // si plus rien == readOne or readAll
        if (parts.Count == 0) return result;
        
        return ParseOptions(result, updatedString);
    }
    
    private static AgendaCommand ParseOptions(AgendaCommand agendaCommand, string input)
    {
        var pattern1 = @"-d:\d{4}-\d{2}-\d{2}";
        var pattern2 = @"-c:""(.+?)""";
        var pattern3 = @"-s:\w+";

        agendaCommand.DueDate = Regex.Match(input, pattern1).Value != "" ? DateTime.Parse(Regex.Match(input, pattern1).Value.Remove(0,3)) : null;
        agendaCommand.Content = Regex.Match(input, pattern2)?.Groups[1].Value;
        agendaCommand.Status = Regex.Match(input, pattern3)?.Value != "" ? (State) Enum.Parse(typeof(State), Regex.Match(input, pattern3).Value.Remove(0,3)) : default(State);

        return agendaCommand;
    }
}