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

            public bool SetResult { get; set; }
            internal string ThePropertyBackingField => theProperty;
        }

        [TestMethod]
        public void SetChangesBackingField()
        {
            var viewModel = new FakeViewModel();
            var newValue = "NewValue";
            viewModel.TheProperty = newValue;

            Assert.AreEqual(newValue, viewModel.ThePropertyBackingField);
        }

        [TestMethod]
        public void SetFiresEventOnChange()
        {
            var viewModel = new FakeViewModel();
            string propertyNameFired = null;
            viewModel.PropertyChanged += (s, e) => propertyNameFired = e.PropertyName;
            viewModel.TheProperty = "NewValue";
            Assert.AreEqual(nameof(FakeViewModel.TheProperty), propertyNameFired);
        }

        [TestMethod]
        public void SetDoesNotFireEventOnNoChange()
        {
            var viewModel = new FakeViewModel();
            viewModel.TheProperty = "NewValue";
            string propertyNameFired = null;
            viewModel.PropertyChanged += (s, e) => propertyNameFired = e.PropertyName;
            viewModel.TheProperty = "NewValue";
            Assert.IsNull(propertyNameFired);
        }

        [TestMethod]
        public void SetReturnsCorrectBoolOnChange()
        {
            var viewModel = new FakeViewModel();
            viewModel.TheProperty = "NewValue";
            Assert.IsTrue(viewModel.SetResult);
        }

        [TestMethod]
        public void SetReturnsCorrectBoolOnNoChange()
        {
            var viewModel = new FakeViewModel();
            viewModel.TheProperty = "NewValue";
            viewModel.TheProperty = "NewValue"; // On purpose
            Assert.IsFalse(viewModel.SetResult);
        }
    }
}
