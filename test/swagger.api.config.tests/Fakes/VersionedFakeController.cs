using Microsoft.AspNetCore.Mvc;

namespace swagger.api.config.tests.Fakes
{
    [VersionedRoute("someRoute")]
    [ApiController]
    public class VersionedFakeController : Controller
    {
        public const string NonVersionedActionMethodName = "NonVersionedAction";
        public const string VersionedActionMethodName = "VersionedAction";

        [HttpGet]
        public IActionResult NonVersionedAction() => new OkResult();

        [HttpGet, VersionedActionRoute("someRoute")]
        public IActionResult VersionedAction() => new OkResult();
    }
}
