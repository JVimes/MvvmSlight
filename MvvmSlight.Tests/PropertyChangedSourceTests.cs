using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvvmSlight.Tests
{
    [TestClass]
    public partial class PropertyChangedSourceTests
    {
        [TestMethod]
        public void Set_ChangesBackingField()
        {
            var viewModel = new ViewModel();
            var newValue = "NewValue";

            viewModel.TheProperty = newValue;

            Assert.AreEqual(newValue, viewModel.ThePropertyBackingField);
        }

        [TestMethod]
        public void Set_FiresEventOnChange()
        {
            var viewModel = new ViewModel();
            string nameOfPropertyRaised = null;
            viewModel.PropertyChanged += (s, e) => nameOfPropertyRaised = e.PropertyName;

            viewModel.TheProperty = "NewValue";

            Assert.AreEqual(nameof(ViewModel.TheProperty), nameOfPropertyRaised);
        }

        [TestMethod]
        public void Set_DoesNotFireEventOnNoChange()
        {
            var viewModel = new ViewModel();
            viewModel.TheProperty = "NewValue";
            bool propertyWasRaised = false;
            viewModel.PropertyChanged += (s, e) => propertyWasRaised = true;

            viewModel.TheProperty = "NewValue";

            Assert.IsFalse(propertyWasRaised);
        }

        [TestMethod]
        public void Set_ReturnsCorrectBoolOnChange()
        {
            var viewModel = new ViewModel();
            viewModel.TheProperty = "NewValue";
            Assert.IsTrue(viewModel.SetResult);
        }

        [TestMethod]
        public void Set_ReturnsCorrectBoolOnNoChange()
        {
            var viewModel = new ViewModel();

            viewModel.TheProperty = "NewValue";
            viewModel.TheProperty = "NewValue"; // On purpose

            Assert.IsFalse(viewModel.SetResult);
        }

        [TestMethod]
        public void RaisePropertychanged_RaisesEvent()
        {
            var viewModel = new ViewModel();
            string nameOfPropertyRaised = null;
            viewModel.PropertyChanged += (s, e) => nameOfPropertyRaised = e.PropertyName;

            viewModel.PropertyWithManuallyRaisedEvent = "NewValue";

            Assert.AreEqual(nameof(ViewModel.PropertyWithManuallyRaisedEvent), nameOfPropertyRaised);
        }
    }
}
