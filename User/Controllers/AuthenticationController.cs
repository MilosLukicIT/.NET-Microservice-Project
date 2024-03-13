using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Helpers;
using UserMicroservice.Models;
using UserMicroservice.ServiceCalls;
using UserMicroservice.Models.DTO;


namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : Controller
    {

        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ILoggerService loggerService;


        public AuthenticationController(IAuthenticationHelper authenticationHelper, ILoggerService loggerService)
        {
            this.authenticationHelper = authenticationHelper;
            this.loggerService = loggerService;
        }


        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public IActionResult Authenticate(Principal principal)
        {
            if (authenticationHelper.AuthenticationPrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                loggerService.Log(LogLevel.Information, "Authenticate", "Authentication successfull!");
                return Ok(new { token = tokenString });
            }

            loggerService.Log(LogLevel.Warning, "Authenticate", "Authentication failed!");
            return Unauthorized();
        }
    }
}
