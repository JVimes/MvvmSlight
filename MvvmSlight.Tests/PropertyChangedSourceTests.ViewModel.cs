namespace MvvmSlight.Tests
{
    public partial class PropertyChangedSourceTests
    {
        class ViewModel : PropertyChangedSource
        {
            string theProperty;
            public string TheProperty
            {
                get => theProperty;
                set => SetResult = Set(ref theProperty, value);
            }

            public string PropertyWithManuallyRaisedEvent
            {
                get => null;
                set => RaisePropertyChanged();
            }

            public bool SetResult { get; private set; }
            internal string ThePropertyBackingField => theProperty;
        }
    }
}
