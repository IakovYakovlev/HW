using System.Windows;

namespace HW_17.Infrastructure.Commands
{
    internal class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = (Window)parameter;
            window?.Close();
        }
    }

    internal class CloseDialogCommand : Command
    {
        public bool? DialogResult { get; set; }

        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var w = (Window)parameter;
            w.DialogResult = DialogResult;
            w?.Close();
        }
    }
}
