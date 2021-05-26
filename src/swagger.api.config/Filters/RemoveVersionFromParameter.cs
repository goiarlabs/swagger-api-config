using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace swagger.api.config.Filters
{
    /// <summary>
    /// Checks whether if the operation has a paremeter assoiciated to api versioning,
    /// If it finds it, this one is removed
    /// </summary>
    public class RemoveVersionFromParameter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Parameters.Any())
                return;

            var versionParameter = operation.Parameters
                .FirstOrDefault(p => p.Name.ToLower() == "v");

            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }
}
