using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODIN
{
    public class AsyncRelayCommand<T> : AsyncCommandBase<T>
    {
        private readonly Func<T,Task> _callbackwparam;

        public AsyncRelayCommand(Func<T,Task> callback, Func<bool> canExecute, Action<Exception> onException) : base(onException, canExecute)
        {
            
            _callbackwparam = callback;
        }

        protected override async Task ExecuteAsync(T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException();
            }
            await _callbackwparam(parameter);
        }
    }
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public AsyncRelayCommand(Func<Task> callback, Func<bool> canExecute, Action<Exception> onException) : base(onException, canExecute)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync()
        {
            await _callback();
        }
    }
}
