using System;
using App.Domain.Interfaces.Commands;
using FluentValidation.Results;

namespace App.Domain.Commands
{
    public abstract class Command : ICommand
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        public int ExpectedVersion { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
        public abstract bool Execute();
    }
}
