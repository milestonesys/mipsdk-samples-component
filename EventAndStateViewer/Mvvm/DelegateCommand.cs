using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAndStateViewer.Mvvm
{
    /// <summary>
    /// Implementation of <see cref="ICommand"/>, supporting both synchronous and asynchronous commands.
    /// </summary>
    class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<Task> _asyncAction;
        private bool _canExecute = true;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public DelegateCommand(Func<Task> asyncAction)
        {
            _asyncAction = asyncAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
                return;
            }

            SetCanExecute(false);
            try
            {
                await _asyncAction();
            }
            finally
            {
                SetCanExecute(true);
            }
        }

        private void SetCanExecute(bool value)
        {
            _canExecute = value;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
