using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSlight
{
    /// <summary>
    ///   Simple way to implement a command handler. If you are not passing a
    ///   command parameter, use <see cref="Command"/> instead. Usually, put a
    ///   property of this type in your ViewModel and bind a control's command
    ///   to it.
    /// </summary>
    /// <typeparam name="T"> Type of the command parameter. </typeparam>
    public class Command<T> : ICommand
    {
        readonly Action<T> execute;
        readonly Func<T, bool> canExecute = (p) => true;

        /// <param name="execute">
        ///   The action to execute when the command is run. It is passed the
        ///   command parameter.
        /// </param>
        /// <param name="canExecute">
        ///   Optional. Tells if the command is currently valid. It is passed
        ///   the command parameter. Can cause parts of the UI to be grayed out.
        /// </param>
        public Command(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? this.canExecute;
        }

        /// <summary>
        ///   Defaults to <see cref="CommandManager.RequerySuggested"/>. See
        ///   <see cref="ICommand.CanExecuteChanged"/>.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary> See <see cref="ICommand.CanExecute(object)"/>. </summary>
        public bool CanExecute(object parameter) => canExecute(ConvertObjectToT(parameter));

        /// <summary> See <see cref="ICommand.Execute(object)"/>. </summary>
        public void Execute(object parameter) => execute(ConvertObjectToT(parameter));

        private T ConvertObjectToT(object parameter)
        {
            if (parameter != null && !(parameter is T))
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                var canConvert = converter.CanConvertFrom(parameter.GetType());
                if (canConvert) return (T)converter.ConvertFrom(parameter);
            }

            return (T)parameter;
        }
    }
}
