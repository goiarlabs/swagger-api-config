using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace swagger.api.config.Filters
{
    /// <summary>
    /// Checks every request with the eflow auth attribute, 
    /// and if it has a match adds common responses and a security requirement
    /// </summary>
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
              context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<VersionedRouteAttribute>().Any()
              || context.MethodInfo.GetCustomAttributes(true).OfType<VersionedActionRouteAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"
                                }
                            }
                        ] = new[] {"Api"}
                    }
                };

            }
        }
    }
}
