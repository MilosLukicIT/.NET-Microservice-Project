
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace GatewayMicroservice
{
    public class OcelotJwtMiddleware
    {
        private static readonly string RoleSeparator = ",";
         public OcelotJwtMiddleware() { }


        public static Func<HttpContext, Func<Task>, Task> CreateAuthorizationFilter
        => async (httpContext, next) =>
        {

            if(httpContext.Items.DownstreamRoute().DownstreamPathTemplate.ToString() != "/api/korisnik/authenticate" && !httpContext.Items.DownstreamRoute().RouteClaimsRequirement.IsNullOrEmpty())
            {
                Console.WriteLine("Route needs authorization! Starting authorization.");
                var authorizationHeader = httpContext.Request.Headers["Authorization"];
                string accessToken = "";
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    accessToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
                }

                if (accessToken != null && AuthorizeIfValidToken(httpContext, accessToken))
                {
                    Console.WriteLine("Authorization success!");
                        await next.Invoke();
                }
                else
                {
                    Console.WriteLine("User does not have required rights!");
                    httpContext.Items.UpsertDownstreamResponse(new DownstreamResponse(new HttpResponseMessage(HttpStatusCode.Unauthorized)));
                }
            } 
            else
            {
                await next.Invoke();
            }
            
        };

        private static bool AuthorizeIfValidToken(HttpContext downStreamContext, string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            var claims = decodedToken.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
            string userRole = "";
            for (int i = 0; claims.Count > i; i++)
            {
                if (claims[i].Type == "Role")
                {
                    userRole = claims[i].Value;
                }
            }

            if (decodedToken != null)
            {
                return downStreamContext.Items.DownstreamRoute().RouteClaimsRequirement["Role"]
                    ?.Split(RoleSeparator)
                    .FirstOrDefault(role => role.Trim() == userRole) != default;
            }

            return false;
        }
    }
}