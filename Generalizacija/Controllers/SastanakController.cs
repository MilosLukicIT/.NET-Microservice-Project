using AutoMapper;
using Generalizacija.Data;
using Generalizacija.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Generalizacija.Controllers
{

    [ApiController]
    [Route("api/sastanak")]
    [Produces("application/json", "application/xml")]
    public class SastanakController : Controller
    {
        private readonly ISastanakRepository sastanakRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public SastanakController(ISastanakRepository sastanakRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.sastanakRepository = sastanakRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<SastanakInfoDto>> Get()
        {
            var sastanci = sastanakRepository.GetAll();

            if (sastanci == null)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<SastanakInfoDto>>( sastanci));
        }
    }
}
