﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.LocalRedirectTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class LocalRedirectTestBuilderTests
    {
        [Fact]
        public void PermanentShouldNotThrowExceptionWhenRedirectIsPermanent()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectPermanentAction())
                .ShouldReturn()
                .LocalRedirect()
                .Permanent();
        }

        [Fact]
        public void PermanentShouldThrowExceptionWhenRedirectIsNotPermanent()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .Permanent();
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result to be permanent, but in fact it was not.");
        }

        [Fact]
        public void ToUrlWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrl("/local/test");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl("/local");
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be '/local', but instead received '/local/test'.");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl("http://somehost!@#?Query==true");
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be URI valid, but instead received 'http://somehost!@#?Query==true'.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidaPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrlPassing(url => url.StartsWith("/local/"));
        }

        [Fact]
        public void ToUrlPassingShouldThrowExceptionWithInvalidaPredicate()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrlPassing(url => url.StartsWith("/test/"));
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location ('/local/test') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidaAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrlPassing(url =>
                {
                    Assert.True(url.StartsWith("/local/"));
                });
        }

        [Fact]
        public void ToUrlWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrl(new Uri("/local/test", UriKind.Relative));
        }

        [Fact]
        public void ToUrlWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl(new Uri("/local", UriKind.Relative));
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be '/local', but instead received '/local/test'.");
        }

        [Fact]
        public void ToUrlWithUriBuilderShouldNotThrowExceptionWithCorrectUri()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrl(url => url.WithAbsolutePath("/local/test"));
        }
        
        [Fact]
        public void WithCustomUrlHelperShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(urlHelper))
                .ShouldReturn()
                .LocalRedirect()
                .WithUrlHelper(urlHelper);
        }

        [Fact]
        public void WithCustomUrlHelperShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(urlHelper))
                        .ShouldReturn()
                        .LocalRedirect()
                        .WithUrlHelper(null);
                },
                "When calling LocalRedirectActionWithCustomUrlHelper action in MvcController expected local redirect result UrlHelper to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(urlHelper))
                .ShouldReturn()
                .LocalRedirect()
                .WithUrlHelperOfType<CustomUrlHelper>();
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(urlHelper))
                        .ShouldReturn()
                        .LocalRedirect()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling LocalRedirectActionWithCustomUrlHelper action in MvcController expected local redirect result UrlHelper to be of IUrlHelper type, but instead received CustomUrlHelper.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithNoUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(null))
                        .ShouldReturn()
                        .LocalRedirect()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling LocalRedirectActionWithCustomUrlHelper action in MvcController expected local redirect result UrlHelper to be of IUrlHelper type, but instead received null.");
        }

        [Fact]
        public void ToShouldWorkCorrectly()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(null))
                        .ShouldReturn()
                        .LocalRedirect()
                        .To<NoAttributesController>(c => c.WithParameter(1));
                },
                "When calling LocalRedirectActionWithCustomUrlHelper action in MvcController expected local redirect result to have resolved location to '/api/Redirect/WithParameter?id=1', but in fact received '/api/test'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ToShouldWorkCorrectlyToAsyncAction()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(null))
                .ShouldReturn()
                .LocalRedirect()
                .To<MvcController>(c => c.AsyncOkResultAction());

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectPermanentAction())
                .ShouldReturn()
                .LocalRedirect()
                .Permanent()
                .AndAlso()
                .ToUrl("/local/test");
        }
    }
}
