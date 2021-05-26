using System;
using System.Collections.Generic;
using System.Reflection;

namespace swagger.api.config.Models
{
    /// <summary>
    /// Used by the service collection extensions
    /// giving a fluent, cool and parametrized way to create an ApiConfiguration.
    /// </summary>
    public class ApiConfigurationBuilder
    {
        #region Fields

        private readonly ApiConfiguration _apiConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="ApiConfigurationBuilder"/>
        /// </summary>
        public ApiConfigurationBuilder()
        {
            _apiConfiguration = new ApiConfiguration(
                assembly: null,
                apiName: string.Empty,
                authUrl: string.Empty,
                scopes: new Dictionary<string, string>());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public ApiConfigurationBuilder WithAssembly(Assembly assembly)
        {
            _apiConfiguration.Assembly = assembly;

            return this;
        }

        /// <summary>
        /// Sets the name of the API.
        /// </summary>
        /// <param name="apiName">The name of the API.</param>
        /// <returns></returns>
        public ApiConfigurationBuilder WithApiName(string apiName)
        {
            _apiConfiguration.ApiName = apiName;

            return this;
        }

        /// <summary>
        /// Sets the URL of the authorization service.
        /// </summary>
        /// <param name="authUrl">The URL of the authorization service.</param>
        /// <returns></returns>
        public ApiConfigurationBuilder WithAuthUrl(string authUrl)
        {
            _apiConfiguration.AuthUrl = authUrl;

            return this;
        }


        /// <summary>
        /// Sets the scopes referred to the API to configure.
        /// </summary>
        /// <param name="scopes">The list of the scopes.</param>
        /// <returns></returns>
        public ApiConfigurationBuilder WithScopes(IDictionary<string, string> scopes)
        {
            _apiConfiguration.Scopes = scopes;

            return this;
        }

        /// <summary>
        /// Sets the scopes referred to the API to configure.
        /// </summary>
        /// <param name="scopes">The list of the scopes.</param>
        /// <returns></returns>
        public ApiConfigurationBuilder WithScope(string scopeId, string description)
        {
            _apiConfiguration.Scopes.Add(scopeId, description);

            return this;
        }


        #endregion

        #region Internal methods

        /// <summary>
        /// Returns the wanted instance of type <see cref="ApiConfiguration"/>.
        /// <exception cref="System.Exception">Thrown when any property has an invalid value.</exception>
        /// </summary>
        /// <returns>An instance of type <see cref="ApiConfiguration"/>.</returns>
        internal ApiConfiguration Build()
        {
            if (string.IsNullOrWhiteSpace(_apiConfiguration.ApiName))
            {
                throw new Exception($"ApiName {_apiConfiguration.ApiName} is not valid. It cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(_apiConfiguration.AuthUrl))
            {
                throw new Exception($"AuthUrl {_apiConfiguration.AuthUrl} is not valid. It cannot be null or empty.");
            }

            if (_apiConfiguration.Scopes == null || _apiConfiguration.Scopes.Count == 0)
            {
                throw new Exception("Scopes are invalid. They cannot be null or empty.");
            }

            if (_apiConfiguration.Assembly == null)
            {
                throw new Exception("Assembly is invalid. It cannot be null.");
            }

            return _apiConfiguration;
        }

        #endregion
    }
}
