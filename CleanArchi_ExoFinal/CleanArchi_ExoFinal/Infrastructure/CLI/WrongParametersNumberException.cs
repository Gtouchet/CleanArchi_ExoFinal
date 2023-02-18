﻿using System.ComponentModel;

namespace CleanArchi_ExoFinal.Infrastructure.CLI
{
    public enum CommandErrorMessage
    {
        [Description("create command needs 4 arguments : create, description, dueDate, state")]
        create,
        [Description("read command needs 2 arguments : read, guid")]
        read,
        [Description("readall command needs 1 argument : readall")]
        readall,
        [Description("delete command needs 2 arguments : delete, guid")]
        delete,
    }

    public class WrongParametersForCommandException : Exception
    {
        public WrongParametersForCommandException(CommandErrorMessage command) : base($"Error, {Utils.GetEnumDescription(command)}") { }
    }
}