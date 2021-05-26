using swagger.api.config.Models;
using System.Collections.Generic;
using Xunit;

namespace swagger.api.config.tests.Models
{
    public class ApiConfigurationTests
    {
        #region Constuctor tests

        [Fact]
        public void New_ShouldSetValues()
        {
            var assembly = GetType().Assembly;
            var apiName = "someName";
            var authUrl = "http://auth";
            var scopes = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" }

            };

            var classUnderTest = new ApiConfiguration(assembly, apiName, authUrl, scopes);

            Assert.Equal(assembly, classUnderTest.Assembly);
            Assert.Equal(apiName, classUnderTest.ApiName);
            Assert.Equal(authUrl, classUnderTest.AuthUrl);
            Assert.Equal(scopes, classUnderTest.Scopes);
        }

        #endregion
    }
}
