using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using swagger.api.config.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {

                var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                var apiConfig = app.ApplicationServices.GetService<ApiConfiguration>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{apiConfig.ApiName} {description.GroupName.ToUpperInvariant()}");
                }
            });

            return app;
        }
    }
}
