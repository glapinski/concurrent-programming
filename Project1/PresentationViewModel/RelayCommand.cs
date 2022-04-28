using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class RelayCommand : ICommand
    {
        Action<object> _execute;
        Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object,bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object obj)
        {
            _execute(obj);
        }

        public bool CanExecute(object obj)
        {
            if (_canExecute != null)
            {
                return _canExecute(obj);
            }
            return false;
        }
    }
}
