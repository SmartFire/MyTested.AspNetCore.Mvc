﻿namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using Contracts.ViewComponentResults;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using ViewComponentResults;
    using Utilities.Validators;
    using Exceptions;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IViewTestBuilder View()
        {
            return this.View(null);
        }

        /// <inheritdoc />
        public IViewTestBuilder View(string viewName)
        {
            var viewResult = InvocationResultValidator
                .GetInvocationResult<ViewViewComponentResult>(this.TestContext);

            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                throw ViewViewComponentResultAssertionException.ForNameEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IViewTestBuilder View<TModel>(TModel model)
        {
            return this.View(null, model);
        }

        /// <inheritdoc />
        public IViewTestBuilder View<TModel>(string viewName, TModel model)
        {
            var viewTestBuilder = this.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
