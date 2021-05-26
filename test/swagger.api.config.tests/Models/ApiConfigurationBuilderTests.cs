using swagger.api.config.Models;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace swagger.api.config.tests.Models
{
    public class ApiConfigurationBuilderTests
    {
        #region WithAssembly Tests

        [Fact]
        public void WithAssembly_ShouldSetAnAssembly()
        {
            var assembly = GetType().Assembly;
            var classUnderTest = new ApiConfigurationBuilder();

            classUnderTest.WithAssembly(assembly);

            var result = GetApiConfiguration(classUnderTest);

            Assert.Equal(assembly, result.Assembly);
        }

        #endregion

        #region WithApiName Tests

        [Fact]
        public void WithApiName_ShouldSetAnApiName()
        {
            var apiName = "someName";
            var classUnderTest = new ApiConfigurationBuilder();

            classUnderTest.WithApiName(apiName);

            var result = GetApiConfiguration(classUnderTest);

            Assert.Equal(apiName, result.ApiName);
        }

        #endregion

        #region WithAuthUrl Tests

        [Fact]
        public void WithAuthUrl_ShouldSetAnAuthUrl()
        {
            var authUrl = "http://auth";
            var classUnderTest = new ApiConfigurationBuilder();

            classUnderTest.WithAuthUrl(authUrl);

            var result = GetApiConfiguration(classUnderTest);

            Assert.Equal(authUrl, result.AuthUrl);
        }

        #endregion

        #region WithScopes Tests

        [Fact]
        public void WithScopes_ShouldSetScopes()
        {
            var scopes = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            var classUnderTest = new ApiConfigurationBuilder();

            classUnderTest.WithScopes(scopes);

            var result = GetApiConfiguration(classUnderTest);

            Assert.Equal(scopes, result.Scopes);
        }

        #endregion

        #region WithScope Tests

        [Fact]
        public void WithScope_ShouldAddTheScope()
        {
            var scope = new KeyValuePair<string, string>("key", "value");
            var classUnderTest = new ApiConfigurationBuilder();

            classUnderTest.WithScope(scope.Key, scope.Value);

            var result = GetApiConfiguration(classUnderTest);

            Assert.Contains(result.Scopes, a => a.Key == scope.Key && a.Value == scope.Value);
        }

        #endregion

        #region Private methods

        private static ApiConfiguration GetApiConfiguration(ApiConfigurationBuilder builder)
        {
            return typeof(ApiConfigurationBuilder)
                .GetField("_apiConfiguration", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(builder) as ApiConfiguration;
        }

        #endregion
    }
}
