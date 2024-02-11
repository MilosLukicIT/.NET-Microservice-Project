using UserMicroservice.Entites;
using UserMicroservice.Models;

namespace UserMicroservice.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticationPrincipal(Principal principal);
        public string GenerateJwt(Principal principal);
    }
}
