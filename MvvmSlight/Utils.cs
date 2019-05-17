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
        public static bool RunningInDesigner { get; } =
            DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}
