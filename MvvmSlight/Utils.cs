using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSlight
{
    /// <summary> </summary>
    public static class Utils
    {
        /// <summary> Tells if running in the XAML designer </summary>
        public static bool IsInDesigner { get; } =
            DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}
