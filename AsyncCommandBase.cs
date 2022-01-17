using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiscLibs
{
public abstract class AsyncCommandBase<T> : ICommand
    {
        protected readonly Func<bool> _canExecute;
        private readonly Action<Exception> _onException;

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged=delegate { };

        public AsyncCommandBase(Action<Exception> onException,Func<bool> canExecute)
        {
            _onException = onException;
            _canExecute = canExecute;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter)
        {
            return !IsExecuting && _canExecute();
        }
        public bool CanExecute()
        {
            return !IsExecuting && _canExecute();
        }

        public async void Executeb(T parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }

            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(T parameter);

        public void Execute(object parameter)
        {
            Executeb((T)parameter);
        }
    }
    public abstract class AsyncCommandBase : ICommand
    {
        protected readonly Func<bool> _canExecute;
        private readonly Action<Exception> _onException;

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged=delegate { };

        public AsyncCommandBase(Action<Exception> onException, Func<bool> canExecute)
        {
            _onException = onException;
            _canExecute = canExecute;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter)
        {
            return !IsExecuting && _canExecute();
        }
        public bool CanExecute()
        {
            return !IsExecuting && _canExecute();
        }

        public async void Execute()
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync();
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }

            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync();

        public void Execute(object parameter)
        {
            Execute();
        }
    }

}
