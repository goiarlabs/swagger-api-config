using System;

namespace Microsoft.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class VersionedRouteAttribute : RouteAttribute
    {
        public VersionedRouteAttribute(string template) : base("api/v{v:apiVersion}/" + template)
        {
            if (template.StartsWith("/"))
            {
                throw new ArgumentException(
                    $"template route '{template}' cannot start with '/'");
            }
        }
    }
}
