using HW_19.Infrastructure.Commands;
using System;

namespace HW_19.Infrastructure
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
