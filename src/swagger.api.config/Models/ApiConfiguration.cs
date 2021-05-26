using System.Collections.Generic;
using System.Reflection;

namespace swagger.api.config.Models
{
    /// <summary>
    /// Represents data about an API that Swagger needs to build its documents.
    /// </summary>
    public class ApiConfiguration
    {
        #region Constructor

        public ApiConfiguration(Assembly assembly, string apiName, string authUrl, IDictionary<string, string> scopes)
        {
            Assembly = assembly;
            ApiName = apiName;
            AuthUrl = authUrl;
            Scopes = scopes;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current assembly.
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// The name to be used as title in Swagger documents.
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// The URL of the Identity Provider.
        /// </summary>
        public string AuthUrl { get; set; }

        /// <summary>
        /// The scopes in which the API is involved.
        /// The key must have the scope identifier.
        /// The value must have the scope description.
        /// </summary>
        public IDictionary<string, string> Scopes { get; set; }

        #endregion
    }
}
