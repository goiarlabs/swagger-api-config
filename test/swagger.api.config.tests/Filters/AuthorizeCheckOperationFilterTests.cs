using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Moq;
using swagger.api.config.Filters;
using swagger.api.config.tests.Fakes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;
using Xunit;

namespace swagger.api.config.tests.Filters
{
    public class AuthorizeCheckOperationFilterTests
    {
        #region Apply Tests

        [Fact]
        public void Apply_ShouldDoNothing_WhenDeclaringTypeHasNotVersionedRouteAttribute()
        {
            var operation = ExcecuteApply(typeof(NonVersionedFakeController), NonVersionedFakeController.NonVersionedActionMethodName);

            Assert.DoesNotContain(operation.Responses, s => s.Key == "401");
            Assert.DoesNotContain(operation.Responses, s => s.Key == "403");
        }

        [Fact]
        public void Apply_ShoulAddResponses_WhenMethodHasVersionedRouteAttribute()
        {
            var operation = ExcecuteApply(typeof(NonVersionedFakeController), NonVersionedFakeController.VersionedActionMethodName);

            Assert.Contains(operation.Responses, s => s.Key == "401");
            Assert.Contains(operation.Responses, s => s.Key == "403");
        }

        [Fact]
        public void Apply_ShoulAddResponses_WhenControllerAndMethodHasVersionedRouteAttribute()
        {
            var operation = ExcecuteApply(typeof(VersionedFakeController), VersionedFakeController.VersionedActionMethodName);

            Assert.Contains(operation.Responses, s => s.Key == "401");
            Assert.Contains(operation.Responses, s => s.Key == "403");
        }

        [Fact]
        public void Apply_ShoulAddResponses_WhenControllerHasVersionedRouteAttribute()
        {
            var operation = ExcecuteApply(typeof(VersionedFakeController), VersionedFakeController.NonVersionedActionMethodName);

            Assert.Contains(operation.Responses, s => s.Key == "401");
            Assert.Contains(operation.Responses, s => s.Key == "403");
        }

        #endregion

        #region Private methods

        private static OpenApiOperation ExcecuteApply(Type controllerType, string methodName)
        {
            var operation = new OpenApiOperation();

            var method = controllerType
                    .GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

            var context = new OperationFilterContext(
                new ApiDescription(),
                Mock.Of<ISchemaGenerator>(),
                new SchemaRepository(),
                method
            );

            var classUnderTest = new AuthorizeCheckOperationFilter();

            classUnderTest.Apply(operation, context);

            return operation;
        }

        #endregion
    }
}
