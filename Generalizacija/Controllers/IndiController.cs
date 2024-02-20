using AutoMapper;
using Generalizacija.Data;
using Generalizacija.Dto;
using Generalizacija.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Generalizacija.Controllers
{

    [ApiController]
    [Route("api/sastanak/ind")]
    [Produces("application/json", "application/xml")]
    public class IndiController : Controller
    {
        private readonly IIndSastanakRepository indRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;



        public IndiController(IIndSastanakRepository indRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.indRepository = indRepository;
            this.linkGenerator = linkGenerator; 
            this.mapper = mapper;  
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<SastanakDto>> Get()
        {
            var sastanci = indRepository.GetAll();

            if (sastanci == null)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<SastanakDto>>(sastanci));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<SastanakDto> CreateSastanak([FromBody] SastanakDto sastanakCreation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var mappedSastanak = mapper.Map<IndividualniSastanak>(sastanakCreation);
                IndividualniSastanak indSastanak = indRepository.CreateInd(mappedSastanak);
                indRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetSastanak", "Sastanak", new { id = indSastanak.Id});

                return Created(location, mapper.Map<SastanakDto>(indSastanak));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
