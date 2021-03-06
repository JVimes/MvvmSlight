﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSlight
{
    /// <summary>
    ///   Simple way to implement a command handler. Usually, put a property of
    ///   this type in your ViewModel and bind a control's command to it. If
    ///   you are passing a command parameter, use <see cref="Command{T}"/>
    ///   instead.
    /// </summary>
    public class Command : Command<object>
    {
        /// <param name="execute">
        ///   The action to execute when the command is run.
        /// </param>
        /// <param name="canExecute">
        ///   Optional. Returns if the command is currently valid. Can cause
        ///   the control to be disabled.
        /// </param>
        public Command(Action execute, Func<bool>? canExecute = null)
            : base(parameter => execute(),
                  parameter => canExecute == null ? true : canExecute())
        { }
    }
}
