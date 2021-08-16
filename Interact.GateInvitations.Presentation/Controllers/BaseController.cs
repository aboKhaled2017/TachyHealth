using Interact.GateInvitations.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController: ControllerBase
    {
        public BaseController()
        {
            var userId = Utility.GetCurrentLoggedUserId();
            LoggedUserId =userId is null ?Guid.Empty:Guid.Parse(userId) ;
        }
        protected Guid LoggedUserId { get; private set; }
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
