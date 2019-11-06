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
    ///   A base class for view-model and model classes. Reduces the boilerplate
    ///   usually required to implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class PropertyChangedSource : INotifyPropertyChanged
    {
        /// <summary>
        ///   See <see cref="INotifyPropertyChanged.PropertyChanged"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary> 
        ///   A call to this takes the place of the boilerplate traditionally
        ///   used in a property setter to fire <see
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
        /// <param name="propertyName"> 
        ///   Usually don't pass this. Default is the name of the property whose
        ///   setter is being called. This specifies the property name that the
        ///   <see cref="PropertyChanged"/> event will contain.
        /// </param>
        /// <returns> 
        ///   True if the value changed (and event fired), else false. Useful
        ///   when you want to bypass code when the value didn't change.
        /// </returns>
        protected bool Set<T>(ref T field,
                              T value,
                              [CallerMemberName] string propertyName = null)
        {
            if (AreEqual(field, value)) return false;

            field = value;
            RaisePropertychanged(propertyName);
            return true;
        }

        /// <summary> 
        ///   A call to this takes the place of the boilerplate traditionally
        ///   used in a property setter to fire <see
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
        /// <param name="propertyName"> 
        ///   Usually don't pass this. Default is the name of the property whose
        ///   setter is being called. This specifies the property name that the
        ///   <see cref="PropertyChanged"/> event will contain.
        /// </param>
        /// <returns> 
        ///   True if the value changed (and event fired), else false. Useful
        ///   when you want to bypass code when the value didn't change.
        /// </returns>
        protected bool Set<T>(object backingObject,
                              string backingPropertyName,
                              T value,
                              [CallerMemberName] string propertyName = null)
        {
            if (backingObject is null)
                throw new ArgumentNullException(nameof(backingObject));

            var property = backingObject.GetType().GetProperty(backingPropertyName);
            if (property == null)
                throw new ArgumentException(
                    $"{nameof(backingPropertyName)}, \"{backingPropertyName}\", not found on {nameof(backingObject)}.");

            var backingValue = property.GetValue(backingObject);
            if (AreEqual(backingValue, value)) return false;

            property.SetValue(backingObject, value, null);
            RaisePropertychanged(propertyName);
            return true;
        }

        /// <summary>
        ///   Useful when the Set methods are inadequate. This lets you raise
        ///   the PropertyChanged event manually. Calling this within a
        ///   property's setter ensures the setter's code coincides with the
        ///   <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">
        ///   Usually don't pass this. Default is the name of the property
        ///   whose setter is being called. This specifies the property name
        ///   that the <see cref="PropertyChanged"/> event will contain.
        /// </param>
        protected void RaisePropertychanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        ///   Checks equality the same way the Set methods do.
        /// </summary>
        protected static bool AreEqual<T>(T value1, T value2)
            => EqualityComparer<T>.Default.Equals(value1, value2);
    }
}
