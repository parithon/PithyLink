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
            var pageModel = new IndexModel();

            var pageResult = await pageModel.OnGetAsync() as PageResult;

            Assert.NotNull(pageResult);
        }

        [Theory]
        [InlineData("", false, typeof(PageResult))]
        [InlineData("NotAValidUrl", false, typeof(PageResult))]
        [InlineData("mailto://git@github.com", false, typeof(PageResult))]
        [InlineData("http://github.com", true, typeof(RedirectToPageResult))]
        [InlineData("https://github.com", true, typeof(RedirectToPageResult))]
        public async Task Index_OnPostAsync(string ShortenUrl, bool expectedIsValid, Type expectedResultType)
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

            var pageModel = new IndexModel
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                ShortenUrl = ShortenUrl
            };

            var result = await pageModel.OnPostAsync();

            Assert.IsType(expectedResultType, result);
        }
    }
}
