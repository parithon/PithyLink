using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PithyLink.Web.Attributes;
using PithyLink.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PithyLink.Web.Tests.Pages
{
    public class IndexTests
    {
        [Fact]
        public async Task Index_OnGetAsync_Returns_PageResult()
        {
            var pageModel = new IndexModel(new FakeHttpClientFactory());

            var pageResult = await pageModel.OnGetAsync() as PageResult;

            Assert.NotNull(pageResult);
        }

        [Theory]
        [MemberData(nameof(UrlValidMembers))]
        public async Task Index_OnPostAsync_UrlValid(string ShortenUrl, bool expectedIsValid, Type expectedResultType)
        {
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();

            var required = new RequiredAttribute();
            if (!required.IsValid(ShortenUrl))
            {
                modelState.AddModelError(nameof(ShortenUrl), required.FormatErrorMessage(nameof(ShortenUrl)));
            }

            var isValidUrl = new IsValidUrlAttribute();
            if (!isValidUrl.IsValid(ShortenUrl))
            {
                modelState.AddModelError(nameof(ShortenUrl), isValidUrl.FormatErrorMessage(nameof(ShortenUrl)));
            }

            Assert.Equal(expectedIsValid, modelState.IsValid);

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new IndexModel(new FakeHttpClientFactory())
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                ShortenUrl = ShortenUrl
            };

            var result = await pageModel.OnPostAsync();

            Assert.IsType(expectedResultType, result);
        }

        [Theory]
        [MemberData(nameof(DoesntAllowRedirectsMembers))]
        public async Task Index_OnPostAsync_DoesntAllowRedirects(string ShortenUrl, Type expectedResultType)
        {
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new IndexModel(new FakeHttpClientFactory())
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                ShortenUrl = ShortenUrl
            };

            var result = await pageModel.OnPostAsync();

            Assert.IsType(expectedResultType, result);
        }

        [Theory]
        [MemberData(nameof(DoesntAllowLoopingMembers))]
        public async Task Index_OnPostAsync_DoesntAllowLooping(string ShortenUrl, Type expectedResultType, string expectedErrorMessage)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Host = new HostString("localhost");

            var modelState = new ModelStateDictionary();

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new IndexModel(new FakeHttpClientFactory())
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                ShortenUrl = ShortenUrl
            };

            var result = await pageModel.OnPostAsync();

            Assert.IsType(expectedResultType, result);

            var value = pageModel.ModelState.GetValueOrDefault(nameof(ShortenUrl));

            Assert.Equal(expectedErrorMessage, value?.Errors[0].ErrorMessage ?? "");
        }

        public static IEnumerable<object[]> UrlValidMembers()
        {
            return new List<object[]>()
            {
                new object[] { FakeHttpMessageHandler.EmptyRequest, false, typeof(PageResult) },
                new object[] { FakeHttpMessageHandler.InvalidRequst, false, typeof(PageResult) },
                new object[] { FakeHttpMessageHandler.UnsupportedRequest, false, typeof(PageResult) },
                new object[] { FakeHttpMessageHandler.ValidRequest, true, typeof(RedirectToPageResult) }
            };
        }

        public static IEnumerable<object[]> DoesntAllowRedirectsMembers()
        {
            return new List<object[]>()
            {
                new object[] { FakeHttpMessageHandler.RedirectRequest, typeof(PageResult) },
                new object[] { FakeHttpMessageHandler.PerminentRedirectRequest, typeof(PageResult) },
                new object[] { FakeHttpMessageHandler.ValidRequest, typeof(RedirectToPageResult) }
            };
        }

        public static IEnumerable<object[]> DoesntAllowLoopingMembers()
        {
            return new List<object[]>
            {
                new object[] { FakeHttpMessageHandler.LoopbackRequest, typeof(PageResult), IndexModel.UrlLoopbackNotSupported },
                new object[] { FakeHttpMessageHandler.LoopbackSslRequest, typeof(PageResult), IndexModel.UrlLoopbackNotSupported },
                new object[] { FakeHttpMessageHandler.ValidRequest, typeof(RedirectToPageResult), "" }
            };
        }
    }
}
