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
    public abstract class PropertyChangedSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   For <see cref="PropertyChanged"/> firing properties with a
        ///   traditional backing field, call this inside the setter.
        /// </summary>
        /// <typeparam name="T">
        ///   C# infers this from the <paramref name="value"/> parameter, so you
        ///   don't have to pass it.
        /// </typeparam>
        /// <param name="field"> A ref to the backing field. </param>
        /// <param name="value"> The value to set the property to. </param>
        /// <param name="propertyName">
        ///   Usually leave blank (to use the name of the property whose setter
        ///   is being called). The property name that will be fired by the <see
        ///   cref="PropertyChanged"/> event.
        /// </param>
        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (AreEqual(field, value)) return;

            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///   For <see cref="PropertyChanged"/> firing properties backed by
        ///   another property, potentially on another object.
        /// </summary>
        /// <typeparam name="T">
        ///   C# infers this from the <paramref name="value"/> parameter, so you
        ///   don't have to pass it.
        /// </typeparam>
        /// <param name="backingObject">
        ///   The object that the backing property is on.
        /// </param>
        /// <param name="backingPropertyName">
        ///   The name of the backing property. Use C#'s "nameof" keyword at
        ///   call-site, if possible.
        /// </param>
        /// <param name="value"> The value to set the property to. </param>
        /// <param name="propertyName">
        ///   Usually leave blank (to use the name of the property whose setter
        ///   is being called). The property name that will be fired by the <see
        ///   cref="PropertyChanged"/> event.
        /// </param>
        protected void Set<T>(object backingObject, string backingPropertyName,
            T value, [CallerMemberName] string propertyName = null)
        {
            var property = backingObject.GetType().GetProperty(backingPropertyName);
            if(property == null) throw new ArgumentException(
                $"{nameof(backingPropertyName)}, \"{backingPropertyName}\", not found on {nameof(backingObject)}.");

            if (AreEqual(property.GetValue(backingObject), value)) return;

            property.SetValue(backingObject, value, null);
            OnPropertyChanged(propertyName);
        }

        private static bool AreEqual<T>(T field, T value) =>
            EqualityComparer<T>.Default.Equals(field, value);

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
