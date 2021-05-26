using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace swagger.api.config.Filters
{
    /// <summary>
    /// Looks for version parameter on the document's urls,
    /// If this parameter is found, it changes it to the swagger version
    /// </summary>
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
                throw new ArgumentNullException(nameof(swaggerDoc));

            var replacements = new OpenApiPaths();

            foreach (var path in swaggerDoc.Paths)
            {
                replacements.Add(path.Key.Replace("v{v}", swaggerDoc.Info.Version), path.Value);
            }

            swaggerDoc.Paths = replacements;
        }
    }
}
