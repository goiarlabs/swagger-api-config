using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using swagger.api.config.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for Swagger.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures Swagger in your project based on values passed by <see cref="builder"/> parameter.
        /// </summary>
        /// <param name="services">Current <see cref="IServiceCollection"/> to which this extension will be applied.</param>
        /// <param name="builder">Allows ApiConfiguration settings.</param>
        /// <returns>A variable of type <see cref="IServiceCollection"/> updated with Swagger configurations.</returns>
        public static IServiceCollection AddVersionedSwagger(this IServiceCollection services, Action<ApiConfigurationBuilder> builder)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });

            var apiConfigBuilder = new ApiConfigurationBuilder();
            builder(apiConfigBuilder);
            var apiConfig = apiConfigBuilder.Build();
            services.AddSingleton(apiConfig);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigurationOptions>();
            return services.AddSwaggerGen();
        }
    }
}
