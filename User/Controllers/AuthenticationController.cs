using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Helpers;
using UserMicroservice.Models;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : Controller
    {

        private readonly IAuthenticationHelper authenticationHelper;


        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
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
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }
}
