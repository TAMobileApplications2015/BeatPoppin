﻿namespace BeatPoppin.Commands
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action execute;
        private Func<bool> canExecute;

        public RelayCommand(Action execute,
                            Func<bool> canExecute = null)
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

            return this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }
    }
}
