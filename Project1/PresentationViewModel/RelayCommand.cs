using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    internal class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        public RelayCommand(Action execute) : this(execute, null) { }

        public virtual void Execute(object obj)
        {
            this._execute();
        }

        public bool CanExecute(object obj)
        {
            if (this._canExecute == null)
            {
                return true;
            }
            if (obj == null)
            {
                return this._canExecute();
            }
            return this._canExecute();
        }
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
