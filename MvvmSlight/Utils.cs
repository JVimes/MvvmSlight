using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSlight
{
    public static class Utils
    {
        /// <summary>
        ///   Useful for loading design-time values into an view model classes,
        ///   which keeps Visual Studio's "Go to Definition", etc. working
        ///   correctly in XAML designer.
        /// </summary>
        public static bool IsInDesignMode => isInDesignMode;
        static bool isInDesignMode = DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}
