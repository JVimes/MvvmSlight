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
        ///   Sometimes useful in keeping the XAML designer behaving well.
        /// </summary>
        public static bool IsInDesignMode { get; } =
            DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}
