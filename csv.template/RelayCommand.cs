using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

namespace scanner.csv
{
    public class RelayCommand : ICommand
    {

        readonly Action _execute;
        readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {

            

            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;

            MessageBox.Show("RelayCommand -> _execute : " + _execute);
            _canExecute = canExecute;

            MessageBox.Show("RelayCommand -> _canExecute : " + _canExecute);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            MessageBox.Show("CanExecute");

            return _canExecute == null || _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }


        public void Execute(object parameter)
        {

            MessageBox.Show("Execute");

            _execute();
        }

        public Action CurrentAction { get { return _execute; } }
    }
}
