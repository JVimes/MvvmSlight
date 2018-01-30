using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSlight
{
    /// <summary>
    ///   Simple way to implement a command handler. If you are not passing a
    ///   command parameter, use <see cref="Command"/> instead. Usually, put an
    ///   instance of this in your ViewModel and bind a control's command to the
    ///   instance.
    ///
    ///   This class has a generic parameter so that execute/canExecute do not
    ///   have to convert the command parameter from <see cref="object"/> to the
    ///   desired type.
    /// </summary>
    /// <typeparam name="T"> Type of the command parameter. </typeparam>
    public class Command<T> : ICommand
    {
        readonly Action<T> execute;
        readonly Func<T, bool> canExecute = (p) => true;

        /// <param name="execute">
        ///   The action to execute when the command is run. Is passed the
        ///   command parameter.
        /// </param>
        /// <param name="canExecute">
        ///   Optional. Tells if the command is currently valid. Is passed the
        ///   command parameter. Can cause parts of the UI to be grayed out.
        /// </param>
        public Command(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? this.canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => canExecute(ConvertObjectToT(parameter));
        public void Execute(object parameter) => execute(ConvertObjectToT(parameter));

        private T ConvertObjectToT(object parameter)
        {
            var wrongTypeButConvertible = parameter != null && !(parameter is T) && parameter is IConvertible;
            if (wrongTypeButConvertible)
                return (T)Convert.ChangeType(parameter, typeof(T), null);

            return  (T)parameter;
        }
    }
}
