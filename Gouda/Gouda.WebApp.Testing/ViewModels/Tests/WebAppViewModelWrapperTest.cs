using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.WebApp.ViewModels.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestClass]
    public class WebAppViewModelWrapperTest
    {
        private SimpleViewModel TestViewModel = new SimpleViewModel();

        private WebAppViewModelWrapper TestObject = new WebAppViewModelWrapper();

        private void VerifyWebAppViewModel(IWebAppViewModel viewModel)
        {
            Assert.IsInstanceOfType(viewModel, typeof(WebAppViewModel<SimpleViewModel>));
            WebAppViewModel<SimpleViewModel> wrapped = (WebAppViewModel<SimpleViewModel>)viewModel;
            Assert.AreSame(TestViewModel, wrapped.Wrapped);
        }

        [TestMethod]
        public void WrapsViewModel()
        {
            IWebAppViewModel actual = TestObject.Wrap(TestViewModel);

            VerifyWebAppViewModel(actual);
        }

        [TestMethod]
        public async Task CanWrapManyAtOnce()
        {
            List<Task> wrapTasks = new List<Task>();
            for (int i = 0; i < NumberToWrap; i++)
                wrapTasks.Add(WrapTask());

            await Task.WhenAll(wrapTasks);
        }
        private int NumberToWrap = 10;
        private Task WrapTask() => Task.Run(WrapAction);
        private Action WrapAction => () => TestObject.Wrap(TestViewModel);
    }
}
