using PithyLink.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PithyLink.Web.Tests.Attributes
{
    public class IsValidUrlAttributeTest
    {
        [Theory]
        [InlineData("NotValidUrl", false)]
        [InlineData("http://github.com", true)]
        [InlineData("mailto://git@github.com", false)]
        [InlineData("https://github.com", true)]
        public void IsValidUrlAttribute_IsValid(string ShortenUrl, bool expected)
        {
            var att = new IsValidUrlAttribute();
            var result = att.IsValid(ShortenUrl);
            Assert.Equal(expected, result);
        }
    }
}
