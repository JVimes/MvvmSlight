using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSlight
{
    /// <summary>
    ///   Simple ICommand implementation. Usually, bind to instances of this in
    ///   the ViewModel.
    /// </summary>
    public class Command : ICommand
    {
        readonly Action<object> execute;
        readonly Func<object, bool> canExecute = (p) => true;
        
        /// <param name="execute">
        ///   The action to execute when the command is run.
        /// </param>
        /// <param name="canExecute">
        ///   Optional. A <see cref="Func{T, TResult}"/> that tells if the
        ///   command is currently valid. Can cause parts of the UI to be
        ///   disabled (grayed out).
        /// </param>
        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? this.canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute == null) return;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute == null) return;
                CommandManager.RequerySuggested -= value;
            }
        }

        bool ICommand.CanExecute(object parameter) => canExecute(parameter);
        void ICommand.Execute(object parameter) => execute(parameter);
    }
}
