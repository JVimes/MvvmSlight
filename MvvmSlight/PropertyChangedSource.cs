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
        ///   Takes the place of the boilerplate traditionally used to fire <see
        ///   cref="INotifyPropertyChanged.PropertyChanged"/> events. Use this
        ///   overload when the property is backed by a field.
        /// </summary>
        /// <typeparam name="T">
        ///   You generally don't have to specify this. C# infers it from the
        ///   <paramref name="value"/> parameter.
        /// </typeparam>
        /// <param name="field"> A ref to the backing field. </param>
        /// <param name="value">
        ///   The value to set the backing field to.
        /// </param>
        /// <param name="OnComplete"> Optional callback. </param>
        /// <param name="propertyName">
        ///   Usually leave blank (to use the name of the property whose setter
        ///   is being called). The property name that will be fired by the <see
        ///   cref="PropertyChanged"/> event.
        /// </param>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (AreEqual(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///   Takes the place of the boilerplate traditionally used to fire <see
        ///   cref="INotifyPropertyChanged.PropertyChanged"/> events. Use this
        ///   overload when the property is backed by another property,
        ///   potentially on another object.
        /// </summary>
        /// <typeparam name="T">
        ///   You generally don't have to specify this. C# infers it from the
        ///   <paramref name="value"/> parameter.
        /// </typeparam>
        /// <param name="backingObject">
        ///   The object that the backing property is on.
        /// </param>
        /// <param name="backingPropertyName">
        ///   The name of the backing property. Use C#'s "nameof" keyword at
        ///   call-site, if possible.
        /// </param>
        /// <param name="value"> The value to set the property to. </param>
        /// <param name="OnComplete"> Optional callback. </param>
        /// <param name="propertyName">
        ///   Usually leave blank (to use the name of the property whose setter
        ///   is being called). The property name that will be fired by the <see
        ///   cref="PropertyChanged"/> event.
        /// </param>
        protected bool Set<T>(object backingObject, string backingPropertyName,
            T value, [CallerMemberName] string propertyName = null)
        {
            var property = backingObject.GetType().GetProperty(backingPropertyName);
            if (property == null) throw new ArgumentException(
                $"{nameof(backingPropertyName)}, \"{backingPropertyName}\", not found on {nameof(backingObject)}.");
            var backingValue = property.GetValue(backingObject);

            if (AreEqual(backingValue, value)) return false;
            property.SetValue(backingObject, value, null);
            OnPropertyChanged(propertyName);
            return true;
        }

        static bool AreEqual<T>(T field, T value) => EqualityComparer<T>.Default.Equals(field, value);

        void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
