using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace MessageService
{
    /// <inheritdoc />
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        /// <summary>Редирект на swagger.</summary>
        /// <returns>ActionResult.</returns>
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}