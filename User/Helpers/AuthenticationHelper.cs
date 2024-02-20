using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserMicroservice.Data;
using UserMicroservice.Entites;
using UserMicroservice.Models;

namespace UserMicroservice.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {

        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public AuthenticationHelper (IConfiguration configuration, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
        }
        public bool AuthenticationPrincipal(Principal principal)
        {
            if (userRepository.UserWithCedentialsExists(principal.EmailKorisnika, principal.LozinkaKorisnika))
            {
                return true;
            }
            return false;
        }

        public string GenerateJwt(Principal principal)
        {
            var user = userRepository.GetUserByEmail(principal.EmailKorisnika);

            principal.NazivUloge = userRoleRepository.GetUserRoleById((Guid)user.UlogaId).NazivUloge;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, principal.NazivUloge)
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
