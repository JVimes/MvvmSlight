using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvvmSlight.Tests
{
    [TestClass]
    public class PropertyChangedSourceTests
    {
        class FakeViewModel : PropertyChangedSource
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


        [TestMethod]
        public void Set_ChangesBackingField()
        {
            var viewModel = new FakeViewModel();
            var newValue = "NewValue";

            viewModel.TheProperty = newValue;

            Assert.AreEqual(newValue, viewModel.ThePropertyBackingField);
        }

        [TestMethod]
        public void Set_FiresEventOnChange()
        {
            var viewModel = new FakeViewModel();
            string nameOfPropertyRaised = null;
            viewModel.PropertyChanged += (s, e) => nameOfPropertyRaised = e.PropertyName;

            viewModel.TheProperty = "NewValue";

            Assert.AreEqual(nameof(FakeViewModel.TheProperty), nameOfPropertyRaised);
        }

        [TestMethod]
        public void Set_DoesNotFireEventOnNoChange()
        {
            var viewModel = new FakeViewModel();
            viewModel.TheProperty = "NewValue";
            bool propertyWasRaised = false;
            viewModel.PropertyChanged += (s, e) => propertyWasRaised = true;

            viewModel.TheProperty = "NewValue";

            Assert.IsFalse(propertyWasRaised);
        }

        [TestMethod]
        public void Set_ReturnsCorrectBoolOnChange()
        {
            var viewModel = new FakeViewModel();
            viewModel.TheProperty = "NewValue";
            Assert.IsTrue(viewModel.SetResult);
        }

        [TestMethod]
        public void Set_ReturnsCorrectBoolOnNoChange()
        {
            var viewModel = new FakeViewModel();

            viewModel.TheProperty = "NewValue";
            viewModel.TheProperty = "NewValue"; // On purpose

            Assert.IsFalse(viewModel.SetResult);
        }

        [TestMethod]
        public void RaisePropertychanged_RaisesEvent()
        {
            var viewModel = new FakeViewModel();
            string nameOfPropertyRaised = null;
            viewModel.PropertyChanged += (s, e) => nameOfPropertyRaised = e.PropertyName;

            viewModel.PropertyWithManuallyRaisedEvent = "NewValue";

            Assert.AreEqual(nameof(FakeViewModel.PropertyWithManuallyRaisedEvent), nameOfPropertyRaised);
        }
    }
}
