using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace swagger.api.config.tests.Attributes
{
    public class VersionedRouteAttributeTests
    {
        #region Constants

        private const string Prefix = "api/v{v:apiVersion}/";

        #endregion

        #region Constructor tests

        [Theory]
        [InlineData("template1")]
        [InlineData("template1/asd")]
        [InlineData("template1/{id}/pepe")]
        public void New_ShouldAddAPrefix(string template)
        {
            var classUnderTest = new VersionedRouteAttribute(template);

            Assert.Equal(Prefix + template, classUnderTest.Template);
        }

        [Fact]
        public void New_ShouldThrowArgumentException_WhenTemplateStartsWithSlash()
        {
            var template = "/sometemplate";

            var ex = Assert.Throws<ArgumentException>(() => new VersionedRouteAttribute(template));

            Assert.Contains(template, ex.Message);
        }

        #endregion
    }
}
