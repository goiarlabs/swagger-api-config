using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using swagger.api.config.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace swagger.api.config.Models
{
    /// <summary>
    /// Manages the options of a Swagger Configuration.
    /// </summary>
    public class SwaggerGenConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly ApiConfiguration _apiConfiguration;

        /// <summary>
        /// Constructor with injected dependencies.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="apiConfiguration"></param>
        public SwaggerGenConfigurationOptions(IApiVersionDescriptionProvider provider, ApiConfiguration apiConfiguration)
        {
            _provider = provider;
            _apiConfiguration = apiConfiguration;
        }

        /// <summary>
        /// Sets the Swagger configuration based on values of <see cref="IApiVersionDescriptionProvider"/> and <see cref="ApiConfiguration"/>
        /// received in constructor.
        /// </summary>
        /// <param name="options">Options variable to apply the configurations.</param>
        public void Configure(SwaggerGenOptions options)
        {
            var xmlFile = $"{_apiConfiguration.Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  description.GroupName,
                  new OpenApiInfo
                  {
                      Title = _apiConfiguration.ApiName,
                      Version = description.ApiVersion.ToString(),
                      Contact = new OpenApiContact()
                      {
                          Name = "Goiar"
                      }
                  });
            }

            options.IncludeXmlComments(xmlPath);
            options.OperationFilter<AuthorizeCheckOperationFilter>();
            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(_apiConfiguration.AuthUrl.TrimEnd('/') + "/connect/authorize"),
                        TokenUrl = new Uri(_apiConfiguration.AuthUrl.TrimEnd('/') + "/connect/token"),
                        Scopes = _apiConfiguration.Scopes
                    }
                }
            });
        }
    }
}
