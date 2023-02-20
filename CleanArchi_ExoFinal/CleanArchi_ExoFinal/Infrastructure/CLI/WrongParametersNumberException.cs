using System.ComponentModel;

namespace CleanArchi_ExoFinal.Infrastructure.CLI
{
    public enum CommandErrorMessage
    {
        [Description("unrecognized command")]
        CommandNotRecognized,
        [Description("not enough arguments")]
        NotEnoughArguments,
    }

    public class WrongParametersForCommandException : Exception
    {
        public WrongParametersForCommandException(CommandErrorMessage command) : base($"Error, {Utils.GetEnumDescription(command)}") { }
    }
}
