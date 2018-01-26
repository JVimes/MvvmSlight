using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSlight
{
    /// <summary>
    ///   <see cref="INotifyPropertyChanged"/> implementation for use as a base
    ///   class.
    /// </summary>
    public abstract class PropertyChangeSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Call this inside the setter of each property that should fire the
        ///   <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T"> The type of the property. </typeparam>
        /// <param name="field"> A ref to the backing field. </param>
        /// <param name="value"> The value to set the property to. </param>
        /// <param name="propertyName">
        ///   Leave blank to use the property's name, or pass a string to
        ///   manually specify.
        /// </param>
        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
