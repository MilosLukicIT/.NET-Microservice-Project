using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{

    [ApiController]
    [Route("api/korisnik/uloga")]
    [Produces("application/json", "application/xml")]
    public class UserRoleController : Controller
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;


        public UserRoleController(LinkGenerator linkGenerator, IMapper mapper)
        {
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        
    }
}
