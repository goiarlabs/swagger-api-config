using System;

namespace Microsoft.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VersionedActionRouteAttribute : RouteAttribute
    {
        public VersionedActionRouteAttribute(string template) : base("~/api/v{v:apiVersion}/" + template)
        {
            if (template.StartsWith("/"))
            {
                throw new ArgumentException(
                    $"template route '{template}' cannot start with '/'");
            }
        }
    }
}
