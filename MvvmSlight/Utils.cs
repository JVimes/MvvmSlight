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
        ///   Tells if the code is running in the XAML designer. Useful for
        ///   supplying design-time values in the actual view-model, or to
        ///   bypass code that doesn't work in XAML designer.
        /// </summary>
        public static bool IsInDesignMode { get; } =
            DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}
