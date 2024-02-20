using AutoMapper;
using Generalizacija.Data;
using Generalizacija.Dto;
using Generalizacija.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Generalizacija.Controllers
{

    [ApiController]
    [Route("api/sastanak/tim")]
    [Produces("application/json", "application/xml")]
    public class TimskiController : Controller
    {
        private readonly ITimskiSastanakRepository tim;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;



        public TimskiController(ITimskiSastanakRepository tim, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tim = tim;
            this.linkGenerator = linkGenerator; 
            this.mapper = mapper;  
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<TimskiSastanakDto>> Get()
        {
            var sastanci = tim.GetAll();

            if (sastanci == null)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<TimskiSastanakDto>>(sastanci));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<TimskiSastanakDto> CreateSastanak([FromBody] TimskiSastanakDto sastanakCreation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var mappedSastanak = mapper.Map<TimskiSastanak>(sastanakCreation);
                TimskiSastanak indSastanak = tim.CreateInd(mappedSastanak);
                tim.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetSastanak", "Sastanak", new { id = indSastanak.Id});

                return Created(location, mapper.Map<TimskiSastanakDto>(indSastanak));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
