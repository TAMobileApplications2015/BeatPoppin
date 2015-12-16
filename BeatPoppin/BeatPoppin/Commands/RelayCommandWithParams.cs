namespace BeatPoppin.Commands
{
    using System;
    using System.Windows.Input;

    public class RelayCommandWithParams : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommandWithParams(Action<object> execute,
                            Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
